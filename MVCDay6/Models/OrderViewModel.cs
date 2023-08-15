namespace MVCDay6.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }

        public IEnumerable<OrderProduct> OrderProducts { get; set; }

    }
}
