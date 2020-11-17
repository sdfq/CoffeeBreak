using System.ComponentModel.DataAnnotations;

namespace CoffeeBreak.Models
{
    public class SignUpViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CellPhone { get; set; }
    }
}
