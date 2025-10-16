using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using pow_project.Server.Models;
using System.Text;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace pow_project.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            var cs = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' no está configurada.");

            builder.Services.AddDbContext<MyDBContext>(options =>
                options.UseMySql(cs, ServerVersion.AutoDetect(cs)));

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MyDBContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var jwtSecret = builder.Configuration["Jwt:Secret"]
                ?? throw new InvalidOperationException("Jwt:Secret no está configurado en appsettings.");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                    IssuerSigningKey = signingKey
                };
            });

            var app = builder.Build();

            app.UseDefaultFiles();
            app.MapStaticAssets();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi().AllowAnonymous();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "pow-project v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapFallbackToFile("/index.html");
            app.Run();
        }
    }
}
