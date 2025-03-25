using Catalog.API.WorkerServices;
using Catalog.API.WorkerServices.Interfaces;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Configuration;
using Catalog.Infrastructure.Interfaces;
using Catalog.Infrastructure.Repository;
using Catalog.Infrastructure.Repository.Interfaces;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
RegisterServices(builder.Services, builder.Configuration);
ConfigureServices();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.Configure<CatalogDatabaseSettings>(configuration.GetSection(nameof(CatalogDatabaseSettings)));
    services.AddSingleton<ICatalogDatabaseSettings>(opt => opt.GetRequiredService<IOptions<CatalogDatabaseSettings>>().Value);
    services.AddTransient<ICatalogContext, CatalogContext>();
    services.AddTransient<IProductRepository, ProductRepository>();
    services.AddTransient<ICatalogWorkerService, CatalogWorkerService>();
}


void ConfigureServices()
{
    ProductModelBuilder.Configure();
}