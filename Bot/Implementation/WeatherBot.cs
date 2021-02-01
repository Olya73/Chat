using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotLibrary.Implementation
{
    class WeatherBot : ICommandBot
    {
        public string Name => "Weather";
        private readonly IConfiguration _configuration;

        public WeatherBot(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string OnCommand(MessageGetDTO messageGetDTO)
        {
            string api = GetApiKey();

            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", messageGetDTO.Text, api);
            try
            {
                JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(url));
                return CreateString(response);
            }            
            catch(Exception)
            {
                return null;
            }            
        }

        protected string GetApiKey()
        {
            return _configuration[$"Bots:{Name}:keyApi"];
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
