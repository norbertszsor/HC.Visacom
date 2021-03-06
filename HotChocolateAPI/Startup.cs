using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolateAPI.Entities;
using HotChocolateAPI.Services;
using Microsoft.AspNetCore.Identity;
using HotChocolateAPI.Models.Validators;
using FluentValidation;
using HotChocolateAPI.Models;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotChocolateAPI.Middleware;
using AutoMapper;
using HotChocolateAPI.Models.DTO;
using Microsoft.OpenApi.Models;
using HotChocolateAPI.Models.Query;

namespace HotChocolateAPI
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
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssurl,
                    ValidAudience = authenticationSettings.JwtIssurl,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gr2 UWM API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
                });
            });
            services.AddControllers().AddFluentValidation(); ;
            services.AddDbContext<HotChocolateDbContext>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<HotChocolateSeeder>();

            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IBlogSerivce, BlogService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<CreateAccountDto>, CreateAccountDtoValidator>();
            services.AddScoped<IValidator<NewPasswordDto>, NewPasswordDtoValidator>();
            services.AddScoped<IValidator<ManageAccountDto>, ManageAccountDtoValidator>();
            services.AddScoped<IValidator<OrderStatusDto>, OrderStatusDtoValidator>();
            services.AddScoped<IValidator<OpininDto>, OpinionDtoValidator>();
            services.AddScoped<IValidator<(CreateOrderDto,Order)>, CreateOrderDtoValidator>();
            services.AddScoped<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();
            services.AddScoped<IValidator<UpdateDetailsDto>, UpdateDetailsValidator>();
            services.AddScoped<IValidator<AddressDto>, AdressDtoValidator>();
            services.AddScoped<IValidator<ProductQuery>, ProductQueryValidator>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HotChocolateSeeder seeder)
        {
            
            app.UseCors("CorsPolicy");
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            
            app.UseSwagger(); //endpointy w swaggerze
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotChocolate API");
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
