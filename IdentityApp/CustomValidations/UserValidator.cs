﻿using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.CustomValidations;

public class UserValidator : IUserValidator<AppUser>
{
    public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
    {
        var errors = new List<IdentityError>();
        var isDigit = int.TryParse(user.UserName[0]!.ToString(), out _);

        if (isDigit)
        {
            errors.Add(new()
            {
                Code = "UsernameFirstLetterContainsDigit",
                Description = "Username cannot start with a digit"
            });
        }

        if (errors.Any())
        {
            return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
        }

        return Task.FromResult(IdentityResult.Success);
    }
}