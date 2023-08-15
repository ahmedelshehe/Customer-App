using MVCDay6.Models;

namespace MVCDay6.Repositories.CustomerRepositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAllCustomersWithOrders();

        Customer? GetCustomerWithOrders(int id);


    }
}
