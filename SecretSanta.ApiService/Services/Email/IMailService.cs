
namespace SecretSanta.ApiService.Services.Email;

public interface IMailService
{
    bool SendMail(string giverEmail, string giverName, string receiverEmail, string receiverName);
}
