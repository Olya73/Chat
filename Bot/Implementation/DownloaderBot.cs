using Contract.Bot.Interface;
using Contract.ConfigurationModel;
using Contract.DTO;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BotLibrary.Implementation
{
    public class DownloaderBot : IMessageBot
    {
        public string Name => "Downloader";
        string pattern = @"^скачай\s(http(?:s)?://(?:[\w-]+(?:\.[\w-]+))+(?:/[\w-]+)+\.(?:jpg|mp3|mp4|pdf|doc))\s((?:\w)+)\z";

        private readonly DownloaderBotSettings _settings;
        public DownloaderBot(IOptions<DownloaderBotSettings> options)
        {
            _settings = options.Value;
        }

        public string OnMessage(ChatEventGetDTO chatEventGetDTO)
        {            
            RpcClient rpcClient = new RpcClient(_settings);
            var s = rpcClient.Call(GetJson(chatEventGetDTO).ToString());
            rpcClient.Close();
            return s;
        }

        protected JObject GetJson(ChatEventGetDTO chatEventGetDTO)
        {
            Match match = Regex.Match(chatEventGetDTO.Message.Text, pattern);
            if (match.Success)
            {
                string uri = match.Result("$1");
                string folder = match.Result("$2");
                JObject jObject = new JObject(
                    new JProperty(nameof(uri), uri),
                    new JProperty(nameof(folder), folder),
                    new JProperty(nameof(chatEventGetDTO.User.Login).ToLower(), chatEventGetDTO.User.Login));

                return jObject;
            }
            return null;
        }
    }

    public class RpcClient
    {
        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly EventingBasicConsumer consumer;
        private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
        private readonly IBasicProperties props;

        public RpcClient(DownloaderBotSettings settings)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = settings.RMQConnectionSettings.HostName,
                Port = settings.RMQConnectionSettings.Port,
                VirtualHost = settings.RMQConnectionSettings.VirtualHost,
                UserName = settings.RMQConnectionSettings.UserName,
                Password = settings.RMQConnectionSettings.Password
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            replyQueueName = channel.QueueDeclare().QueueName;
            consumer = new EventingBasicConsumer(channel);

            props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var response = Encoding.UTF8.GetString(body);
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    respQueue.Add(response);
                }
            };
        }

        public string Call(string message)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            try
            {
                channel.QueueDeclarePassive("rpc");
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "rpc",
                    basicProperties: props,
                    body: messageBytes);

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: true);

                return respQueue.Take();
            }
            catch (OperationInterruptedException)
            {
                return "Сервер недоступен для скачивания";
            }
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
