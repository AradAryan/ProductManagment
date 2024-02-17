using Application.Common.Commands;
using Application.Common.Handlers;
using Application.Dtos;
using Application.Mapper;
using Application.Services.Implementations;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.IdentityEntities;
using Infrastructure.DbContexts;
using Infrastructure.Repositories.Implementations;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Filters;
using System.Text;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ProductAuthorizationFilter>();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection")));
            builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Configure Customize password requirements, lockout settings, etc.
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddScoped<IUserAppService, UserAppService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<ICommandHandler<CreateProductCommand, bool>, CreateProductCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<UpdateProductCommand, bool>, UpdateProductCommandHandler>();
            builder.Services.AddScoped<ICommandHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
            builder.Services.AddScoped<IQueryHandler<GetProductByIdQuery, ProductDto>, GetProductByIdQueryHandler>();
            builder.Services.AddScoped<IQueryHandler<GetAllProductsQuery, List<ProductDto>>, GetAllProductQueryHandler>();


            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Configure JWT authentication
            var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
            var jwtIssuer = builder.Configuration["Jwt:Issuer"];
            var jwtAudience = builder.Configuration["Jwt:Audience"];

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                    };
                });
            builder.Services.AddAuthorization();

            var app = builder.Build();

            using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                var identityContext = serviceScope?.ServiceProvider.GetRequiredService<IdentityDbContext>();
                identityContext?.Database.Migrate();

                var applicationContext = serviceScope?.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                applicationContext?.Database.Migrate();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
