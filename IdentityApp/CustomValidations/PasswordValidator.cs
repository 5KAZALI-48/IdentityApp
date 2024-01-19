using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.CustomValidations;

public class PasswordValidator : IPasswordValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
    {
        var errors = new List<IdentityError>();
        if (password!.ToLower().Contains(user.UserName!.ToLower()))
        {
            errors.Add(new()
            {
                Code = "PasswordContainsUserName",
                Description = "Password cannot contain username"
            });
        }

        if (password!.ToLower().StartsWith("123456"))
        {
            errors.Add(new()
            {
                Code = "PasswordStartWith123456",
                Description = "Password is too simple"
            });
        }

        if (errors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }

        return Task.FromResult(IdentityResult.Success);


        //throw new NotImplementedException();
    }
}
