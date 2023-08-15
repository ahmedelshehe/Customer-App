namespace MVCDay6.Models
{
    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    }
}
