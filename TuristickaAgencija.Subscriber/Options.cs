namespace TuristickaAgencija.Subscriber
{
    public class RabbitMqOptions
    {
        public const string Sekcija = "RabbitMQ";

        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string VirtualHost { get; set; } = "/";
    }

    public class SmtpOptions
    {
        public const string Sekcija = "Smtp";

        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string FromEmail { get; set; } = "noreply@turisticka-agencija.ba";
        public string FromName { get; set; } = "Turistička agencija";
    }
}
