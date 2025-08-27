using System;
using EFakturCallback.Configuration;
using EFakturCallback.Data;
using Microsoft.EntityFrameworkCore;
namespace EFakturCallback.Extensions
{
	public static class DatabaseExtensions
	{
		public static void AddDatabaseConfiguration(this IServiceCollection services, GeneralConfiguration config)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<ApiContext>(options =>
				options.UseSqlServer(config.DbConnection.Default)
			);
		}
	}
}

