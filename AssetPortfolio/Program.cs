using AssetPortfolio.Application.Handlers.QueryHandler.Assets;
using AssetPortfolio.Core.IRepositories.Asset;
using AssetPortfolio.Core.IRepositories.AssetType;
using AssetPortfolio.Infrastructure.Data;
using AssetPortfolio.Infrastructure.Repositories.Asset;
using AssetPortfolio.Infrastructure.Repositories.AssetType;
using AssetPortfolio.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.Enrich.FromLogContext()
	.Enrich.WithMachineName()
	.Enrich.WithThreadId()
	.WriteTo.MSSqlServer(
		connectionString: builder.Configuration.GetConnectionString("AssetPortfolioDB"),
		tableName: "AuditLog",
		autoCreateSqlTable: true
	)
	.CreateLogger();
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AssetPortfolioDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AssetPortfolioDB")));
builder.Services.AddMediatR(Config =>
	Config.RegisterServicesFromAssembly(typeof(GetAllAssetsHandler).Assembly));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAssetRepository, AssetRepository>();
builder.Services.AddScoped<IAssetTypeRepository,AssetTypeRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
