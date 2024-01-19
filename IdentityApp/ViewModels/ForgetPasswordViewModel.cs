

using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class ForgetPasswordViewModel
{
    [EmailAddress(ErrorMessage = "Not a Valid Email address!")]
    [Required(ErrorMessage = "Email cannot be empty!")]
    [Display(Name = "Email:")]
    public string Email { get; set; }
}
