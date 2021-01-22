using BotLibrary.Implementation;
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
            var bots = botService.GetAllWithTypeAsync().Result;

            var serviceCollection = new ServiceCollection();
            string cuurentNamespace = "BotLibrary.Implementation";
            foreach (var bot in bots)
            {
                string typename = "Service.Implementation.BotService"; //$"{cuurentNamespace}.{bot.Implementation}";
                var impl = Assembly.Load("BotLibrary").GetType($"BotLibrary.Implementation.{bot.Implementation}");//GetExecutingAssembly().GetType($"{typename}");
                foreach (var typ in bot.BotTypes)
                {
                    Type interf = Assembly.Load("BotLibrary").GetType($"BotLibrary.Inteface.{typ.TypeOfBot.Inteface}");
                    var f = interf.IsAssignableFrom(impl);//
                    if (interf.IsAssignableFrom(impl))
                    {
                        var obj = Activator.CreateInstance(impl);
                        serviceCollection.AddTransient(interf, f => obj);
                    }
                }
            };
            return new BotManager(serviceCollection.BuildServiceProvider());
        }
    }
}
