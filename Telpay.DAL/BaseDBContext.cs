using Microsoft.EntityFrameworkCore;
using Telpay.Business;

namespace Telpay.DAL
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>();
        }
    }
}
