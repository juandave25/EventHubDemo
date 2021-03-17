using System;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using EventHubDemoSender.Dto;

namespace EventHubDemoSender
{
    class Program
    {
        private const string ConnectionString = "NameSpace ConnectionString";
        private const string EventHubName = "Event Hub Name";
        static async Task Main(string[] args)
        {
            Console.WriteLine("Event Hub Sender");
            await using (var producerClient = new EventHubProducerClient(ConnectionString, EventHubName))
            {
                using (EventDataBatch eventBatch = await producerClient.CreateBatchAsync())
                {
                    eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(GenerateEvent())));
                    await producerClient.SendAsync(eventBatch);
                    Console.WriteLine("Batch events published!!");
                }
            }
        }

        static string GenerateEvent()
        {
            string serializedString = string.Empty;
            CardStatusEvent Event = new CardStatusEvent();
            Event.Name = "Card";
            Event.Status = "Approved";
            Event.Message = "Event Test";

            serializedString = JsonConvert.SerializeObject(Event);

            return serializedString;
        }
    }
}
