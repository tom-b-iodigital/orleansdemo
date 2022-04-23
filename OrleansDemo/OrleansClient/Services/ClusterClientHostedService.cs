using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace OrleansClient.Services
{
    public class ClusterClientHostedService : IHostedService
    {
        public IClusterClient Client { get; }

        public ClusterClientHostedService(ILoggerProvider loggerProvider, IConfiguration configuration)
        {
            Client = new ClientBuilder()
                .UseAzureStorageClustering((options) =>
                {
                    string connectionString = configuration.GetConnectionString("CosmosBDConnectionString");
                    options.ConfigureTableServiceClient(connectionString);
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "orleans_demo_cluster";
                    options.ServiceId = "1";
                })
                .ConfigureLogging(builder => builder.AddProvider(loggerProvider))
                .Build();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // A retry filter could be provided here.
            await Client.Connect();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Client.Close();

            Client.Dispose();
        }
    }
}
