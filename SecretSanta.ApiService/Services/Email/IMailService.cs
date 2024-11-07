
namespace SecretSanta.ApiService.Services.Email;

public interface IMailService
{
    bool SendMail(string recipientEmail, string giverName, string receiverEmail, string receiverName);
}
