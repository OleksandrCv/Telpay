using System.Collections.Generic;

namespace Telpay.Business
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {
        new List<Customer> GetAll();
    }
}