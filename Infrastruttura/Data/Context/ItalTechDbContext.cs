using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Infrastruttura.Data.Modelli;

#nullable disable

namespace Infrastruttura.Data.Context
{
    public partial class ItalTechDbContext : DbContext
    {
        public ItalTechDbContext()
        {
        }

        public ItalTechDbContext(DbContextOptions<ItalTechDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnalisiDiMercatoProgetti> AnalisiDiMercatoProgettis { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Componente> Componentes { get; set; }
        public virtual DbSet<Fornitore> Fornitores { get; set; }
        public virtual DbSet<Fornitura> Fornituras { get; set; }
        public virtual DbSet<Impiegato> Impiegatos { get; set; }
        public virtual DbSet<OrdineForniture> OrdineFornitures { get; set; }
        public virtual DbSet<Ordini> Ordinis { get; set; }
        public virtual DbSet<ProdottoAssemblato> ProdottoAssemblatos { get; set; }
        public virtual DbSet<ProdottoCase> ProdottoCases { get; set; }
        public virtual DbSet<Progetto> Progettos { get; set; }
        public virtual DbSet<Prototipo> Prototipos { get; set; }
        public virtual DbSet<RichiestaProgetto> RichiestaProgettos { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestProdottoAssemblato> TestProdottoAssemblatos { get; set; }
        public virtual DbSet<TestProdottoCase> TestProdottoCases { get; set; }
        public virtual DbSet<TestPrototipo> TestPrototipos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=.; initial catalog = ItalTech; persist security info=True; Integrated Security = SSPI;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<AnalisiDiMercatoProgetti>(entity =>
            {
                entity.HasKey(e => e.Codice)
                    .HasName("PK_AnalisiDiMercato");

                entity.ToTable("AnalisiDiMercatoProgetti");

                entity.Property(e => e.Codice).ValueGeneratedNever();

                entity.Property(e => e.CodiceOperatore)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DataFine).HasColumnType("datetime");

                entity.Property(e => e.Datainizio).HasColumnType("datetime");

                entity.Property(e => e.Descrizione).HasMaxLength(200);

                entity.Property(e => e.NomeOperatore).HasMaxLength(50);

                entity.Property(e => e.Risultato).HasMaxLength(200);

                entity.HasOne(d => d.CodiceOperatoreNavigation)
                    .WithMany(p => p.AnalisiDiMercatoProgettis)
                    .HasPrincipalKey(p => p.AziendaId)
                    .HasForeignKey(d => d.CodiceOperatore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnalisiDiMercato_AnalisiDiMercato");

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.AnalisiDiMercatoProgettis)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnalisiDiMercato_Progetto");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodFiscale);

                entity.ToTable("Cliente");

                entity.Property(e => e.CodFiscale)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Cap)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Citta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cognome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataDiNascita).HasColumnType("datetime");

                entity.Property(e => e.Indirizzo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Componente>(entity =>
            {
                entity.HasKey(e => new { e.CodiceFornitura, e.CodiceProgetto });

                entity.ToTable("Componente");

                entity.Property(e => e.CodiceFornitura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceFornituraNavigation)
                    .WithMany(p => p.Componentes)
                    .HasForeignKey(d => d.CodiceFornitura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Componente_Componente");

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.Componentes)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Componente_Progetto");
            });

            modelBuilder.Entity<Fornitore>(entity =>
            {
                entity.HasKey(e => e.PartitaIva);

                entity.ToTable("Fornitore");

                entity.Property(e => e.PartitaIva)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Cap)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Città)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.Indirizzo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumTelefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RagioneSociale).HasMaxLength(200);
            });

            modelBuilder.Entity<Fornitura>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("Fornitura");

