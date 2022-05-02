using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PeterEngland2.Models
{
    [Table("Inventry")]
    public class Inventry
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Filed Required")]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Filed Required")]
        [Display(Name = "CategoryName")]
        public int CategoryId { get; set; }
        [NotMapped]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Filed Required")]
        public decimal ProductPrice { get; set; }
        
        public string ProductImageUrl { get; set; }

        [NotMapped]

        public IFormFile ImageFile { get; set; }



    }
}
