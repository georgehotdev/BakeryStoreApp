using Discount.API.WorkerServices;
using Discount.Grpc.Services;
using Discount.Grpc.WorkerServices;
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

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();

    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

void RegisterServices(IServiceCollection services, ConfigurationManager configuration)
{
    services.AddGrpc();
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