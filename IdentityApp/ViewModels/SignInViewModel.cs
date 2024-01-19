using System.ComponentModel.DataAnnotations;

namespace IdentityApp.ViewModels;

public class SignInViewModel
{
    public SignInViewModel()
    {

    }
    public SignInViewModel(string email, string password)
    {

        Email = email;
        Password = password;

    }
    [EmailAddress(ErrorMessage = "Not a Valid Email address!")]
    [Required(ErrorMessage = "Email cannot be empty!")]
    [Display(Name = "Email:")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password cannot be empty!")]
    [Display(Name = "Password:")]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}

