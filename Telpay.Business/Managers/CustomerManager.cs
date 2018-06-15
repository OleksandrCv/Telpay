using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telpay.Business.Models;

namespace Telpay.Business.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private ICustomerRepository repository;

        public CustomerManager(ICustomerRepository CustomerRepo) {
            repository = CustomerRepo; 
        }

        public IList<Customer> GetCustomers() {
            return repository.GetAll();
        }

        public SaveStatus Save(Customer customer) {
            var status = new SaveStatus();
            if (customer != null && customer.Id == 0)
            {
                try
                {
                    repository.Save(customer);
                    repository.Commit();
                } catch(Exception ex)
                {
                    status.Successful = false;
                    status.Message = ex.Message;
                }
            }
            else {
                status.Successful = false;
                status.Message = "Please enter valid customer data";
            }

            return status;
        }

        public SaveStatus Update(int id, Customer customer) {
            var status = new SaveStatus();
            if (id == customer.Id && customer.Id != 0)
            {
                try
                {
                    repository.Update(customer);
                    repository.Commit();
                }
                catch (Exception ex)
                {
                    status.Successful = false;
                    status.Message = ex.Message;
                }
            }
            else {
                status.Successful = false;
                status.Message = "Request was wrong, please try one more time";
            }
            return status;

        }
        public SaveStatus Delete(int id) {
            var status = new SaveStatus();
            if (id != 0)
            {
                try
                {
                    repository.Delete(id);
                    repository.Commit();
                }
                catch (Exception ex)
                {
                    status.Successful = false;
                    status.Message = ex.Message;
                }
            } else {
                status.Successful = false;
                status.Message = "Id of the customer can't be 0";
            }
            return status;
        }
    }
}
