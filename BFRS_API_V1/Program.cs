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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddOData(options =>
                options.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors();

//DbContext
builder.Services.AddDbContext<BFRS_dbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BFRSDB")));

// C?u h�nh Memory Cache
builder.Services.AddMemoryCache();

//Repositories
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IBirdRepository, BirdRepository>();
builder.Services.AddScoped<IBirdSpeciesRepository, BirdSpeciesRepository>();
builder.Services.AddScoped<IBirdTypeRepository, BirdTypeRepository>();
builder.Services.AddScoped<IBreedingCheckListDetailRepository, BreedingCheckListDetailRepository>();
builder.Services.AddScoped<IBreedingNormRepository, BreedingNormRepository>();
builder.Services.AddScoped<IBreedingReasonRepository, BreedingReasonRepository>();
builder.Services.AddScoped<IBreedingRepository, BreedingRepository>();
builder.Services.AddScoped<ICageRepository, CageRepository>();
builder.Services.AddScoped<ICheckListDetailRepository, CheckListDetailRepository>();
builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<IClutchReasonRepository, ClutchReasonRepository>();
builder.Services.AddScoped<IClutchRepository, ClutchRepository>();
builder.Services.AddScoped<IEggReasonRepository, EggReasonRepository>();
builder.Services.AddScoped<IEggRepository, EggRepository>();
builder.Services.AddScoped<IFarmRepository, FarmRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IIssueTypeRepository, IssueTypeRepository>();
builder.Services.AddScoped<IMutationRepository, MutationRepository>();
builder.Services.AddScoped<ISpeciesMutationRepository, SpeciesMutationRepository>();

//Services
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

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    ;

});
app.MapControllers();

app.Run();
