using Contract.Bot.Interface;
using Contract.DTO;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

        public string OnMessage(BotMessageDTO botMessageDTO)
        {            
            RpcClient rpcClient = new RpcClient();
            var s = rpcClient.Call(GetJson(botMessageDTO).ToString());
            rpcClient.Close();
            return s;
        }

        protected JObject GetJson(BotMessageDTO botMessageDTO)
        {
            Match match = Regex.Match(botMessageDTO.Message, pattern);
            if (match.Success)
            {
                string url = match.Result("$1");
                string folder = match.Result("$2");
                JObject jObject = new JObject(
                    new JProperty(nameof(url), url),
                    new JProperty(nameof(folder), folder),
                    new JProperty(nameof(botMessageDTO.Login), botMessageDTO.Login));

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

        public RpcClient()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

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
            channel.BasicPublish(
                exchange: "",
                routingKey: "rpc_queue",
                basicProperties: props,
                body: messageBytes);

            channel.BasicConsume(
                consumer: consumer,
                queue: replyQueueName,
                autoAck: true);

            return respQueue.Take();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
