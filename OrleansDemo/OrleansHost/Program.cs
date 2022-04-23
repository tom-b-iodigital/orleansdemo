using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansDemo.Domain.Grains;
using OrleansDemo.Domain.Grains.Implementations;

// read the configuration file
IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddCommandLine(args)
        .Build();

// We need a silo to connect to, start one now. If you have an external Silo running then you can skip this line
await CreateSampleSiloHost(config).StartAsync();

// Create an orleans client that connects to our Silo
using var orleansClient = new ClientBuilder()
        .UseAzureStorageClustering((options) =>
        {
            string connectionString = config.GetConnectionString("CosmosBDConnectionString");
            options.ConfigureTableServiceClient(connectionString);
        })
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "orleans_demo_cluster";
            options.ServiceId = "1";
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .Build();

// Create our appBuilder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
// Register our client as a GrainFactory
builder.Services.AddSingleton(c => (IGrainFactory)orleansClient);
// Add services required for the OrleansDashboard middleware
builder.Services.AddServicesForSelfHostedDashboard();

var app = builder.Build();
// Connect our client
await orleansClient.Connect();

// Register our middleware
app.UseOrleansDashboard();

app.Run();


// A helper method used to create a sample silo
static ISiloHost CreateSampleSiloHost(IConfigurationRoot config)
    => new SiloHostBuilder()
        .UseAzureStorageClustering((options) =>
        {
            string connectionString = config.GetConnectionString("CosmosBDConnectionString");
            options.ConfigureTableServiceClient(connectionString);
        })
        .AddAzureTableGrainStorageAsDefault((options) =>
        {
            string connectionString = config.GetConnectionString("CosmosBDConnectionString");
            options.ConfigureTableServiceClient(connectionString);
            options.UseJson = true;
        })
        .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
        .UseDashboard(options =>
        {
            options.HostSelf = false;
        })
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "orleans_demo_cluster";
            options.ServiceId = "1";
        })
        .Build();