using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using TuristickaAgencija.Model.Messages;

namespace TuristickaAgencija.Subscriber
{
    public interface IEmailSender
    {
        Task PosaljiAsync(NotifikacijaPoruka poruka, CancellationToken cancellationToken);
    }

    public class SmtpEmailSender : IEmailSender
    {
        private readonly SmtpOptions _options;
        private readonly ILogger<SmtpEmailSender> _logger;

        public SmtpEmailSender(IOptions<SmtpOptions> options, ILogger<SmtpEmailSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task PosaljiAsync(NotifikacijaPoruka poruka, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(poruka.PrimalacEmail))
            {
                _logger.LogWarning("Poruka {Tip} nema primaoca - preskacem.", poruka.Tip);
                return;
            }

            var html = KreirajHtml(poruka);

            if (string.IsNullOrWhiteSpace(_options.Host))
            {
                // SMTP nije konfigurisan -> samo logujemo sadrzaj (korisno za lokalni razvoj).
                _logger.LogInformation("[EMAIL - SMTP nije konfigurisan] Za: {Email} | {Naslov} | {Sadrzaj} {Link}",
                    poruka.PrimalacEmail, poruka.Naslov, poruka.Sadrzaj, poruka.Link);
                return;
            }

            using var message = new MailMessage
            {
                From = new MailAddress(_options.FromEmail, _options.FromName),
                Subject = poruka.Naslov,
                Body = html,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };
            message.To.Add(new MailAddress(poruka.PrimalacEmail, poruka.PrimalacIme ?? poruka.PrimalacEmail));

            using var client = new SmtpClient(_options.Host, _options.Port)
            {
                EnableSsl = _options.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            if (!string.IsNullOrWhiteSpace(_options.Username))
            {
                client.Credentials = new NetworkCredential(_options.Username, _options.Password);
            }

            await client.SendMailAsync(message, cancellationToken);
            _logger.LogInformation("Email ({Tip}) poslan na {Email}.", poruka.Tip, poruka.PrimalacEmail);
        }

        private static string KreirajHtml(NotifikacijaPoruka poruka)
        {
            var ime = WebUtility.HtmlEncode(poruka.PrimalacIme ?? "korisniče");
            var sadrzaj = WebUtility.HtmlEncode(poruka.Sadrzaj ?? string.Empty).Replace("\n", "<br/>");
            var link = string.IsNullOrWhiteSpace(poruka.Link)
                ? string.Empty
                : $"<p><a href=\"{WebUtility.HtmlEncode(poruka.Link)}\" style=\"background:#1565c0;color:#fff;padding:10px 18px;border-radius:6px;text-decoration:none\">Aktiviraj nalog</a></p>" +
                  $"<p style=\"font-size:12px;color:#666\">Ili kopirajte link: {WebUtility.HtmlEncode(poruka.Link)}</p>";

            return $@"<html><body style=""font-family:Segoe UI,Arial,sans-serif;color:#222"">
<h2 style=""color:#1565c0"">Turistička agencija</h2>
<p>Poštovani/a {ime},</p>
<p>{sadrzaj}</p>
{link}
<hr/><p style=""font-size:12px;color:#888"">Ova poruka je automatski generisana. Molimo ne odgovarajte na nju.</p>
</body></html>";
        }
    }
}
