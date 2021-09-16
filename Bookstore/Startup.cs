using Bookstore.Infrastructure.Data.Context;
using Bookstore.Repository;
using Bookstore.Security.Credentials;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace Bookstore
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
            services.AddCors();

            services.AddDbContext<AppDbContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(
              JwtBearerDefaults.AuthenticationScheme).
              AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidAudience = Credentials.Audience,
                     ValidIssuer = Credentials.Issuer,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(Credentials.Key))
                 });

            services.AddSwaggerGen(c =>
            {
                 c.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Version = "v1",
                     Title = "Application BookStore",
                     Description = "BookStore API",
                     TermsOfService = new Uri("http://pluralbits.com"),
                     Contact = new OpenApiContact
                     {
                         Name = "Tadeu",
                         Email = "tadeu.matos@gmail.com",
                         Url = new Uri("http://pluralbits.com"),
                     },
                     License = new OpenApiLicense
                     {
                         Name = "User to research",
                         Url = new Uri("http://pluralbits.com"),
                     }
                 });

                 OpenApiSecurityScheme securityDefinition = new OpenApiSecurityScheme()
                 {
                     Name = "Bearer",
                     BearerFormat = "JWT",
                     Scheme = "bearer",
                     Description = "Specify the authorization token.",
                     In = ParameterLocation.Header,
                     Type = SecuritySchemeType.Http,
                 };
                 c.AddSecurityDefinition("jwt_auth", securityDefinition);

                 // Make sure swagger UI requires a Bearer token specified
                 OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                 {
                     Reference = new OpenApiReference()
                     {
                         Id = "jwt_auth",
                         Type = ReferenceType.SecurityScheme
                     }
                 };
                 OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
                 {
                 {
                   securityScheme, new string[] { }},
                 };
                 c.AddSecurityRequirement(securityRequirements);

            });

            services.AddControllers()
                    .AddNewtonsoftJson(Options =>
                    {
                        Options.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

           
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
