using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using PROYECTOS_MONSTER_NRC30715_GR04.Services.Interfaces;

namespace PROYECTOS_MONSTER_NRC30715_GR04.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        var smtp = _config.GetSection("Smtp");
        var host = smtp["Host"];
        var port = int.Parse(smtp["Port"] ?? "25");
        var user = smtp["User"];
        var pass = smtp["Pass"];
        var from = smtp["From"] ?? user;
        var fromName = smtp["FromName"] ?? "No Reply";
        var useSsl = bool.Parse(smtp["UseSSL"] ?? "false");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromName, from));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;

        var body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = htmlBody
        };

        message.Body = body;

        using var client = new SmtpClient();
        if (useSsl)
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
        else
            await client.ConnectAsync(host, port, SecureSocketOptions.None);

        if (!string.IsNullOrEmpty(user))
        {
            await client.AuthenticateAsync(user, pass);
        }

        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
