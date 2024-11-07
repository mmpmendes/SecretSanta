using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Mail;

namespace SecretSanta.ApiService.Services.Email;

public class MailService : IMailService
{
    MailSettings Mail_Settings;
    private readonly IConfiguration _config;

    public MailService(
        IOptions<MailSettings> options
        , IConfiguration config
        )
    {
        Mail_Settings = options.Value;
        _config = config;
    }

    public bool SendMail(string giverEmail, string giverName, string receiverEmail, string receiverName)
    {
        try
        {
            var templatepath = _config["template_path"];
            var sstemplate = _config["secret_santa_template"];

            if (String.IsNullOrEmpty(templatepath) || String.IsNullOrEmpty(sstemplate))
                throw new Exception("Bad configuration for templates");

            string templatePath = Path.Combine(templatepath, sstemplate);

            // Define placeholders and values
            var placeholders = new Dictionary<string, string>
        {
            { "GiverName", giverName },
            { "ReceiverName", receiverName },
            { "ReceiverEmail", receiverEmail }
        };

            // Load and parse the template with placeholders
            var body = LoadTemplate(templatePath, placeholders);

            SmtpClient client = new SmtpClient(Mail_Settings.Host)
            {
                Port = Mail_Settings.Port,
                EnableSsl = Mail_Settings.UseSSL,
                Credentials = new NetworkCredential(Mail_Settings.UserName, Mail_Settings.Password)
            };

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(Mail_Settings.EmailId, Mail_Settings.Name),
                Subject = Mail_Settings.DefaultSubject,
                Body = body,
                IsBodyHtml = true // Set to true if you want to send HTML email
            };

            mail.To.Add(giverEmail);
            client.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
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
}