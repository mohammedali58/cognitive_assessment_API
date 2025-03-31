
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using Infrastructure.Repositories;
using Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Npgsql;
using APP.Infrastructure.Seeding;
using APP.Middlewares;
using APP.Application.Common;

namespace APP
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddDbContext<AppDbContext>(opt => {

                //Console.WriteLine("////////////////" +  builder.Configuration.GetConnectionString("DefaultConnection")  );
				opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IJournalRepository, JournalRepository>();
			builder.Services.AddScoped<IWordRepository, WordRepository>();
			builder.Services.AddScoped<IJournalScoringService, JournalScoringService>();

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
											.AddJwtBearer(options =>
											{
												options.TokenValidationParameters = new TokenValidationParameters
												{
													ValidateIssuer = false,
													ValidateAudience = false,
													ValidateLifetime = false,
													ValidateIssuerSigningKey = false,
													ValidIssuer = builder.Configuration["JWT_Issuer"],
													ValidAudience = builder.Configuration["JWT_Audience"],
													IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT_Key"]!))
												};
											});

			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter your token 'Bearer' will be automatically added with a space"
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
			});

			var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                var config = builder.Configuration;
                var connectionString = config.GetConnectionString("DefaultConnection");
                DbInitializer.EnsureDatabaseAndMigrate(scope.ServiceProvider, connectionString);
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
