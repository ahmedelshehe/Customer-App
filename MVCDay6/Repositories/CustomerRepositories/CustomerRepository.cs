using Microsoft.EntityFrameworkCore;
using MVCDay6.Models;

namespace MVCDay6.Repositories.CustomerRepositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly Context context;

        public CustomerRepository(Context context) : base(context)
        {
            this.context = context;
        }


        public IEnumerable<Customer> GetAllCustomersWithOrders()
        {
            return context.Customers.Include("Orders").ToList();
        }

        public Customer? GetCustomerWithOrders(int id)
        {
            return context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefault(c => c.ID == id);
        }
    }
}
