using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Telpay.Business;
using Microsoft.AspNetCore.Authorization;
using Telpay.Business.Models;
using Telpay.Business.Managers;

namespace Telpay.API.Controllers
{
    [Produces("application/json")]
    [Route("api/customer")]
    [Authorize]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager customerManager;

        public CustomerController(ICustomerManager Manager)
        {
            customerManager = Manager;
        }

        [HttpGet]
        public IList<Customer> GetCustomers()
        {
            return customerManager.GetCustomers();
        }

        [HttpPost]
        public SaveStatus Save([FromBody] Customer customer)
        {
            return customerManager.Save(customer);
        }

        [HttpPut("{id}")]
        public SaveStatus Put([FromRoute] int id, [FromBody] Customer customer)
        {
            return customerManager.Update(id, customer);
        }

        [HttpDelete("{id}")]
        public SaveStatus Delete(int id)
        {
            return customerManager.Delete(id);
        }
    }
}
