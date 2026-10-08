using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TuristickaAgencija.Model.Messages;

namespace TuristickaAgencija.Subscriber
{
    /// <summary>
    /// Pomocni servis: slusa red "turisticka-agencija.notifikacije" na RabbitMQ-u i
    /// asinhrono salje email (potvrda registracije, obavijesti, rezervacije, uplate).
    /// </summary>
    public class NotifikacijeWorker : BackgroundService
    {
        private readonly RabbitMqOptions _options;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<NotifikacijeWorker> _logger;
        private IConnection _connection;
        private IModel _channel;

        public NotifikacijeWorker(IOptions<RabbitMqOptions> options, IEmailSender emailSender, ILogger<NotifikacijeWorker> logger)
        {
            _options = options.Value;
            _emailSender = emailSender;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await PoveziSeAsync(stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (_, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var poruka = JsonSerializer.Deserialize<NotifikacijaPoruka>(json);
                    _logger.LogInformation("Primljena poruka {Tip} za {Email}.", poruka?.Tip, poruka?.PrimalacEmail);

                    if (poruka != null)
                    {
                        await _emailSender.PosaljiAsync(poruka, stoppingToken);
                    }

                    _channel.BasicAck(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Obrada poruke nije uspjela.");
                    // Poruka se ne vraca u red da ne bi nastala beskonacna petlja.
                    _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                }
            };

            _channel.BasicConsume(queue: NotifikacijaPoruka.RedPoruka, autoAck: false, consumer: consumer);
            _logger.LogInformation("Subscriber slusa red {Red}.", NotifikacijaPoruka.RedPoruka);

            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                // gasenje servisa
            }
        }

        private async Task PoveziSeAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory
            {
                HostName = _options.Host,
                Port = _options.Port,
                UserName = _options.Username,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                DispatchConsumersAsync = true,
                AutomaticRecoveryEnabled = true
            };

            // RabbitMQ kontejner se obicno podigne nakon subscriber-a -> pokusavamo ponovo.
            for (var pokusaj = 1; ; pokusaj++)
            {
                try
                {
                    _connection = factory.CreateConnection("turisticka-agencija-subscriber");
                    _channel = _connection.CreateModel();
                    _channel.QueueDeclare(
                        queue: NotifikacijaPoruka.RedPoruka,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                    _logger.LogInformation("Povezan na RabbitMQ ({Host}:{Port}).", _options.Host, _options.Port);
                    return;
                }
                catch (Exception ex) when (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogWarning("RabbitMQ nije dostupan (pokusaj {Pokusaj}): {Poruka}", pokusaj, ex.Message);
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
