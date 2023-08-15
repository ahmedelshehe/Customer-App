using MVCDay6.Models;

namespace MVCDay6.Repositories.OrderRepositories
{
	public interface IOrderRepository : IRepository<Order>
	{
		IEnumerable<Order> GetAllOrdersWithCustomer();
		IEnumerable<Order> GetCustomerOrders(int id);

		Order? GetOrderByIdWithCustomer(int id);
		Order? GetOrderByIdWithProducts(int id);
		Order? GetOrderByIdWithProductsAndCustomer(int id);

		void AddOrderProduct(int orderId, int productId, int quantity);
		void DeleteOrderProduct(int orderId, int productId);

		IEnumerable<OrderProduct> GetOrderProducts(int id);
		IEnumerable<Order> GetAllOrdersWithCustomerAndProducts();
	}
}
