using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RabbitMQ.Worker
{
    class Program
    {
        static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost", DispatchConsumersAsync = true };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "rpc_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var reply = channel.CreateBasicProperties();
                var prop = ea.BasicProperties;
                reply.CorrelationId = prop.CorrelationId;
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                JObject json = JObject.Parse(message);
                
                string path = CreatePathAsync(json);
                var url = json.SelectToken("url").ToString();
                string response = null;

                try
                {
                    await DownloadAsync(path, url);
                    response = "Файл успешно скачан на сервер";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    response = ex.Message;
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: prop.ReplyTo,
                      basicProperties: reply, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                      multiple: false);
                }
            };
            channel.BasicConsume(queue: "rpc_queue", autoAck: true, consumer: consumer);
            Console.WriteLine(" Press enter to shutdown.");
            Console.ReadLine();
        }

        static async Task DownloadAsync(string path, string url)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode) throw new HttpRequestException();
                using (var fs = new FileStream(Path.Combine(path, Path.GetFileName(url)), FileMode.OpenOrCreate, FileAccess.Write))
                    await response.Content.CopyToAsync(fs);                
            }
            catch (IOException)
            { throw new IOException("Ошибка доступа к директории или директория не найдена"); }
            catch (ArgumentNullException)
            { throw new ArgumentNullException("Не все данные введены корректно введены"); }
            catch (HttpRequestException)
            { throw new HttpRequestException("Ресурс не существует или доступ ограничен"); }
            catch (Exception)
            { throw new Exception("Ошибка"); }
        }

        static string CreatePathAsync(JObject json)
        {
            
            var userFolder = json.SelectToken("login").ToString();
            var folder = json.SelectToken("folder").ToString();
            try
            {
                string baseFolder = Directory.GetCurrentDirectory();
                string path = Path.Combine(baseFolder, userFolder, folder);
                return path;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
        }

    }

    public class UploadFileMessage
    {
        public string Login { get; set; }
        public string Message { get; set; }
        public string BaseDirectory { get; set; }
    }
}
