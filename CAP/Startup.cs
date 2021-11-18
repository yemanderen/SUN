using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Savorboard.CAP.InMemoryMessageQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAP
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CAP", Version = "v1" });
            });

            //services.AddDbContext<AppDbContext>(); //Options, If you are using EF as the ORM
            //services.AddSingleton<IMongoClient>(new MongoClient("")); //Options, If you are using MongoDB
            //Note: The injection of services needs before of `services.AddCap()`
            //services.AddTransient<ISubscriberService, SubscriberService>();

            services.AddCap(x =>
            {
                x.UseInMemoryStorage();
                x.UseInMemoryMessageQueue();

                //// If you are using EF, you need to add the configuration£º
                ////x.UseEntityFramework<AppDbContext>(); //Options, Notice: You don't need to config x.UseSqlServer(""") again! CAP can autodiscovery.

                //// If you are using ADO.NET, choose to add configuration you needed£º
                //x.UseSqlServer("Your ConnectionStrings");
                //x.UseMySql("Your ConnectionStrings");
                //x.UsePostgreSql("Your ConnectionStrings");

                //// If you are using MongoDB, you need to add the configuration£º
                //x.UseMongoDB("Your ConnectionStrings");  //MongoDB 4.0+ cluster

                //// CAP support RabbitMQ,Kafka,AzureService as the MQ, choose to add configuration you needed£º
                //x.UseRabbitMQ("ConnectionString");
                //x.UseKafka("ConnectionString");
                //x.UseAzureServiceBus("ConnectionString");
                //x.UseAmazonSQS(Amazon.RegionEndpoint.APEast1);

                //// Register Dashboard
                //x.UseDashboard();

                //// Register to Consul
                //x.UseDiscovery(d =>
                //{
                //    d.DiscoveryServerHostName = "localhost";
                //    d.DiscoveryServerPort = 8500;
                //    d.CurrentNodeHostName = "localhost";
                //    d.CurrentNodePort = 5800;
                //    d.NodeId = 1;
                //    d.NodeName = "CAP No.1 Node";
                //});

                //x.DefaultGroupName = "default-group-name";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAP v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
