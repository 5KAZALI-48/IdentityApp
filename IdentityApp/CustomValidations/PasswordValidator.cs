using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.CustomValidations;

public class PasswordValidator : IPasswordValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
    {
        var errors = new List<IdentityError>();
        if (password!.Contains(user.UserName!, StringComparison.CurrentCultureIgnoreCase))
        {
            errors.Add(new()
            {
                Code = "PasswordContainsUserName",
                Description = "Password cannot contain username"
            });
        }

        if (password!.StartsWith("123456", StringComparison.CurrentCultureIgnoreCase))
        {
            errors.Add(new()
            {
                Code = "PasswordStartWith123456",
                Description = "Password is too simple"
            });
        }

        if (errors.Count != 0)
        {
            return Task.FromResult(IdentityResult.Failed([.. errors]));
        }

        return Task.FromResult(IdentityResult.Success);


        //throw new NotImplementedException();
    }
}
