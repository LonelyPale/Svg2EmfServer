using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Serialization;

namespace SNWDSafeMonitor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddApiDoc(t => t.ApiDocPath = "apidoc");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("apidoc", new Info { Title = "NSBDAQJCAPI", Version = "v1" });
            });

            services.AddHttpClient(); //注册 HttpClientFactory

            //services.AddErrorCheckService();
            //services.AddMultiloggerService();
            //services.AddCurrentDataAnaService();
            //services.AddNewDataAnalysisService();
            //services.AddDataAnaService();
            //services.AddStatModelCalcService();
            //services.AddMySqlTransService();


            //  services.AddResponseCompression();

            services.AddMvc()
           .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
                
            });

            //Jil序列化工具对int?等类型处理出错，放弃使用
            //services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2)
            //    .AddMvcOptions(opts =>
            //    {
            //        // 使用 Jil 替换默认的 JSON 解析
            //        opts.InputFormatters.Clear();
            //        opts.InputFormatters.Add(new MyInputFormatter());
            //        opts.OutputFormatters.Clear();
            //        opts.OutputFormatters.Add(new MyOutputFormatter());
            //    });

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        var serverSecret = new SymmetricSecurityKey(
            //            Encoding.UTF8.GetBytes(Configuration["JWT:ServerSecret"]));
            //        options.TokenValidationParameters = new TokenValidationParameters()
            //        {
            //            IssuerSigningKey = serverSecret,
            //            ValidIssuer = Configuration["JWT:Issuer"],
            //            ValidAudience = Configuration["JWT:Audience"]
            //        };

            //    });
            services.AddResponseCaching();


            // services.AddSingleton<IRedisRepository, RedisRepositoryImpl>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();

            //}


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!\nother line");
            //});
            //app.UseMvc();
            //app.UseAuthentication();
            //app.UseResponseCaching();
            //  app.UseHttpCacheHeaders();
            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), "grf")),
            //    RequestPath = "/grf"
            //});
            // app.UseResponseCompression();    //默认使用gzip压缩



            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.InjectJavascript("/swagger/ui/zh_CN.js"); // 加载中文包
                options.SwaggerEndpoint("/swagger/apidoc/swagger.json", "NSBD-AQJC-API V1");
            });
        }

    }
}
