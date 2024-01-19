using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class ResetPasswordViewModel
{
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password cannot be empty!")]
    [Display(Name = "New Password:")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Confirmation Password must match with Password!")]
    [Required(ErrorMessage = "ConfirmPassword cannot be empty!")]
    [Display(Name = "Confirm Password:")]
    public string ConfirmPassword { get; set; }
}
