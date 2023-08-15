using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDay6.Models
{
    [PrimaryKey("OrderId", "ProductId")]
    public class OrderProduct
    {

        [Column(Order = 0)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        [NotMapped]
        public double TotalPrice
        {
            get
            {
                if (Product == null)
                    return 0;
                return Product.Price * Quantity;
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Quantity { get; set; }

    }


}
