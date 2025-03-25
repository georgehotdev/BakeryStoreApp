using BakeryStore.API.Configuration;
using BakeryStore.API.Gateways;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services, builder.Configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();


void ConfigureServices(IServiceCollection builderServices, ConfigurationManager builderConfiguration)
{
    SetupGateway("CatalogService", builderServices, builderConfiguration);
    SetupGateway("DiscountService", builderServices, builderConfiguration);
    SetupGateway("BasketService", builderServices, builderConfiguration);

    builderServices.Configure<CatalogServiceEndpoints>(builderConfiguration.GetSection("CatalogService"));
    builderServices.Configure<DiscountServiceEndpoints>(builderConfiguration.GetSection("DiscountService"));
    builderServices.Configure<BasketServiceEndpoints>(builderConfiguration.GetSection("BasketService"));
    builderServices.AddScoped<ICatalogServiceGateway, CatalogServiceGateway>();
    builderServices.AddScoped<IDiscountServiceGateway, DiscountServiceGateway>();
    builderServices.AddScoped<IBasketServiceGateway, BasketServiceGateway>();
}

void SetupGateway(string serviceName, IServiceCollection serviceCollection, ConfigurationManager configuration)
{
    serviceCollection.AddHttpClient(serviceName,
            client => { client.BaseAddress = new Uri(configuration.GetValue<string>($"{serviceName}:BaseUrl")); })
        .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)))
        .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
}