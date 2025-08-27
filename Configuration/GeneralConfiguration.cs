using System;
namespace EFakturCallback.Configuration
{
	public class GeneralConfiguration
	{
		public DbConnection DbConnection { get; set; }
		public string ApiKey { get; set; }
	}

	public class DbConnection
	{
		public string? Default { get; set; }
	}
}