                entity.Property(e => e.Codice)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodiceFornitore)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CostoAlKg).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostoPerPezzo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SettoreDeposito)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceFornitoreNavigation)
                    .WithMany(p => p.Fornituras)
                    .HasForeignKey(d => d.CodiceFornitore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fornitura_Fornitura");
            });

            modelBuilder.Entity<Impiegato>(entity =>
            {
                entity.HasKey(e => e.CodFiscale);

                entity.ToTable("Impiegato");

                entity.HasIndex(e => e.AziendaId, "IX_Impiegato")
                    .IsUnique();

                entity.Property(e => e.CodFiscale)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.AziendaId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AziendaID");

                entity.Property(e => e.Cap)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Citta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cognome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DataDiNascita).HasColumnType("datetime");

                entity.Property(e => e.Indirizzo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(450)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Impiegatos)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Impiegato_AspNetUsers");
            });

            modelBuilder.Entity<OrdineForniture>(entity =>
            {
                entity.HasKey(e => new { e.CodiceOrdine, e.CodiceFornitura });

                entity.ToTable("OrdineForniture");

                entity.Property(e => e.CodiceFornitura)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceFornituraNavigation)
                    .WithMany(p => p.OrdineFornitures)
                    .HasForeignKey(d => d.CodiceFornitura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdineForniture_Fornitura");

                entity.HasOne(d => d.CodiceOrdineNavigation)
                    .WithMany(p => p.OrdineFornitures)
                    .HasForeignKey(d => d.CodiceOrdine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdineForniture_Ordini");
            });

            modelBuilder.Entity<Ordini>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("Ordini");

                entity.Property(e => e.DataCreazione).HasColumnType("datetime");

                entity.Property(e => e.DataInvio).HasColumnType("datetime");

                entity.Property(e => e.Operatore)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.OperatoreNavigation)
                    .WithMany(p => p.Ordinis)
                    .HasPrincipalKey(p => p.AziendaId)
                    .HasForeignKey(d => d.Operatore)
                    .HasConstraintName("FK_Ordini_Ordini");
            });

            modelBuilder.Entity<ProdottoAssemblato>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("ProdottoAssemblato");

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FasciaDiMercato)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.ProdottoAssemblatos)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProdottoAssemblato_Progetto");
            });

            modelBuilder.Entity<ProdottoCase>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("ProdottoCase");

                entity.Property(e => e.Colore)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Costo).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.ProdottoCases)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProdottoCase_Progetto");
            });

            modelBuilder.Entity<Progetto>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("Progetto");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CostoFinale).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CostoPrevisto).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DataFine).HasColumnType("datetime");

                entity.Property(e => e.DataInizio).HasColumnType("datetime");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeProgetto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectManager)                    
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.Progettos)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_Progetto_Cliente");

                entity.HasOne(d => d.ProjectManagerNavigation)
                    .WithMany(p => p.Progettos)
                    .HasPrincipalKey(p => p.AziendaId)
                    .HasForeignKey(d => d.ProjectManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Progetto_Impiegato");
            });

            modelBuilder.Entity<Prototipo>(entity =>
            {
                entity.HasKey(e => new { e.Numero, e.CodiceProgetto })
                    .HasName("PK_Prototipo2");

                entity.ToTable("Prototipo");

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Descrizione)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Modifiche)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MotivoFallimento)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.Prototipos)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prototipo_Progetto");
            });

            modelBuilder.Entity<RichiestaProgetto>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("RichiestaProgetto");

                entity.Property(e => e.Budget).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Cliente)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Operatore)                    
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.RichiestaProgettos)
                    .HasForeignKey(d => d.Cliente)
                    .HasConstraintName("FK_RichiestaProgetto_Cliente");

                entity.HasOne(d => d.CodiceProgettoNavigation)
                    .WithMany(p => p.RichiestaProgettos)
                    .HasForeignKey(d => d.CodiceProgetto)
                    .HasConstraintName("FK_RichiestaProgetto_Progetto");

                entity.HasOne(d => d.OperatoreNavigation)
                    .WithMany(p => p.RichiestaProgettos)
                    .HasPrincipalKey(p => p.AziendaId)
                    .HasForeignKey(d => d.Operatore)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RichiestaProgetto_Impiegato");
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Codice);

                entity.ToTable("Test");

                entity.Property(e => e.Codice).ValueGeneratedNever();

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Operatore)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ValoriDiRiferimento).HasMaxLength(200);

                entity.HasOne(d => d.OperatoreNavigation)
                    .WithMany(p => p.Tests)
                    .HasPrincipalKey(p => p.AziendaId)
                    .HasForeignKey(d => d.Operatore)
                    .HasConstraintName("FK_Test_Impiegato");
            });

            modelBuilder.Entity<TestProdottoAssemblato>(entity =>
            {
                entity.HasKey(e => new { e.CodiceTest, e.CodiceProdottoAssemblato });

                entity.ToTable("TestProdottoAssemblato");

                entity.HasOne(d => d.CodiceProdottoAssemblatoNavigation)
                    .WithMany(p => p.TestProdottoAssemblatos)
                    .HasForeignKey(d => d.CodiceProdottoAssemblato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestProdottoAssemblato_ProdottoAssemblato");

                entity.HasOne(d => d.CodiceTestNavigation)
                    .WithMany(p => p.TestProdottoAssemblatos)
                    .HasForeignKey(d => d.CodiceTest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestProdottoAssemblato_Test");
            });

            modelBuilder.Entity<TestProdottoCase>(entity =>
            {
                entity.HasKey(e => new { e.CodiceTest, e.CodiceProdottoCase });

                entity.ToTable("TestProdottoCase");

                entity.HasOne(d => d.CodiceProdottoCaseNavigation)
                    .WithMany(p => p.TestProdottoCases)
                    .HasForeignKey(d => d.CodiceProdottoCase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestProdottoCase_ProdottoCase");

                entity.HasOne(d => d.CodiceTestNavigation)
                    .WithMany(p => p.TestProdottoCases)
                    .HasForeignKey(d => d.CodiceTest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestProdottoCase_Test");
            });

            modelBuilder.Entity<TestPrototipo>(entity =>
            {
                entity.HasKey(e => new { e.CodiceTest, e.NumPrototipo, e.CodiceProgetto })
                    .HasName("PK_TestPrototipo_1");

                entity.ToTable("TestPrototipo");

                entity.HasOne(d => d.CodiceTestNavigation)
                    .WithMany(p => p.TestPrototipos)
                    .HasForeignKey(d => d.CodiceTest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestPrototipo_Test");

                entity.HasOne(d => d.Prototipo)
                    .WithMany(p => p.TestPrototipos)
                    .HasForeignKey(d => new { d.NumPrototipo, d.CodiceProgetto })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TestPrototipo_Prototipo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
