using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PeterEngland2.Models
{
    [Table("Ordered")]
    public class Ordered
    {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductName { get; set; }

        public int ProductQuntity { get; set; }

        public decimal ProductTotalBill { get; set; }

        public string ProductImageUrl { get; set; }
    }
}
