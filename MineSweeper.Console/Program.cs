// See https://aka.ms/new-console-template for more information

using GIC_Minesweeper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;


var configBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("AppSettings.json", optional: false);

IConfiguration config = configBuilder.Build();

IServiceCollection services = new ServiceCollection();

// Add & Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.File(Path.Combine(Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName, "logs/logMinesweeper.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Registering the dependencies
var serviceProvider = services.AddServices(config).BuildServiceProvider();


var mineSweeper = serviceProvider.GetService<GIC_Minesweeper.MineSweeper>();

try
{
    mineSweeper?.Start();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
finally
{
    // Ensure Serilog flushes any remaining logs before application exits
    Log.CloseAndFlush();
}
