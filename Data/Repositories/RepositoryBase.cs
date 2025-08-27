using System;
using EFakturCallback.Data;
namespace EFakturCallback.Data.Repositories
{
	public class RepositoryBase
	{
		protected readonly ApiContext Db;
		public RepositoryBase(ApiContext context)
		{
			Db = context;
		}
	}
}

