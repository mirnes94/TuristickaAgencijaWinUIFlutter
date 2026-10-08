using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Drzava;
using TuristickaAgencija.Services.Firma;
using TuristickaAgencija.Services.Gradovi;
using TuristickaAgencija.Services.Izvjestaji;
using TuristickaAgencija.Services.Komentar;
using TuristickaAgencija.Services.Korisnici;
using TuristickaAgencija.Services.ListaZelja;
using TuristickaAgencija.Services.Mapping;
using TuristickaAgencija.Services.Messaging;
using TuristickaAgencija.Services.Obavijesti;
using TuristickaAgencija.Services.Ocjene;
using TuristickaAgencija.Services.Placanje;
using TuristickaAgencija.Services.Prevoz;
using TuristickaAgencija.Services.Putovanja;
using TuristickaAgencija.Services.RecommenderService;
using TuristickaAgencija.Services.Rezervacija;
using TuristickaAgencija.Services.Security;
using TuristickaAgencija.Services.Smjestaj;
using TuristickaAgencija.Services.Uloge;
using TuristickaAgencija.Services.Uplate;
using TuristickaAgencija.Services.Vodic;
using TuristickaAgencija.WebAPI.Filter;
using TuristickaAgencija.WebAPI.Security;

var builder = WebApplication.CreateBuilder(args);

// ---------- Konfiguracija (appsettings.json, nadjacano varijablama okruzenja iz .env u Dockeru) ----------
builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection(RabbitMqOptions.Sekcija));
builder.Services.Configure<VerifikacijaOptions>(builder.Configuration.GetSection(VerifikacijaOptions.Sekcija));
builder.Services.Configure<PreporukeOptions>(builder.Configuration.GetSection(PreporukeOptions.Sekcija));
builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection(StripeOptions.Sekcija));

builder.Services.AddDbContext<TuristickaAgencijaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TuristickaAgencija")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// ---------- Servisi ----------
builder.Services.AddSingleton<IMessageProducer, RabbitMqMessageProducer>();

builder.Services.AddScoped<IDrzavaService, DrzavaService>();
builder.Services.AddScoped<IGradoviService, GradoviService>();
builder.Services.AddScoped<IFirmaService, FirmaService>();
builder.Services.AddScoped<IPrevozService, PrevozService>();
builder.Services.AddScoped<ISmjestajService, SmjestajService>();
builder.Services.AddScoped<IUlogeService, UlogeService>();
builder.Services.AddScoped<IVodicService, VodicService>();
builder.Services.AddScoped<IPutovanjaService, PutovanjaService>();
builder.Services.AddScoped<IKorisniciService, KorisniciService>();
builder.Services.AddScoped<IRezervacijaService, RezervacijaService>();
builder.Services.AddScoped<IUplateService, UplateService>();
builder.Services.AddScoped<IKomentarService, KomentarService>();
builder.Services.AddScoped<IOcjeneService, OcjeneService>();
builder.Services.AddScoped<IObavijestiService, ObavijestiService>();
builder.Services.AddScoped<IListaZeljaService, ListaZeljaService>();
builder.Services.AddScoped<IRecommenderService, RecommenderService>();
builder.Services.AddScoped<IIzvjestajService, IzvjestajService>();
builder.Services.AddScoped<IPlacanjeService, PlacanjeService>();

// ---------- Autentifikacija ----------
builder.Services
    .AddAuthentication(BasicAuthenticationHandler.SchemeName)
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(BasicAuthenticationHandler.SchemeName, null);
builder.Services.AddAuthorization();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Newtonsoft.Json: tolerantnije parsiranje (npr. datum "2026-10-08 10:15:00" koji salje mobilna aplikacija).
builder.Services
    .AddControllers(o => o.Filters.Add<ErrorFilter>())
    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Turistička agencija API", Version = "v1" });
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic autentifikacija (korisničko ime i lozinka)."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "basic" }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// ---------- Kreiranje baze + testni podaci (sa ponavljanjem dok SQL Server u Dockeru ne postane dostupan) ----------
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    const int maxPokusaja = 20;

    for (var pokusaj = 1; ; pokusaj++)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TuristickaAgencijaContext>();
            Data.Seed(context);
            logger.LogInformation("Baza je spremna.");
            break;
        }
        catch (Exception ex) when (pokusaj < maxPokusaja)
        {
            logger.LogWarning("Baza još nije dostupna (pokušaj {Pokusaj}/{Max}): {Poruka}", pokusaj, maxPokusaja, ex.Message);
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }
    }
}

app.Run();
