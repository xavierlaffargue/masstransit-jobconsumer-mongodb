using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Worker.Message;
using MassTransit;

namespace CreateMessageApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "";

            Console.WriteLine("Sending...");

            var busControl = Bus.Factory.CreateUsingAzureServiceBus(cfg => {
                cfg.Host(connectionString);
            });

            await busControl.StartAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            try
            {
                var endpoint = await busControl.GetSendEndpoint(new Uri("queue:queue-sample"));

                await endpoint.Send<ISampleMessage>(new
                {
                    ids = new[] { 1,2,3 },
                    IsValid = true
                });
            }
            finally
            {
                await busControl.StopAsync();
            }

            Console.WriteLine("Message sended");
            Console.ReadKey();
        }
    }
}
