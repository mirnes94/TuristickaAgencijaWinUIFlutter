using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.WebAPI.Database;
using TuristickaAgencija.WebAPI.Filter;
using TuristickaAgencija.WebAPI.Security;
using TuristickaAgencija.WebAPI.Services.Drzava;
using TuristickaAgencija.WebAPI.Services.Firma;
using TuristickaAgencija.WebAPI.Services.Gradovi;
using TuristickaAgencija.WebAPI.Services.Komentar;
using TuristickaAgencija.WebAPI.Services.Korisnici;
using TuristickaAgencija.WebAPI.Services.ListaZelja;
using TuristickaAgencija.WebAPI.Services.Obavijesti;
using TuristickaAgencija.WebAPI.Services.Ocjene;
using TuristickaAgencija.WebAPI.Services.Prevoz;
using TuristickaAgencija.WebAPI.Services.Putovanja;
using TuristickaAgencija.WebAPI.Services.RecommenderService;
using TuristickaAgencija.WebAPI.Services.Rezervacija;
using TuristickaAgencija.WebAPI.Services.Smjestaj;
using TuristickaAgencija.WebAPI.Services.Uloge;
using TuristickaAgencija.WebAPI.Services.Uplate;
using TuristickaAgencija.WebAPI.Services.Vodic;

namespace TuristickaAgencija.WebAPI
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
            services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
                     builder =>
                     {
                         builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader();
                     }));

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });

                
            });
            
            services.AddScoped<IDrzavaService, DrzavaService>();
            services.AddScoped<IGradoviService, GradoviService>();
        
            services.AddScoped<IFirmaService, FirmaService>();
            services.AddScoped<IPrevozService, PrevozService>();
            services.AddScoped<ISmjestajService, SmjestajService>();
            services.AddScoped<IPutovanjaService, PutovanjaService>();
            services.AddScoped<IKorisniciService, KorisniciService>();
            services.AddScoped<IUlogeService, UlogeService>();
            services.AddScoped<IRezervacijaService, RezervacijaService>();
            services.AddScoped<IUplateService, UplateService>();
            services.AddScoped<IKomentarService, KomentarService>();
            services.AddScoped<IOcjeneService, OcjeneService>();
            services.AddScoped<IObavijestiService, ObavijestiService>();
            services.AddScoped<IVodicService, VodicService>();
            services.AddScoped<IListaZeljaService, ListaZeljaService>();
            services.AddScoped<IRecommenderService, RecommenderService>();
            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddControllers();
           
            /*services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );*/

            services.AddAutoMapper();

            services.AddMvc(x => x.Filters.Add<ErrorFilter>());

            //var connection = @"Server=.;Database=TuristickaAgencija;Trusted_Connection=True;ConnectRetryCount=0";
            //services.AddDbContext<TuristickaAgencijaContext>(options => options.UseSqlServer(connection));

            var connection = Configuration.GetConnectionString("TuristickaAgencija");
            //services.AddDbContext<TuristickaAgencijaContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<TuristickaAgencijaContext>(options => {
                options.UseSqlServer(connection);
                options.EnableSensitiveDataLogging(true);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAnyOrigin");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                
            });

            //app.UseHttpsRedirection();
            // app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
