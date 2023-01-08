using CurrencyConverter.Integrations.ExchangeRatesAPI;
using CurrencyConverter.Integrations.ExchangeRatesAPI.Interfaces;
using CurrencyConverter.Services;
using CurrencyConverter.Services.Interfaces;

namespace CurrencyConverter;

public class Startup
{
    public IConfiguration Configuration
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IQuotesService, QuotesService>();
        services.AddSingleton<IExchangeRatesClient, ExchangeRatesClient>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}