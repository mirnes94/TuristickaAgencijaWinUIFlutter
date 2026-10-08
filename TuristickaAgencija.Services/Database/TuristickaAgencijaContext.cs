using Microsoft.EntityFrameworkCore;

namespace TuristickaAgencija.Services.Database
{
    public partial class TuristickaAgencijaContext : DbContext
    {
        public TuristickaAgencijaContext(DbContextOptions<TuristickaAgencijaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drzava> Drzava { get; set; }
        public virtual DbSet<Firma> Firma { get; set; }
        public virtual DbSet<Gradovi> Gradovi { get; set; }
        public virtual DbSet<Komentar> Komentar { get; set; }
        public virtual DbSet<Korisnici> Korisnici { get; set; }
        public virtual DbSet<KorisniciUloge> KorisniciUloge { get; set; }
        public virtual DbSet<Obavijesti> Obavijesti { get; set; }
        public virtual DbSet<Ocjene> Ocjene { get; set; }
        public virtual DbSet<Prevoz> Prevoz { get; set; }
        public virtual DbSet<Putovanja> Putovanja { get; set; }
        public virtual DbSet<Rezervacija> Rezervacija { get; set; }
        public virtual DbSet<Smjestaj> Smjestaj { get; set; }
        public virtual DbSet<Uloge> Uloge { get; set; }
        public virtual DbSet<Uplate> Uplate { get; set; }
        public virtual DbSet<Vodic> Vodic { get; set; }
        public virtual DbSet<VodiciPutovanja> VodiciPutovanja { get; set; }
        public virtual DbSet<ListaZelja> ListaZelja { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drzava>(entity =>
            {
                entity.Property(e => e.Naziv).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Naziv).IsUnique();
            });

            modelBuilder.Entity<Gradovi>(entity =>
            {
                entity.Property(e => e.NazivGrada).IsRequired().HasMaxLength(100);
                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Gradovi)
                    .HasForeignKey(d => d.DrzavaId);
            });

            modelBuilder.Entity<Firma>(entity =>
            {
                entity.Property(e => e.Naziv).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Adresa).IsRequired().HasMaxLength(200);
                entity.Property(e => e.BrojZiroracuna).IsRequired().HasMaxLength(20);
                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Firma)
                    .HasForeignKey(d => d.GradId);
            });

            modelBuilder.Entity<Prevoz>(entity =>
            {
                entity.Property(e => e.TipPrevoza).IsRequired().HasMaxLength(50);
                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.Prevoz)
                    .HasForeignKey(d => d.FirmaId);
            });

            modelBuilder.Entity<Smjestaj>(entity =>
            {
                entity.Property(e => e.NazivSmjestaja).IsRequired().HasMaxLength(100);
                entity.Property(e => e.OpisSmjestaja).HasMaxLength(1000);
                entity.Property(e => e.TipSobe).IsRequired().HasMaxLength(50).HasColumnName("Tip_sobe");
            });

            modelBuilder.Entity<Putovanja>(entity =>
            {
                entity.Property(e => e.NazivPutovanja).IsRequired().HasMaxLength(100);
                entity.Property(e => e.OpisPutovanja).IsRequired().HasMaxLength(2000);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Putovanja)
                    .HasForeignKey(d => d.GradId);

                entity.HasOne(d => d.Prevoz)
                    .WithMany(p => p.Putovanja)
                    .HasForeignKey(d => d.PrevozId);

                entity.HasOne(d => d.Smjestaj)
                    .WithMany(p => p.Putovanja)
                    .HasForeignKey(d => d.SmjestajId);
            });

            modelBuilder.Entity<Korisnici>(entity =>
            {
                entity.Property(e => e.Ime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Prezime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Telefon).IsRequired().HasMaxLength(20);
                entity.Property(e => e.KorisnickoIme).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LozinkaHash).IsRequired();
                entity.Property(e => e.LozinkaSalt).IsRequired();
                entity.HasIndex(e => e.KorisnickoIme).IsUnique();
            });

            modelBuilder.Entity<Uloge>(entity =>
            {
                entity.Property(e => e.Naziv).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Opis).HasMaxLength(200);
                entity.HasIndex(e => e.Naziv).IsUnique();
            });

            modelBuilder.Entity<KorisniciUloge>(entity =>
            {
                entity.HasKey(e => e.KorisnikUlogaId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Uloga)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.UlogaId);
            });

            modelBuilder.Entity<Komentar>(entity =>
            {
                entity.Property(e => e.Sadrzaj).IsRequired().HasMaxLength(1000);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Komentar)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Komentar)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<Ocjene>(entity =>
            {
                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.PutovanjeId);

                // Jedan korisnik moze ocijeniti jedno putovanje samo jednom (bitno za sistem preporuke).
                entity.HasIndex(e => new { e.KorisnikId, e.PutovanjeId }).IsUnique();
            });

            modelBuilder.Entity<ListaZelja>(entity =>
            {
                entity.Property(e => e.Opis).HasMaxLength(500);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.ListaZelja)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.ListaZelja)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<Obavijesti>(entity =>
            {
                entity.Property(e => e.Naziv).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Sadrzaj).IsRequired().HasMaxLength(2000);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Obavijesti)
                    .HasForeignKey(d => d.KorisnikId);
            });

            modelBuilder.Entity<Rezervacija>(entity =>
            {
                entity.Property(e => e.Ime).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Status).IsRequired().HasMaxLength(30);
                entity.Property(e => e.Napomena).HasMaxLength(500);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<Uplate>(entity =>
            {
                entity.Property(e => e.StripePaymentIntentId).HasMaxLength(100);
                entity.HasIndex(e => e.StripePaymentIntentId)
                    .IsUnique()
                    .HasFilter("[StripePaymentIntentId] IS NOT NULL");

                entity.HasOne(d => d.Rezervacija)
                    .WithMany(p => p.Uplate)
                    .HasForeignKey(d => d.RezervacijaId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany()
                    .HasForeignKey(d => d.KorisnikId);
            });

            modelBuilder.Entity<Vodic>(entity =>
            {
                entity.Property(e => e.Ime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Prezime).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Kontakt).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Jmbg).IsRequired().HasMaxLength(13).HasColumnName("JMBG");
            });

            modelBuilder.Entity<VodiciPutovanja>(entity =>
            {
                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.VodiciPutovanja)
                    .HasForeignKey(d => d.PutovanjeId);

                entity.HasOne(d => d.Vodic)
                    .WithMany(p => p.VodiciPutovanja)
                    .HasForeignKey(d => d.VodicId);
            });

            // Nema kaskadnog brisanja: brisanje zapisa koji se koristi negdje drugo vraca
            // jasnu poruku korisniku (vidi BaseCRUDService.DeleteAsync), a zavisni podaci
            // koji trebaju nestati zajedno sa roditeljem brisu se eksplicitno u servisima.
            foreach (var fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
