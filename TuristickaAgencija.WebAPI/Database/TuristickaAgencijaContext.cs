using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class TuristickaAgencijaContext : DbContext
    {
        public TuristickaAgencijaContext()
        {
        }

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


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=TuristickaAgencija;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firma>(entity =>
            {
                entity.HasIndex(e => e.GradId);

                entity.HasOne(d => d.Grad)
                    .WithMany(p => p.Firma)
                    .HasForeignKey(d => d.GradId);
            });

            modelBuilder.Entity<Gradovi>(entity =>
            {
                entity.HasIndex(e => e.DrzavaId);

                entity.HasOne(d => d.Drzava)
                    .WithMany(p => p.Gradovi)
                    .HasForeignKey(d => d.DrzavaId);
            });

            modelBuilder.Entity<Komentar>(entity =>
            {
                entity.HasIndex(e => e.KorisnikId);

                entity.HasIndex(e => e.PutovanjeId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Komentar)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Komentar)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<ListaZelja>(entity =>
            {
                entity.HasIndex(e => e.KorisnikId);

                entity.HasIndex(e => e.PutovanjeId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.ListaZelja)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.ListaZelja)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<KorisniciUloge>(entity =>
            {
                entity.HasKey(e => e.KorisnikUlogaId);

                entity.HasIndex(e => e.KorisnikId);

                entity.HasIndex(e => e.UlogaId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Uloga)
                    .WithMany(p => p.KorisniciUloge)
                    .HasForeignKey(d => d.UlogaId);
            });

            modelBuilder.Entity<Obavijesti>(entity =>
            {
                entity.HasIndex(e => e.KorisnikId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Obavijesti)
                    .HasForeignKey(d => d.KorisnikId);
            });

            modelBuilder.Entity<Ocjene>(entity =>
            {
                entity.HasIndex(e => e.KorisnikId);

                entity.HasIndex(e => e.PutovanjeId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Ocjene)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<Prevoz>(entity =>
            {
                entity.HasIndex(e => e.FirmaId);

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.Prevoz)
                    .HasForeignKey(d => d.FirmaId);
            });

            modelBuilder.Entity<Putovanja>(entity =>
            {
                entity.HasIndex(e => e.GradId);

                entity.HasIndex(e => e.PrevozId);

                entity.HasIndex(e => e.SmjestajId);

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

            modelBuilder.Entity<Rezervacija>(entity =>
            {
                entity.HasIndex(e => e.KorisnikId);

                entity.HasIndex(e => e.PutovanjeId);

                entity.HasOne(d => d.Korisnik)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.KorisnikId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.Rezervacija)
                    .HasForeignKey(d => d.PutovanjeId);
            });

            modelBuilder.Entity<Smjestaj>(entity =>
            {
                entity.Property(e => e.TipSobe).HasColumnName("Tip_sobe");
            });

            modelBuilder.Entity<Uplate>(entity =>
            {
                entity.HasOne(d => d.Rezervacija)
                    .WithMany(p => p.Uplate)
                    .HasForeignKey(d => d.RezervacijaId);
            });

            modelBuilder.Entity<Vodic>(entity =>
            {
                entity.Property(e => e.Jmbg).HasColumnName("JMBG");
            });

            modelBuilder.Entity<VodiciPutovanja>(entity =>
            {
                entity.HasIndex(e => e.PutovanjeId);

                entity.HasIndex(e => e.VodicId);

                entity.HasOne(d => d.Putovanje)
                    .WithMany(p => p.VodiciPutovanja)
                    .HasForeignKey(d => d.PutovanjeId);

                entity.HasOne(d => d.Vodic)
                    .WithMany(p => p.VodiciPutovanja)
                    .HasForeignKey(d => d.VodicId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
