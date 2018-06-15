using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Telpay.Business;

namespace Telpay.DAL
{
    public class CustomerRepository : Repository<Customer, int>, ICustomerRepository
    {
        private readonly DbSet<Customer> _dbSet;

        public CustomerRepository(BaseDbContext dbContext) : base(dbContext)
        {
            _dbSet = _dbContext.Set<Customer>();
        }

        public new List<Customer> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
