using Serilog;
using WebsiteStatus;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"D:\Logs\LogFile.txt")
    .CreateLogger();
IHost host;
try
{
    Log.Information("Starting up the service");
    host = Host.CreateDefaultBuilder(args)
    .UseWindowsService() //to run as windows service
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    host.Run();
}
catch (Exception exception)
{
    Log.Fatal(exception, "There was a problem starting the service");
    return;
}
finally
{
    Log.CloseAndFlush(); 
}


