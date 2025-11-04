using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder(args)
    .UseOrleans(silo =>
    {
        silo.UseLocalhostClustering()
            .ConfigureLogging(logging => logging.AddConsole());
        silo.UseDashboard();
    })
    .UseConsoleLifetime();

using var host = builder.Build();

await host.RunAsync().ConfigureAwait(false);
