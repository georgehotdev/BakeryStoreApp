using Basket.API.Configuration;
using Basket.API.Gateways;
using Basket.API.GrpcServices;
using Basket.API.WorkerServices.Interfaces;
using Basket.Infrastructure;
using Basket.Infrastructure.Interfaces;
using Discount.Grpc.Protos;
using Microsoft.OpenApi.Models;
using Polly;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

RegisterServices(builder.Services, builder.Configuration);

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
    SetupGateway("DiscountService", services, configuration);

    services.Configure<DiscountServiceEndpoints>(configuration.GetSection("DiscountService"));
    services.AddScoped<IDiscountServiceGateway, DiscountServiceGateway>();
    
    services.AddScoped<IBasketWorkerService, BasketWorkerService>();
    services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
    });

    services.AddScoped<IBasketRepository, BasketRepository>();
    services.AddScoped<DiscountGrpcService>();

    services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
        o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
    services.AddControllers();
    services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" }); });

    services.AddSingleton<ConnectionMultiplexer>(sp =>
    {
        var connectionConfiguration =
            ConfigurationOptions.Parse(configuration.GetConnectionString("CacheSettings"), true);
        return ConnectionMultiplexer.Connect(connectionConfiguration);
    });
}


void SetupGateway(string serviceName, IServiceCollection serviceCollection, ConfigurationManager configuration)
{
    serviceCollection.AddHttpClient(serviceName,
            client => { client.BaseAddress = new Uri(configuration.GetValue<string>($"{serviceName}:BaseUrl")); })
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));
}