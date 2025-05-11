using DomainLayer.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.Identity;
using StackExchange.Redis;
using Talabat.API.Extentions;
using Talabat.API.MiddelWears;

namespace Talabat.API
{
    public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Sevicies
			// Add services to the container.

			builder.Services.AddControllers();

			builder.Services.AddDbContext<StoreContext>(
				option =>
				{
					option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				});
			builder.Services.AddDbContext<AppIdentityDbContext>(BuilderOption=>
			{
				BuilderOption.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
				var ConnectionString = builder.Configuration.GetConnectionString("Redis");
             return ConnectionMultiplexer.Connect(ConnectionString);
            });

            builder.Services.AddApplicationServicers();

			builder.Services.AddSecuritySevices(builder.Configuration);

			builder.Services.AddSwaggerServices();
			
			#endregion
			

			var app = builder.Build();

			#region Explicitly Injection
			using var scope = app.Services.CreateScope();
			var Services = scope.ServiceProvider;
			// ask CLR to create object from DbContext Explicitly 
			var _dbContext = Services.GetRequiredService<StoreContext>();
			var _dbIdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
            var LoggerFactory=Services.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
				await StoreContextSeed.SeedAsync(_dbContext);

				await _dbIdentityContext.Database.MigrateAsync();
				var _usermanger = Services.GetRequiredService<UserManager<AppUser>>();

                await AppIdentityDbContextSeed.SeedUserAsync(_usermanger);

			}
			catch (Exception Ex)
			{

				var logger=LoggerFactory.CreateLogger<Program>();
				logger.LogError(Ex, "an error has been occurred during apply the Migration");
			}			 
			#endregion

			app.UseMiddleware<ServerMiddelWear>();

			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			// Configure the HTTP request pipeline.
		
		    app.UserSwagger();

			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}



