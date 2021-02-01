using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BotLibrary;
using BotLibrary.Implementation;
using Contract.Bot.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Storage.Implementation;
using Repository.Storage.Interface;
using Service;
using Service.Implementation;
using Service.Interface;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            
            services.AddDbContextPool<ChatNpgSQLContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("AspPostgreSQLContext"));
                options.UseLoggerFactory(LoggerFactory.Create(buider => buider.AddConsole()));
            }
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped(typeof(IDialogRepository), typeof(DialogRepository));
            services.AddScoped(typeof(IMessageRepository), typeof(MessageRepository));
            services.AddScoped(typeof(IDialogService), typeof(DialogService));
            services.AddScoped(typeof(IMessageService), typeof(MessageService));
            services.AddScoped(typeof(IBotRepository), typeof(BotRepository));
            services.AddScoped(typeof(IBotService), typeof(BotService));
            services.AddScoped(typeof(IBotActionOnEventRepository), typeof(BotActionOnEventRepository));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IChatEventRepository), typeof(ChatEventRepository));
            services.AddScoped(typeof(IChatActionService), typeof(ChatActionService));

            services.AddSingleton<IBotManager>(BotLoader.Load);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
