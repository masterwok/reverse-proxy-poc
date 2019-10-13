using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ReverseProxyPoC.Clients;
using ReverseProxyPoC.Clients.Contracts;
using ReverseProxyPoC.Constants;
using ReverseProxyPoC.Filters;

namespace ReverseProxyPoC
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(ServiceDescriptor.Transient<IHttpClientWrapper, HttpClientWrapper>());

            services.Configure<Config>(_configuration.GetSection("Config"));

            services.Add(ServiceDescriptor.Transient<IHttpProxyClient, RestfulApiProxyClient>(provider =>
            {
                var config = provider.GetService<IOptions<Config>>().Value;

                return new RestfulApiProxyClient(
                    provider.GetService<IHttpClientWrapper>()
                    , config.ProxiedApiBaseUrl
                );
            }));

            services.AddControllers(options => options.Filters.Add<ProxyFilter>());
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}