// WpfWebApi.Startup.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

namespace WpfWebApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Console.WriteLine("Startup.cs");
        }

        public IConfiguration Configuration { get; }

        // 서비스를 컨테이너에 추가하는 메서드. 컨테이너는 애플리케이션의 모든 서비스를 보유하고 있으며 필요한 서비스를 가져올 수 있음.
        // 런타임에 호출되며 서비스를 등록함.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // 컨트롤러 서비스 추가
            // Add other services like DbContext, Identity, etc.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WpfWebApi",
                    Version = "v1"
                });
            });
        }

        // 애플리케이션의 HTTP 요청 처리 파이프라인을 구성하는 메서드. 런타임에 호출되며 HTTP 요청을 처리하는 방법을 지정함.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // 미들웨어를 사용하여 Swagger JSON 엔드포인트를 제공
            app.UseSwagger();
            // 미들웨어를 사용하여 SwaggerUI를 JSON 엔드포인트로 제공
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WpfWebApi v1");
                c.RoutePrefix = "swagger";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
