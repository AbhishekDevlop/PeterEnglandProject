using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
namespace PeterEngland2.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        
        public int UserId { get; set; }
        [Required(ErrorMessage = "Field required")]

        [RegularExpression( @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
       
        //validation
        [Required(ErrorMessage ="Field required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field required")]
        public string  UserName { get; set; }
        public int RoleId { get; set; } 
    }
}
