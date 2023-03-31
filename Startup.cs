using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;

namespace DotNetwithVue3
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
            services.AddSpaStaticFiles(configuration =>
            {
                // 20230330 Evan 網路上的解法，不確定有沒有用
                // configuration.RootPath = "ClientApp";
                configuration.RootPath = "ClientApp/dist";
            });
            // 20230329 Evan Swgger畫面設定
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Document",
                    Description = "API Document For Evan Lin",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Evan Lin",
                        Email = "evan_lin@orbit.com.tw"
                    }
                });
            });
            //
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 20230329 Evan 使用Swagger, SwaggerUI 方便測試 WebApi
            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "/api/swagger/{DocumentName}/swagger.json";
            //});
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "API Document V1");
            //    c.RoutePrefix = "api/swagger";
            //});
            //

            //app.UseSwagger(c => 
            //{
            //    c.RouteTemplate = "/swagger/swagger/{DocumentName}/swagger.json";
            //});
            //app.UseSwaggerUI(c =>
            //{
            //    // 未測試完
            //    c.SwaggerEndpoint("swagger/v1/swagger.json", "API Document V1");
            //    c.RoutePrefix = "swagger";
            //    // localhost:{port}/swagger/v1/swagger.json 有東西
            //    // localhost:{port}/swagger/index.html => 找 localhost:{port}/swagger/swagger/v1/swagger.json => 找不到

            //    //c.SwaggerEndpoint("api/swagger/v1/swagger.json", "API Document V1");
            //    // localhost:{port}/swagger/v1/swagger.json 有東西
            //    // localhost:{port}/swagger/index.html => 找 localhost:{port}/api/swagger/v1/swagger.json => 找不到
            //});

            // 20230331 Evan 沒有錯誤版本
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API Document V1");
                // localhost:{port}/swagger/v1/swagger.json 有東西
                // localhost:{port}/swagger/index.html => 找 localhost:{port}/swagger/v1/swagger.json => 找得到
            });
            //

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }

            });
        }
    }
}
