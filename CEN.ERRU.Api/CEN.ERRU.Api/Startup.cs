using CEN.ERRU.Api.Service;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SoapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CEN.ERRU.Api
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
            services.AddSoapCore();
            services.AddScoped<IErruService, ErruService>();
            services.AddScoped<Contract.ERRU, BizTalkService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CEN.ERRU.Api", Version = "v1" });
            });

            services.AddAuthentication(
            CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate(options =>
            {
                options.RevocationMode = X509RevocationMode.NoCheck;
                options.AllowedCertificateTypes = CertificateTypes.All;
                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        if (context.ClientCertificate.Thumbprint == "DEF55A8AF7211129CC7F11828083BF6B9A7E3928" ||
                            context.ClientCertificate.Thumbprint == "0B894EEF9583083644CD2A9789DE41E522754C39")
                        {
                            context.Success();
                        }
                        else
                        {
                            context.Fail("Niepoprawny certyfikat.");
                        }


                        return Task.CompletedTask;
                    }
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CEN.ERRU.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCertificateForwarding();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.UseSoapEndpoint<Contract.ERRU>("/ErruService.svc", new SoapEncoderOptions(), SoapSerializer.XmlSerializer).RequireAuthorization();
            });
        }
    }
}
