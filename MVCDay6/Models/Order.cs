using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDay6.Models
{
    public class Order
    {
        [Key]
        [DisplayName("Order ID")]
        public int ID { get; set; }

        [DisplayName("Order Date")]
        [PastDate(ErrorMessage = "Date cannot be in the future")]
        public DateTime? Date { get; set; } = DateTime.Now;

        [DisplayName("Total Price")]
        [Required(ErrorMessage = "Total Price Can't Be Empty")]
        [TotalPrice]
        public double? TotalPrice { get; set; }

        [ForeignKey("Customer")]
        public int CustID { get; set; }
        public Customer? Customer { get; set; }

        public virtual ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
