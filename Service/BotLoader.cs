using BotLibrary.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Service
{
    public class BotLoader
    {
        public static BotManager Load(IServiceProvider baseServiceProvider)
        {
            using var scope = baseServiceProvider.CreateScope();
            var botService = scope.ServiceProvider.GetRequiredService<IBotService>();
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var bots = botService.GetAllWithTypeAsync().Result;

            var serviceCollection = new ServiceCollection();
            foreach (var bot in bots)
            {                
                var impl = Assembly.Load("BotLibrary").GetType($"BotLibrary.Implementation.{bot.Implementation}");
                foreach (var typ in bot.BotTypes)
                {
                    Type interf = Assembly.Load("Contract").GetType($"Contract.Bot.Interface.{typ.TypeOfBot.Inteface}");
                    var f = interf.IsAssignableFrom(impl);//
                    if (interf.IsAssignableFrom(impl))
                    {
                        serviceCollection.AddScoped(interf, impl);
                    }
                }
            };
            serviceCollection.AddScoped(c => config);
            return new BotManager(serviceCollection.BuildServiceProvider());
        }
    }
}
