using Microsoft.EntityFrameworkCore;
namespace EFakturCallback.Data
{
	public class ApiContext: DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options) : base (options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

