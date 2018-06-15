using System.Collections.Generic;
using System.Threading.Tasks;
using Telpay.Business.Models;

namespace Telpay.Business.Managers
{
    public interface ICustomerManager
    {
        IList<Customer>GetCustomers();
        SaveStatus Save(Customer customer);
        SaveStatus Update(int id, Customer customer);
        SaveStatus Delete(int id);
    }
}
