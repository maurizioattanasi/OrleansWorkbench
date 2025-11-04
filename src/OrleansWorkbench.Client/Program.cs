using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OrleansWorkbench.Application.Interfaces;

var builder = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(client =>
    {
        client.UseLocalhostClustering();
    })
    .ConfigureLogging(logging => logging.AddConsole())
    .UseConsoleLifetime();

using var host = builder.Build();
await host.StartAsync().ConfigureAwait(false);

var client = host.Services.GetRequiredService<IClusterClient>();

while (true)
{
    Console.WriteLine("Please enter a robot name");

    var grainId = Console.ReadLine();

    var grain = client.GetGrain<IRobotGrain>(grainId ?? "my-robot");

    Console.WriteLine(value: "Please, enter an instruction");

    var instruction = Console.ReadLine();

    await grain.AddInstruction(instruction!).ConfigureAwait(false);
    var count = await grain.GetInstructionCountAsync().ConfigureAwait(false);
    Console.WriteLine($"{grainId} has {count} instruction(s).");
}



