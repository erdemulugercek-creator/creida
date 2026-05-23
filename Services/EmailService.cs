using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Creida.Services;

public class SmtpOptions
{
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromName { get; set; } = "Creida";
    public string FromEmail { get; set; } = "hello@creida.co";
    public string ToEmail { get; set; } = "hello@creida.co";
    public bool UseStartTls { get; set; } = true;
}

public interface IEmailService
{
    Task SendBriefAsync(string name, string email, string? company, string? service, string message, CancellationToken ct = default);
}

public class SmtpEmailService : IEmailService
{
    private readonly SmtpOptions _opts;
    private readonly ILogger<SmtpEmailService> _log;

    public SmtpEmailService(SmtpOptions opts, ILogger<SmtpEmailService> log)
    {
        _opts = opts;
        _log = log;
    }

    public async Task SendBriefAsync(string name, string email, string? company, string? service, string message, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(_opts.Username) || string.IsNullOrWhiteSpace(_opts.Password))
        {
            // SMTP yapılandırılmadıysa sadece logla — geliştirme aşamasında pratik.
            _log.LogWarning("SMTP yapılandırılmadı, mail GÖNDERİLMEDİ. Brief: {Name} <{Email}> {Company} {Service}", name, email, company, service);
            return;
        }

        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(_opts.FromName, _opts.FromEmail));
        msg.To.Add(MailboxAddress.Parse(_opts.ToEmail));
        msg.ReplyTo.Add(new MailboxAddress(name, email));
        msg.Subject = $"Yeni brief — {name}" + (string.IsNullOrWhiteSpace(company) ? "" : $" / {company}");

        var body = $"""
        Yeni brief geldi.

        Ad Soyad : {name}
        E-posta  : {email}
        Şirket   : {company ?? "—"}
        İlgi     : {service ?? "—"}

        Mesaj
        ─────
        {message}
        """;

        msg.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync(_opts.Host, _opts.Port, _opts.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.SslOnConnect, ct);
        await client.AuthenticateAsync(_opts.Username, _opts.Password, ct);
        await client.SendAsync(msg, ct);
        await client.DisconnectAsync(true, ct);

        _log.LogInformation("Brief mail gönderildi: {Email}", _opts.ToEmail);
    }
}
