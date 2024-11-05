using System.ComponentModel.DataAnnotations;

namespace CMSpro1.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="enter email address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "enter confirm email address")]
        [EmailAddress]
        [Compare("Email",ErrorMessage =("email and confirm not match"))]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "enter password address")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "enter confirm password address")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = ("password and confirm not match"))]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
    }
}
