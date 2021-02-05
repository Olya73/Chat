using BotLibrary.Implementation;
using Contract.ConfigurationModel;
using DataAccess.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using System;
using System.Linq;
using Microsoft.Extensions.Options;
using System.Reflection;
using Repository.Storage.Interface;
using Repository.Storage.Implementation;
using Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class BotLoader
    {
        private static ServiceCollection serviceCollection;
        public static BotManager Load(IServiceProvider baseServiceProvider)
        {
            using var scope = baseServiceProvider.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            serviceCollection = new ServiceCollection();

            AddBotsToServices(scope);

            IConfigurationSection downloaderBotSettings = config.GetSection("Bots:Downloader");
            IConfigurationSection weatherBotSettings = config.GetSection("Bots:Weather");
            serviceCollection.Configure<WeatherBotSettings>(options => weatherBotSettings.Bind(options));
            serviceCollection.Configure<DownloaderBotSettings>(options => downloaderBotSettings.Bind(options)); //вынести в отдельный метод?

            //serviceCollection.AddScoped(c => config);

            return new BotManager(serviceCollection.BuildServiceProvider());
        }

        private static void AddBotsToServices(IServiceScope scope)
        {
            var botService = scope.ServiceProvider.GetRequiredService<IBotRepository>();
            Bot[] bots = botService.GetAllWithTypeAsync().Result;
            Type[] botLibrary = Assembly.Load(nameof(BotLibrary)).GetTypes();
            Type[] contract = Assembly.Load(nameof(Contract)).GetTypes();

            foreach (var bot in bots)
            {
                Type concreteBot = botLibrary.SingleOrDefault(n => n.Name == bot.Implementation);
                foreach (var botType in bot.BotTypes)
                {
                    Type interf = contract.SingleOrDefault(n => n.Name == botType.TypeOfBot.Inteface);
                    
                    bool f = interf.IsAssignableFrom(concreteBot);
                    if (interf.IsAssignableFrom(concreteBot))
                    {
                        serviceCollection.AddScoped(interf, concreteBot);
                    }
                }
            };
        }
    }
}
