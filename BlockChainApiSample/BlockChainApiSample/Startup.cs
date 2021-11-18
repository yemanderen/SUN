using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainApiSample
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlockChainApiSample", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlockChainApiSample v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Map("/BlockChain", _ =>
            {
                _.Run(async context =>
                {
                    if (context.Request.Method == "POST")
                    {
                        // Ôö¼ÓÇø¿éÁ´
                        if (BlockGenerator._blockChain.Count == 0)
                        {
                            Block firstBlock = new Block()
                            {
                                Index = 0,
                                TimeStamp = BlockGenerator.CalculateCurrentTimeUTC(),
                                BPM = 0,
                                Hash = string.Empty,
                                PrevHash = string.Empty
                            };
                            BlockGenerator._blockChain.Add(firstBlock);
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(firstBlock));
                        }
                        else
                        {
                            int.TryParse(context.Request.Form["BPM"][0], out int bpm);
                            Block oldBlock = BlockGenerator._blockChain.Last();
                            Block newBlock = BlockGenerator.GenerateBlock(oldBlock, bpm);
                            if (BlockGenerator.IsBlockValid(newBlock, oldBlock))
                            {
                                List<Block> newBlockChain = new List<Block>();
                                foreach (var block in BlockGenerator._blockChain)
                                {
                                    newBlockChain.Add(block);
                                }
                                newBlockChain.Add(newBlock);
                                BlockGenerator.SwitchChain(newBlockChain);
                            }
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(newBlock));
                        }
                    }
                });
            });
            app.Map("/BlockChains", _ =>
            {
                _.Run(async context =>
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(BlockGenerator._blockChain));
                });
            });
        }
    }
}
