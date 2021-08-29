using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastruttura.Data.Migrations
{
    public partial class PrimaMigrazione : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    CodFiscale = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Cognome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DataDiNascita = table.Column<DateTime>(type: "datetime", nullable: true),
                    Indirizzo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Citta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cap = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.CodFiscale);
                });

            migrationBuilder.CreateTable(
                name: "Fornitore",
                columns: table => new
                {
                    PartitaIva = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RagioneSociale = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Indirizzo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Città = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cap = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    NumTelefono = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornitore", x => x.PartitaIva);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Impiegato",
                columns: table => new
                {
                    CodFiscale = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Cognome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DataDiNascita = table.Column<DateTime>(type: "datetime", nullable: false),
                    Indirizzo = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Citta = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cap = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AziendaID = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impiegato", x => x.CodFiscale);
                    table.UniqueConstraint("AK_Impiegato_AziendaID", x => x.AziendaID);
                    table.ForeignKey(
                        name: "FK_Impiegato_AspNetUsers",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fornitura",
                columns: table => new
                {
                    Codice = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    CostoPerPezzo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CostoAlKg = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CodiceFornitore = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    SettoreDeposito = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Tipo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornitura", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_Fornitura_Fornitura",
                        column: x => x.CodiceFornitore,
                        principalTable: "Fornitore",
                        principalColumn: "PartitaIva",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordini",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCreazione = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataInvio = table.Column<DateTime>(type: "datetime", nullable: true),
                    Operatore = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordini", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_Ordini_Ordini",
                        column: x => x.Operatore,
                        principalTable: "Impiegato",
                        principalColumn: "AziendaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Progetto",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInizio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime", nullable: true),
                    NomeProgetto = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    CostoPrevisto = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CostoFinale = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CodiceAnalisiMercato = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Cliente = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    ProjectManager = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progetto", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_Progetto_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Cliente",
                        principalColumn: "CodFiscale",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Progetto_Impiegato",
                        column: x => x.ProjectManager,
                        principalTable: "Impiegato",
                        principalColumn: "AziendaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ValoriDiRiferimento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    QuantitaEseguiti = table.Column<int>(type: "int", nullable: true),
                    QuantitaPassati = table.Column<int>(type: "int", nullable: true),
                    QuantitaFalliti = table.Column<int>(type: "int", nullable: true),
                    Operatore = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_Test_Impiegato",
                        column: x => x.Operatore,
                        principalTable: "Impiegato",
                        principalColumn: "AziendaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdineForniture",
                columns: table => new
                {
                    CodiceOrdine = table.Column<int>(type: "int", nullable: false),
                    CodiceFornitura = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Quantità = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdineForniture", x => new { x.CodiceOrdine, x.CodiceFornitura });
                    table.ForeignKey(
                        name: "FK_OrdineForniture_Fornitura",
                        column: x => x.CodiceFornitura,
                        principalTable: "Fornitura",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdineForniture_Ordini",
                        column: x => x.CodiceOrdine,
                        principalTable: "Ordini",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnalisiDiMercatoProgetti",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false),
                    CodiceOperatore = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    NomeOperatore = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false),
                    Datainizio = table.Column<DateTime>(type: "datetime", nullable: false),
                    DataFine = table.Column<DateTime>(type: "datetime", nullable: true),
                    Descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Risultato = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisiDiMercato", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_AnalisiDiMercato_AnalisiDiMercato",
                        column: x => x.CodiceOperatore,
                        principalTable: "Impiegato",
                        principalColumn: "AziendaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnalisiDiMercato_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Componente",
                columns: table => new
                {
                    CodiceFornitura = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false),
                    NumPezzi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Componente", x => new { x.CodiceFornitura, x.CodiceProgetto });
                    table.ForeignKey(
                        name: "FK_Componente_Componente",
                        column: x => x.CodiceFornitura,
                        principalTable: "Fornitura",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Componente_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdottoAssemblato",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lotto = table.Column<int>(type: "int", nullable: true),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    FasciaDiMercato = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Peso = table.Column<double>(type: "float", nullable: true),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottoAssemblato", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_ProdottoAssemblato_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdottoCase",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lotto = table.Column<int>(type: "int", nullable: true),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PesoKg = table.Column<double>(type: "float", nullable: true),
                    Colore = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Quantita = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdottoCase", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_ProdottoCase_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prototipo",
                columns: table => new
                {
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime", nullable: true),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Modifiche = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RisultatoTest = table.Column<bool>(type: "bit", nullable: true),
                    MotivoFallimento = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prototipo2", x => new { x.Numero, x.CodiceProgetto });
                    table.ForeignKey(
                        name: "FK_Prototipo_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RichiestaProgetto",
                columns: table => new
                {
                    Codice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: true),
                    Tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Descrizione = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    EsitoStudio = table.Column<bool>(type: "bit", nullable: true),
                    Budget = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Cliente = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: true),
                    Operatore = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RichiestaProgetto", x => x.Codice);
                    table.ForeignKey(
                        name: "FK_RichiestaProgetto_Cliente",
                        column: x => x.Cliente,
                        principalTable: "Cliente",
                        principalColumn: "CodFiscale",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RichiestaProgetto_Impiegato",
                        column: x => x.Operatore,
                        principalTable: "Impiegato",
                        principalColumn: "AziendaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RichiestaProgetto_Progetto",
                        column: x => x.CodiceProgetto,
                        principalTable: "Progetto",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestProdottoAssemblato",
                columns: table => new
                {
                    CodiceTest = table.Column<int>(type: "int", nullable: false),
                    CodiceProdottoAssemblato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestProdottoAssemblato", x => new { x.CodiceTest, x.CodiceProdottoAssemblato });
                    table.ForeignKey(
                        name: "FK_TestProdottoAssemblato_ProdottoAssemblato",
                        column: x => x.CodiceProdottoAssemblato,
                        principalTable: "ProdottoAssemblato",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestProdottoAssemblato_Test",
                        column: x => x.CodiceTest,
                        principalTable: "Test",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestProdottoCase",
                columns: table => new
                {
                    CodiceTest = table.Column<int>(type: "int", nullable: false),
                    CodiceProdottoCase = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestProdottoCase", x => new { x.CodiceTest, x.CodiceProdottoCase });
                    table.ForeignKey(
                        name: "FK_TestProdottoCase_ProdottoCase",
                        column: x => x.CodiceProdottoCase,
                        principalTable: "ProdottoCase",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestProdottoCase_Test",
                        column: x => x.CodiceTest,
                        principalTable: "Test",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestPrototipo",
                columns: table => new
                {
                    CodiceTest = table.Column<int>(type: "int", nullable: false),
                    NumPrototipo = table.Column<int>(type: "int", nullable: false),
                    CodiceProgetto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestPrototipo_1", x => new { x.CodiceTest, x.NumPrototipo, x.CodiceProgetto });
                    table.ForeignKey(
                        name: "FK_TestPrototipo_Prototipo",
                        columns: x => new { x.NumPrototipo, x.CodiceProgetto },
                        principalTable: "Prototipo",
                        principalColumns: new[] { "Numero", "CodiceProgetto" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestPrototipo_Test",
                        column: x => x.CodiceTest,
                        principalTable: "Test",
                        principalColumn: "Codice",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalisiDiMercatoProgetti_CodiceOperatore",
                table: "AnalisiDiMercatoProgetti",
                column: "CodiceOperatore");

            migrationBuilder.CreateIndex(
                name: "IX_AnalisiDiMercatoProgetti_CodiceProgetto",
                table: "AnalisiDiMercatoProgetti",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "([NormalizedName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Componente_CodiceProgetto",
                table: "Componente",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_Fornitura_CodiceFornitore",
                table: "Fornitura",
                column: "CodiceFornitore");

            migrationBuilder.CreateIndex(
                name: "IX_Impiegato",
                table: "Impiegato",
                column: "AziendaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Impiegato_UserID",
                table: "Impiegato",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OrdineForniture_CodiceFornitura",
                table: "OrdineForniture",
                column: "CodiceFornitura");

            migrationBuilder.CreateIndex(
                name: "IX_Ordini_Operatore",
                table: "Ordini",
                column: "Operatore");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottoAssemblato_CodiceProgetto",
                table: "ProdottoAssemblato",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_ProdottoCase_CodiceProgetto",
                table: "ProdottoCase",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_Progetto_Cliente",
                table: "Progetto",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Progetto_ProjectManager",
                table: "Progetto",
                column: "ProjectManager");

            migrationBuilder.CreateIndex(
                name: "IX_Prototipo_CodiceProgetto",
                table: "Prototipo",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_RichiestaProgetto_Cliente",
                table: "RichiestaProgetto",
                column: "Cliente");

            migrationBuilder.CreateIndex(
                name: "IX_RichiestaProgetto_CodiceProgetto",
                table: "RichiestaProgetto",
                column: "CodiceProgetto");

            migrationBuilder.CreateIndex(
                name: "IX_RichiestaProgetto_Operatore",
                table: "RichiestaProgetto",
                column: "Operatore");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Operatore",
                table: "Test",
                column: "Operatore");

            migrationBuilder.CreateIndex(
                name: "IX_TestProdottoAssemblato_CodiceProdottoAssemblato",
                table: "TestProdottoAssemblato",
                column: "CodiceProdottoAssemblato");

            migrationBuilder.CreateIndex(
                name: "IX_TestProdottoCase_CodiceProdottoCase",
                table: "TestProdottoCase",
                column: "CodiceProdottoCase");

            migrationBuilder.CreateIndex(
                name: "IX_TestPrototipo_NumPrototipo_CodiceProgetto",
                table: "TestPrototipo",
                columns: new[] { "NumPrototipo", "CodiceProgetto" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalisiDiMercatoProgetti");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Componente");

            migrationBuilder.DropTable(
                name: "OrdineForniture");

            migrationBuilder.DropTable(
                name: "RichiestaProgetto");

            migrationBuilder.DropTable(
                name: "TestProdottoAssemblato");

            migrationBuilder.DropTable(
                name: "TestProdottoCase");

            migrationBuilder.DropTable(
                name: "TestPrototipo");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Fornitura");

            migrationBuilder.DropTable(
                name: "Ordini");

            migrationBuilder.DropTable(
                name: "ProdottoAssemblato");

            migrationBuilder.DropTable(
                name: "ProdottoCase");

            migrationBuilder.DropTable(
                name: "Prototipo");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "Fornitore");

            migrationBuilder.DropTable(
                name: "Progetto");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Impiegato");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
