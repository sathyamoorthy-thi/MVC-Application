using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ReimbursementClaim
{
    public class UserCredentials
    {
        [StringLength(15)]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [MaxLength(25)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z]+\.[a-z]{2,4}", ErrorMessage = "Enter a correct email address")]
        public string emailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,13}$", ErrorMessage = "Enter a valid password")]
        public string password { get; set; }
    }
}
