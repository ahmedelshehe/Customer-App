using Microsoft.EntityFrameworkCore;
using MVCDay6.Models;

namespace MVCDay6.Repositories.OrderRepositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly Context context;

        public OrderRepository(Context context) : base(context)
        {
            this.context = context;
        }

        public void AddOrderProduct(int orderId, int productId, int quantity)
        {
            var orderproduct = context.OrderProducts.FirstOrDefault(o => o.ProductId == productId && o.OrderId == orderId);
            if (orderproduct != null)
            {
                orderproduct.Quantity += quantity;
                var order = GetOrderByIdWithProducts(orderId);
                order.OrderProducts.FirstOrDefault
                    (o => o.ProductId == orderproduct.ProductId && o.OrderId == orderproduct.OrderId)
                    .Quantity += quantity;
                context.SaveChanges();
            }
            else
            {
                var order = GetOrderByIdWithProducts(orderId);
                var product = context.Products.FirstOrDefault(p => p.Id == productId);
                order.OrderProducts.Add
                    (new OrderProduct()
                    { OrderId = orderId, Order = order, Product = product, ProductId = productId, Quantity = quantity });
                Update(order);
            }

        }
        public void DeleteOrderProduct(int orderId, int productId)
        {
            var orderproduct = context.OrderProducts.FirstOrDefault(o => o.ProductId == productId && o.OrderId == orderId);
            if (orderproduct != null)
            {
                var order = GetOrderByIdWithProducts(orderId);
                order.OrderProducts.Remove(orderproduct);
                context.OrderProducts.Remove(orderproduct);
                Update(order);
            }

        }

        public IEnumerable<OrderProduct> GetOrderProducts(int id)
        {
            return context.OrderProducts.Where(o => o.OrderId == id).Include("Product").ToList();
        }

        public IEnumerable<Order> GetAllOrdersWithCustomer()
        {
            return context.Orders.Include("Customer").ToList();
        }

        public IEnumerable<Order> GetCustomerOrders(int id)
        {
            return context.Orders.Where(o => o.CustID == id).Include(o => o.Customer).Include(o => o.OrderProducts).ToList();
        }

        public Order? GetOrderByIdWithCustomer(int id)
        {
            return context.Orders.Include("Customer").FirstOrDefault(o => o.ID == id);
        }

        public Order? GetOrderByIdWithProducts(int id)
        {
            return context.Orders.Include("OrderProducts").FirstOrDefault(o => o.ID == id);
        }

        public IEnumerable<Order> GetAllOrdersWithCustomerAndProducts()
        {
            return context.Orders.Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product).ToList();
        }

        public Order? GetOrderByIdWithProductsAndCustomer(int id)
        {
            return context.Orders.Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(o => o.Product).FirstOrDefault(o => o.ID == id);
        }
    }
}
