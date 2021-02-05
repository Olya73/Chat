using Contract.Bot;
using Contract.Bot.Interface;
using Contract.ConfigurationModel;
using Contract.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class WeatherBot : ICommandBot
    {
        public string Name => "Weather";
        private readonly WeatherBotSettings _settings;

        public WeatherBot(IOptions<WeatherBotSettings> options)
        {
            _settings = options.Value;
        }
        public string OnCommand(ChatEventGetDTO chatEventGetDTO)
        {
            string api = _settings.KeyApi;
            string uri = _settings.Uri;

            string address = string.Format(uri, chatEventGetDTO.Message.Text, api);
            try
            {
                JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(address));
                return CreateString(response);
            }            
            catch(Exception)
            {
                return null;
            }            
        }

        protected string CreateString(JObject response)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(response.SelectToken("name"));
            stringBuilder.Append(", ");
            stringBuilder.Append(response.SelectToken("sys.country"));
            stringBuilder.Append(": ");
            stringBuilder.Append(response.SelectToken("main.temp"));
            stringBuilder.Append("c, ");
            stringBuilder.Append(response.SelectToken("weather[0].main"));

            return stringBuilder.ToString();
        }

        public int CommandExists(string comm)
        {
            throw new NotImplementedException();
        }
    }
}
