using SecretSanta.ApiService.Services.Email.Settings;

using System.Net;
using System.Net.Mail;

namespace SecretSanta.ApiService.Services.Email;

public class EmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    private string LoadTemplate(string templatePath, Dictionary<string, string> placeholders)
    {
        var templateContent = File.ReadAllText(templatePath);

        foreach (var placeholder in placeholders)
        {
            templateContent = templateContent.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
        }

        return templateContent;
    }

    public async Task SendSecretSantaEmailAsync(string recipientEmail, string giverName, string receiverName, string receiverEmail)
    {
        string templatePath = Path.Combine("EmailTemplate", "SecretSantaEmailTemplate.html");

        // Define placeholders and values
        var placeholders = new Dictionary<string, string>
        {
            { "GiverName", giverName },
            { "ReceiverName", receiverName },
            { "ReceiverEmail", receiverEmail }
        };

        // Load and parse the template with placeholders
        var body = LoadTemplate(templatePath, placeholders);

        using (var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort))
        {
            client.Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.Password);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = "Missão de Natal / Christmas Quest",
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(recipientEmail);

            await client.SendMailAsync(mailMessage);
        }
    }
}
