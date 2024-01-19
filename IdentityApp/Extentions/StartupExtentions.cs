using IdentityApp.CustomValidations;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Extentions;

public static class StartupExtentions
{
    public static void AddIdentityWithExtention(this IServiceCollection services)
    {
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
        {
            opt.TokenLifespan = TimeSpan.FromHours(1);
        });
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz123456789_";

            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            options.Lockout.MaxFailedAccessAttempts = 3;

        }).AddPasswordValidator<PasswordValidator>()
          .AddUserValidator<UserValidator>()
          .AddDefaultTokenProviders()
          .AddEntityFrameworkStores<AppDbContext>();
          
    }
}
