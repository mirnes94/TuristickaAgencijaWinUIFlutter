using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TuristickaAgencija.Model.Messages;

namespace TuristickaAgencija.Services.Messaging
{
    /// <summary>
    /// Producer (glavni servis -> RabbitMQ). Registruje se kao singleton i drzi jednu konekciju.
    /// </summary>
    public sealed class RabbitMqMessageProducer : IMessageProducer, IDisposable
    {
        private readonly RabbitMqOptions _options;
        private readonly ILogger<RabbitMqMessageProducer> _logger;
        private readonly object _lock = new object();
        private IConnection _connection;

        public RabbitMqMessageProducer(IOptions<RabbitMqOptions> options, ILogger<RabbitMqMessageProducer> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public void Posalji(NotifikacijaPoruka poruka)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(poruka));

                lock (_lock)
                {
                    using var channel = GetConnection().CreateModel();
                    channel.QueueDeclare(
                        queue: NotifikacijaPoruka.RedPoruka,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;
                    properties.ContentType = "application/json";
                    properties.Type = poruka.Tip;

                    channel.BasicPublish(
                        exchange: string.Empty,
                        routingKey: NotifikacijaPoruka.RedPoruka,
                        basicProperties: properties,
                        body: body);
                }

                _logger.LogInformation("Poslana poruka {Tip} za {Primalac} na RabbitMQ.", poruka.Tip, poruka.PrimalacEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Slanje poruke {Tip} na RabbitMQ nije uspjelo.", poruka.Tip);
            }
        }

        private IConnection GetConnection()
        {
            if (_connection != null && _connection.IsOpen)
            {
                return _connection;
            }

            _connection?.Dispose();

            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.Username,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                AutomaticRecoveryEnabled = true
            };

            _connection = factory.CreateConnection("turisticka-agencija-api");
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
