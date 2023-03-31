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
                // 20230330 Evan �����W���Ѫk�A���T�w���S����
                // configuration.RootPath = "ClientApp";
                configuration.RootPath = "ClientApp/dist";
            });
            // 20230329 Evan Swgger�e���]�w
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

            // 20230329 Evan �ϥ�Swagger, SwaggerUI ��K���� WebApi
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
            //    // �����է�
            //    c.SwaggerEndpoint("swagger/v1/swagger.json", "API Document V1");
            //    c.RoutePrefix = "swagger";
            //    // localhost:{port}/swagger/v1/swagger.json ���F��
            //    // localhost:{port}/swagger/index.html => �� localhost:{port}/swagger/swagger/v1/swagger.json => �䤣��

            //    //c.SwaggerEndpoint("api/swagger/v1/swagger.json", "API Document V1");
            //    // localhost:{port}/swagger/v1/swagger.json ���F��
            //    // localhost:{port}/swagger/index.html => �� localhost:{port}/api/swagger/v1/swagger.json => �䤣��
            //});

            // 20230331 Evan �S�����~����
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "API Document V1");
                // localhost:{port}/swagger/v1/swagger.json ���F��
                // localhost:{port}/swagger/index.html => �� localhost:{port}/swagger/v1/swagger.json => ��o��
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
