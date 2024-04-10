using DataAccess.IRepositories.Implements;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.IService;
using BusinessObjects.IService.Implements;
using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Azure.Storage.Blobs;
using BFRS_API_V1.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddOData(options =>
                options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please Enter The Token To Authenticate The Role",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
});

builder.Services.AddScoped(_ =>
{
    return new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlogStorage"));
});

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//DbContext
builder.Services.AddDbContext<BFRS_DBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BFRSDB")));

// C?u h�nh Memory Cache
builder.Services.AddMemoryCache();

//Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IBirdMutationRepository, BirdMutationRepository>();
builder.Services.AddScoped<IBirdRepository, BirdRepository>();
builder.Services.AddScoped<IBirdSpeciesRepository, BirdSpeciesRepository>();
builder.Services.AddScoped<IBirdTypeRepository, BirdTypeRepository>();
builder.Services.AddScoped<IBreedingCheckListDetailRepository, BreedingCheckListDetailRepository>();
builder.Services.AddScoped<IBreedingCheckListRepository, BreedingCheckListRepository>();
builder.Services.AddScoped<IBreedingNormRepository, BreedingNormRepository>();
builder.Services.AddScoped<IBreedingReasonRepository, BreedingReasonRepository>();
builder.Services.AddScoped<IBreedingRepository, BreedingRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped<ICheckListDetailRepository, CheckListDetailRepository>();
builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<IClutchReasonRepository, ClutchReasonRepository>();
builder.Services.AddScoped<IClutchRepository, ClutchRepository>();
builder.Services.AddScoped<IEggBirdRepository, EggBirdRepository>();
builder.Services.AddScoped<IEggReasonRepository, EggReasonRepository>();
builder.Services.AddScoped<IEggRepository, EggRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IIssueTypeRepository, IssueTypeRepository>();
builder.Services.AddScoped<IMutationRepository, MutationRepository>();
builder.Services.AddScoped<ISpeciesMutationRepository, SpeciesMutationRepository>();

//Services
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBirdService, BirdService>();
builder.Services.AddScoped<IBirdSpeciesService, BirdSpeciesService>();
builder.Services.AddScoped<IBreedingService, BreedingService>();
builder.Services.AddScoped<ICageService, CageService>();
builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<IClutchService, ClutchService>();
builder.Services.AddScoped<IEggService, EggService>();
builder.Services.AddScoped<IFarmService, FarmService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IMutationService, MutationService>();
builder.Services.AddScoped<ICheckListDetailService, CheckListDetailService>();
builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<IBreedingCheckListDetailService, BreedingCheckListDetailService>();
builder.Services.AddScoped<IBreedingCheckListService, BreedingCheckListService>();
builder.Services.AddScoped<IFileService, FileService>();

// Mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BusinessObjects.Mapper.Mapper());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Token
var serect = builder.Configuration["AppSettings:SecretKey"];
var key = Encoding.ASCII.GetBytes(serect);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.UseSwagger();
    app.UseSwaggerUI();
/*}*/

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    ;
});

app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
