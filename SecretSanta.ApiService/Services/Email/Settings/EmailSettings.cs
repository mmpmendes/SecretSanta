namespace SecretSanta.ApiService.Services.Email.Settings;

public class EmailSettings
{
    public string SmtpServer { get; set; } = "smtp.gmail.com";
    public int SmtpPort { get; set; } = 465; // 587 - Use 465 for SSL
    public string SenderEmail { get; set; } // Your Gmail address
    public string SenderName { get; set; } = "Secret Santa";
    public string Password { get; set; } // App Password or Gmail password
}