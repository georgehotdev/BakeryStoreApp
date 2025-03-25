using Discount.API.WorkerServices;
using Discount.Infrastructure;
using Discount.Infrastructure.Configuration;
using Discount.Infrastructure.Interfaces;
using Discount.Infrastructure.Repository;
using Discount.Infrastructure.Repository.Interfaces;
using Discount.Infrastructure.Settings;
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
    services.Configure<DiscountDatabaseSettings>(configuration.GetSection(nameof(DiscountDatabaseSettings)));
    services.AddSingleton<IDiscountDatabaseSettings>(opt => opt.GetRequiredService<IOptions<DiscountDatabaseSettings>>().Value);
    services.AddTransient<IDiscountContext, DiscountContext>();
    services.AddTransient<IDiscountRepository, DiscountRepository>();
    services.AddTransient<IDiscountWorkerService, DiscountWorkerService>();
}


void ConfigureServices()
{
    DiscountModelBuilder.Configure();
}