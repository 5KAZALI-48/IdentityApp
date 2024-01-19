namespace IdentityApp.Services;

public interface IEmailService
{
    Task SendPasswordResetEmail(string resetPasswordEmailLink, string ToEmail);
}
