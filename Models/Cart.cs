using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeterEngland2.Models
{
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductName { get; set; }

        public int ProductQuntity { get; set; }

        public decimal ProductTotalBill { get; set; }

        public string ProductImageUrl { get; set; }
    }
}
