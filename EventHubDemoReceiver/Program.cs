using System;
using System.Threading.Tasks;
using System.Text;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Storage.Blobs;
using Azure.Messaging.EventHubs.Processor;

namespace EventHubDemoReceiver
{
    class Program
    {
        private const string EhubNamespaceConnectionString = "";
        private const string EventHubName = "";
        private const string BlobStorageConnectionString = "";
        private const string BlobContainerName = "";
        
        static async Task Main(string[] args)
        {
            Console.WriteLine("Event Hubs Receiver");
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use 
            BlobContainerClient storageClient = new BlobContainerClient(BlobStorageConnectionString, BlobContainerName);

            // Create an event processor client to process events in the event hub
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, EhubNamespaceConnectionString, EventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += Handlers.ProcessorHandlers.ProcessEventHandler;
            processor.ProcessErrorAsync += Handlers.ProcessorHandlers.ProcessErrorHandler;

            // Start the processing
            await processor.StartProcessingAsync();

            // Wait for 10 seconds for the events to be processed
            await Task.Delay(TimeSpan.FromSeconds(10));

            // Stop the processing
            await processor.StopProcessingAsync();
            
            Console.WriteLine("Event Received");
        }
    }
}