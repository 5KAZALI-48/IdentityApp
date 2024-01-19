using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class SignUpViewModel
{
    public SignUpViewModel()
    {

    }
    public SignUpViewModel(string userName, string email, string phone, string password)
    {
        UserName = userName;
        Email = email;
        Phone = phone;
        Password = password;

    }

    [Required(ErrorMessage = "Username cannot be empty!")]
    [Display(Name = "Username:")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Not a Valid Email address!")]
    [Required(ErrorMessage = "Email cannot be empty!")]
    [Display(Name = "Email:")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Phone number cannot be empty!")]
    [Display(Name = "Phone Number:")]
    public string Phone { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password cannot be empty!")]
    [Display(Name = "Password:")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Confirmation Password must match with Password!")]
    [Required(ErrorMessage = "ConfirmPassword cannot be empty!")]
    [Display(Name = "Confirm Password:")]
    public string ConfirmPassword { get; set; }
}
