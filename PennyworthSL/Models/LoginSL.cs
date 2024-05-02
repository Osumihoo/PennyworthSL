using System.ComponentModel.DataAnnotations;

namespace PennyworthSL.Models
{
    public class LoginSL
    {
        [Required]
        //public string CompanyDB { get; set; } = "TESTREP3";
        public string CompanyDB { get; set; } = "SB1CSL";
        [Required]
        public string UserName { get; set; } = "Desarrollo";
        [Required]
        public string Password { get; set; } = "123456";
    }
}
