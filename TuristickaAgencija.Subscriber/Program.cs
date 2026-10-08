using TuristickaAgencija.Subscriber;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.Sekcija));
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.Sekcija));
builder.Services.AddSingleton<IEmailSender, SmtpEmailSender>();
builder.Services.AddHostedService<NotifikacijeWorker>();

var host = builder.Build();
host.Run();
