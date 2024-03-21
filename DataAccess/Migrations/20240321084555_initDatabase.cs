using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class initDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "BirdType",
                columns: table => new
                {
                    BirdTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirdTypeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdType", x => x.BirdTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Farm",
                columns: table => new
                {
                    FarmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farm", x => x.FarmId);
                });

            migrationBuilder.CreateTable(
                name: "IssueType",
                columns: table => new
                {
                    IssueTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueType", x => x.IssueTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Mutation",
                columns: table => new
                {
                    MutationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MutationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mutation", x => x.MutationId);
                });

            migrationBuilder.CreateTable(
                name: "BirdSpecies",
                columns: table => new
                {
                    BirdSpeciesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirdSpeciesName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BirdTypeId = table.Column<int>(type: "int", nullable: true),
                    HatchingPhaseFrom = table.Column<int>(type: "int", nullable: true),
                    HatchingPhaseTo = table.Column<int>(type: "int", nullable: true),
                    NestlingPhaseFrom = table.Column<int>(type: "int", nullable: true),
                    NestlingPhaseTo = table.Column<int>(type: "int", nullable: true),
                    ChickPhaseFrom = table.Column<int>(type: "int", nullable: true),
                    ChickPhaseTo = table.Column<int>(type: "int", nullable: true),
                    FledgingPhaseFrom = table.Column<int>(type: "int", nullable: true),
                    FledgingPhaseTo = table.Column<int>(type: "int", nullable: true),
                    JuvenilePhaseFrom = table.Column<int>(type: "int", nullable: true),
                    JuvenilePhaseTo = table.Column<int>(type: "int", nullable: true),
                    ImmaturePhaseFrom = table.Column<int>(type: "int", nullable: true),
                    ImmaturePhaseTo = table.Column<int>(type: "int", nullable: true),
                    AdultPhaseFrom = table.Column<int>(type: "int", nullable: true),
                    AdultPhaseTo = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BirdSpec__D9DA595F3082182C", x => x.BirdSpeciesId);
                    table.ForeignKey(
                        name: "FK_BirdTypeBirdSpecies",
                        column: x => x.BirdTypeId,
                        principalTable: "BirdType",
                        principalColumn: "BirdTypeId");
                });

            migrationBuilder.CreateTable(
                name: "Area",
                columns: table => new
                {
                    AreaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Area", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_FarmArea",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "FarmId");
                });

            migrationBuilder.CreateTable(
                name: "BreedingNorm",
                columns: table => new
                {
                    BreedingNormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirdSpeciesId = table.Column<int>(type: "int", nullable: true),
                    BreedingStartMonth = table.Column<DateTime>(type: "date", nullable: true),
                    BreedingEndMonth = table.Column<DateTime>(type: "date", nullable: true),
                    WeatherFeatures = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FoodRecommendation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PairingDurationMin = table.Column<double>(type: "float", nullable: true),
                    PairingDurationMax = table.Column<double>(type: "float", nullable: true),
                    NestingDurationMin = table.Column<double>(type: "float", nullable: true),
                    NestingDurationMax = table.Column<double>(type: "float", nullable: true),
                    IncubatingDurationMin = table.Column<double>(type: "float", nullable: true),
                    IncubatingDurationMax = table.Column<double>(type: "float", nullable: true),
                    NestTemperatureMin = table.Column<double>(type: "float", nullable: true),
                    NestTemperatureMax = table.Column<double>(type: "float", nullable: true),
                    NestHumidityMin = table.Column<double>(type: "float", nullable: true),
                    NestHumidityMax = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedingNorm", x => x.BreedingNormId);
                    table.ForeignKey(
                        name: "FK_BirdSpeciesBreedingNorm",
                        column: x => x.BirdSpeciesId,
                        principalTable: "BirdSpecies",
                        principalColumn: "BirdSpeciesId");
                });

            migrationBuilder.CreateTable(
                name: "CheckList",
                columns: table => new
                {
                    CheckListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CheckListName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SpeciesId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckList", x => x.CheckListId);
                    table.ForeignKey(
                        name: "FK_SpeciesCheckList",
                        column: x => x.SpeciesId,
                        principalTable: "BirdSpecies",
                        principalColumn: "BirdSpeciesId");
                });

            migrationBuilder.CreateTable(
                name: "SpeciesMutation",
                columns: table => new
                {
                    BirdSpeciesId = table.Column<int>(type: "int", nullable: false),
                    MutationId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeciesMutation", x => new { x.BirdSpeciesId, x.MutationId });
                    table.ForeignKey(
                        name: "FK_BirdSpeciesSM",
                        column: x => x.BirdSpeciesId,
                        principalTable: "BirdSpecies",
                        principalColumn: "BirdSpeciesId");
                    table.ForeignKey(
                        name: "FK_MutationSM",
                        column: x => x.MutationId,
                        principalTable: "Mutation",
                        principalColumn: "MutationId");
                });

            migrationBuilder.CreateTable(
                name: "Cage",
                columns: table => new
                {
                    CageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturedDate = table.Column<DateTime>(type: "date", nullable: true),
                    ManufacturedAt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PurchasedDate = table.Column<DateTime>(type: "date", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cage", x => x.CageId);
                    table.ForeignKey(
                        name: "FK_AccountCage",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_AreaCage",
                        column: x => x.AreaId,
                        principalTable: "Area",
                        principalColumn: "AreaId");
                });

            migrationBuilder.CreateTable(
                name: "CheckListDetail",
                columns: table => new
                {
                    CheckListDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckListId = table.Column<int>(type: "int", nullable: true),
                    QuestionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListDetail", x => x.CheckListDetailId);
                    table.ForeignKey(
                        name: "FK_CheckListDetail",
                        column: x => x.CheckListId,
                        principalTable: "CheckList",
                        principalColumn: "CheckListId");
                });

            migrationBuilder.CreateTable(
                name: "Bird",
                columns: table => new
                {
                    BirdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    HatchedDate = table.Column<DateTime>(type: "date", nullable: true),
                    PurchaseFrom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AcquisitionDate = table.Column<DateTime>(type: "date", nullable: true),
                    BirdSpeciesId = table.Column<int>(type: "int", nullable: true),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    FarmId = table.Column<int>(type: "int", nullable: true),
                    FatherBirdId = table.Column<int>(type: "int", nullable: true),
                    MotherBirdId = table.Column<int>(type: "int", nullable: true),
                    BandNumber = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    LifeStage = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bird", x => x.BirdId);
                    table.ForeignKey(
                        name: "FK_BirdSpeciesBird",
                        column: x => x.BirdSpeciesId,
                        principalTable: "BirdSpecies",
                        principalColumn: "BirdSpeciesId");
                    table.ForeignKey(
                        name: "FK_CageBird",
                        column: x => x.CageId,
                        principalTable: "Cage",
                        principalColumn: "CageId");
                    table.ForeignKey(
                        name: "FK_FarmBird",
                        column: x => x.FarmId,
                        principalTable: "Farm",
                        principalColumn: "FarmId");
                    table.ForeignKey(
                        name: "FK_FatherBird",
                        column: x => x.FatherBirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                    table.ForeignKey(
                        name: "FK_MotherBird",
                        column: x => x.MotherBirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                });

            migrationBuilder.CreateTable(
                name: "BirdMutation",
                columns: table => new
                {
                    BirdId = table.Column<int>(type: "int", nullable: false),
                    MutationId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdMutation", x => new { x.BirdId, x.MutationId });
                    table.ForeignKey(
                        name: "FK_BirdBirdMutation",
                        column: x => x.BirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                    table.ForeignKey(
                        name: "FK_MutationBirdMutation",
                        column: x => x.MutationId,
                        principalTable: "Mutation",
                        principalColumn: "MutationId");
                });

            migrationBuilder.CreateTable(
                name: "Breeding",
                columns: table => new
                {
                    BreedingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherBirdId = table.Column<int>(type: "int", nullable: true),
                    MotherBirdId = table.Column<int>(type: "int", nullable: true),
                    CoupleSeperated = table.Column<bool>(type: "bit", nullable: true),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    NextCheck = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeding", x => x.BreedingId);
                    table.ForeignKey(
                        name: "FK_CageBreeding",
                        column: x => x.CageId,
                        principalTable: "Cage",
                        principalColumn: "CageId");
                    table.ForeignKey(
                        name: "FK_CreatedByBreeding",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_FatherBirdBreeding",
                        column: x => x.FatherBirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                    table.ForeignKey(
                        name: "FK_MotherBirdBreeding",
                        column: x => x.MotherBirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                    table.ForeignKey(
                        name: "FK_UpdatedByBreeding",
                        column: x => x.UpdatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "BreedingCheckListDetail",
                columns: table => new
                {
                    BreedingCheckListDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedingId = table.Column<int>(type: "int", nullable: true),
                    CheckListDetailId = table.Column<int>(type: "int", nullable: true),
                    CheckDate = table.Column<DateTime>(type: "date", nullable: true),
                    CheckValue = table.Column<int>(type: "int", nullable: true),
                    Compulsory = table.Column<bool>(type: "bit", nullable: true),
                    Positive = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedingCheckListDetail", x => x.BreedingCheckListDetailId);
                    table.ForeignKey(
                        name: "FK_BreedingBreedingCheckListDetail",
                        column: x => x.BreedingId,
                        principalTable: "Breeding",
                        principalColumn: "BreedingId");
                    table.ForeignKey(
                        name: "FK_CheckListDetailBreedingCheckListDetail",
                        column: x => x.CheckListDetailId,
                        principalTable: "CheckListDetail",
                        principalColumn: "CheckListDetailId");
                });

            migrationBuilder.CreateTable(
                name: "BreedingReason",
                columns: table => new
                {
                    BreedingReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedingId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreedingReason", x => x.BreedingReasonId);
                    table.ForeignKey(
                        name: "FK_BreedingBreedingReason",
                        column: x => x.BreedingId,
                        principalTable: "Breeding",
                        principalColumn: "BreedingId");
                    table.ForeignKey(
                        name: "FK_CreatedByBreedingReason",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Clutch",
                columns: table => new
                {
                    ClutchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedingId = table.Column<int>(type: "int", nullable: true),
                    BroodStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    BroodEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    CageId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clutch", x => x.ClutchId);
                    table.ForeignKey(
                        name: "FK_BreedingClutch",
                        column: x => x.BreedingId,
                        principalTable: "Breeding",
                        principalColumn: "BreedingId");
                    table.ForeignKey(
                        name: "FK_CageClutch",
                        column: x => x.CageId,
                        principalTable: "Cage",
                        principalColumn: "CageId");
                    table.ForeignKey(
                        name: "FK_CreatedByClutch",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_UpdatedByClutch",
                        column: x => x.UpdatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Issue",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BreedingId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    AssignedTo = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    IssueTypeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issue", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK_AssignedToIssue",
                        column: x => x.AssignedTo,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_BreedingIssue",
                        column: x => x.BreedingId,
                        principalTable: "Breeding",
                        principalColumn: "BreedingId");
                    table.ForeignKey(
                        name: "FK_CreatedByIssue",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_TypeIssue",
                        column: x => x.IssueTypeId,
                        principalTable: "IssueType",
                        principalColumn: "IssueTypeId");
                    table.ForeignKey(
                        name: "FK_UpdatedByIssue",
                        column: x => x.UpdatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "ClutchReason",
                columns: table => new
                {
                    ClutchReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClutchId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClutchReason", x => x.ClutchReasonId);
                    table.ForeignKey(
                        name: "FK_ClutchClutchReason",
                        column: x => x.ClutchId,
                        principalTable: "Clutch",
                        principalColumn: "ClutchId");
                    table.ForeignKey(
                        name: "FK_CreatedByClutchReason",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "Egg",
                columns: table => new
                {
                    EggId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClutchId = table.Column<int>(type: "int", nullable: true),
                    LayDate = table.Column<DateTime>(type: "date", nullable: true),
                    HatchedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egg", x => x.EggId);
                    table.ForeignKey(
                        name: "FK_ClutchEgg",
                        column: x => x.ClutchId,
                        principalTable: "Clutch",
                        principalColumn: "ClutchId");
                    table.ForeignKey(
                        name: "FK_CreatedByEgg",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_UpdatedByEgg",
                        column: x => x.UpdatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "EggBird",
                columns: table => new
                {
                    EggId = table.Column<int>(type: "int", nullable: false),
                    BirdId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EggBird", x => new { x.EggId, x.BirdId });
                    table.ForeignKey(
                        name: "FK_BirdEggBird",
                        column: x => x.BirdId,
                        principalTable: "Bird",
                        principalColumn: "BirdId");
                    table.ForeignKey(
                        name: "FK_EggEggBird",
                        column: x => x.EggId,
                        principalTable: "Egg",
                        principalColumn: "EggId");
                });

            migrationBuilder.CreateTable(
                name: "EggReason",
                columns: table => new
                {
                    EggReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EggId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EggReason", x => x.EggReasonId);
                    table.ForeignKey(
                        name: "FK_CreatedByEggReason",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                    table.ForeignKey(
                        name: "FK_EggEggReason",
                        column: x => x.EggId,
                        principalTable: "Egg",
                        principalColumn: "EggId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Area_FarmId",
                table: "Area",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_BirdSpeciesId",
                table: "Bird",
                column: "BirdSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_CageId",
                table: "Bird",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_FarmId",
                table: "Bird",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_FatherBirdId",
                table: "Bird",
                column: "FatherBirdId");

            migrationBuilder.CreateIndex(
                name: "IX_Bird_MotherBirdId",
                table: "Bird",
                column: "MotherBirdId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdMutation_MutationId",
                table: "BirdMutation",
                column: "MutationId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdSpecies_BirdTypeId",
                table: "BirdSpecies",
                column: "BirdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeding_CageId",
                table: "Breeding",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeding_CreatedBy",
                table: "Breeding",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Breeding_FatherBirdId",
                table: "Breeding",
                column: "FatherBirdId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeding_MotherBirdId",
                table: "Breeding",
                column: "MotherBirdId");

            migrationBuilder.CreateIndex(
                name: "IX_Breeding_UpdatedBy",
                table: "Breeding",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingCheckListDetail_BreedingId",
                table: "BreedingCheckListDetail",
                column: "BreedingId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingCheckListDetail_CheckListDetailId",
                table: "BreedingCheckListDetail",
                column: "CheckListDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingNorm_BirdSpeciesId",
                table: "BreedingNorm",
                column: "BirdSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingReason_BreedingId",
                table: "BreedingReason",
                column: "BreedingId");

            migrationBuilder.CreateIndex(
                name: "IX_BreedingReason_CreatedBy",
                table: "BreedingReason",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Cage_AccountId",
                table: "Cage",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Cage_AreaId",
                table: "Cage",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckList_SpeciesId",
                table: "CheckList",
                column: "SpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListDetail_CheckListId",
                table: "CheckListDetail",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_Clutch_BreedingId",
                table: "Clutch",
                column: "BreedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Clutch_CageId",
                table: "Clutch",
                column: "CageId");

            migrationBuilder.CreateIndex(
                name: "IX_Clutch_CreatedBy",
                table: "Clutch",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Clutch_UpdatedBy",
                table: "Clutch",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ClutchReason_ClutchId",
                table: "ClutchReason",
                column: "ClutchId");

            migrationBuilder.CreateIndex(
                name: "IX_ClutchReason_CreatedBy",
                table: "ClutchReason",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Egg_ClutchId",
                table: "Egg",
                column: "ClutchId");

            migrationBuilder.CreateIndex(
                name: "IX_Egg_CreatedBy",
                table: "Egg",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Egg_UpdatedBy",
                table: "Egg",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EggBird_BirdId",
                table: "EggBird",
                column: "BirdId");

            migrationBuilder.CreateIndex(
                name: "IX_EggReason_CreatedBy",
                table: "EggReason",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_EggReason_EggId",
                table: "EggReason",
                column: "EggId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_AssignedTo",
                table: "Issue",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_BreedingId",
                table: "Issue",
                column: "BreedingId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_CreatedBy",
                table: "Issue",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_IssueTypeId",
                table: "Issue",
                column: "IssueTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Issue_UpdatedBy",
                table: "Issue",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SpeciesMutation_MutationId",
                table: "SpeciesMutation",
                column: "MutationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirdMutation");

            migrationBuilder.DropTable(
                name: "BreedingCheckListDetail");

            migrationBuilder.DropTable(
                name: "BreedingNorm");

            migrationBuilder.DropTable(
                name: "BreedingReason");

            migrationBuilder.DropTable(
                name: "ClutchReason");

            migrationBuilder.DropTable(
                name: "EggBird");

            migrationBuilder.DropTable(
                name: "EggReason");

            migrationBuilder.DropTable(
                name: "Issue");

            migrationBuilder.DropTable(
                name: "SpeciesMutation");

            migrationBuilder.DropTable(
                name: "CheckListDetail");

            migrationBuilder.DropTable(
                name: "Egg");

            migrationBuilder.DropTable(
                name: "IssueType");

            migrationBuilder.DropTable(
                name: "Mutation");

            migrationBuilder.DropTable(
                name: "CheckList");

            migrationBuilder.DropTable(
                name: "Clutch");

            migrationBuilder.DropTable(
                name: "Breeding");

            migrationBuilder.DropTable(
                name: "Bird");

            migrationBuilder.DropTable(
                name: "BirdSpecies");

            migrationBuilder.DropTable(
                name: "Cage");

            migrationBuilder.DropTable(
                name: "BirdType");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Area");

            migrationBuilder.DropTable(
                name: "Farm");
        }
    }
}
