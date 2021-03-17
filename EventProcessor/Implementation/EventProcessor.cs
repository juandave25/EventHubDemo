using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using EventProcessor.Dto;

namespace EventProcessor.Implementation
{
    public class EventProcessor : IEventProcessor
    {
        public Task OpenAsync(Microsoft.ServiceBus.Messaging.PartitionContext context)
        {
            return Task.FromResult<object>(null);
        }

        public Task ProcessEventsAsync(Microsoft.ServiceBus.Messaging.PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                if (eventData.Properties["Type"].ToString() != "Event")
                    continue;
 
                var bytes = Encoding.Unicode.GetString(eventData.GetBytes());
                var data = JsonConvert.DeserializeObject<Event>(bytes);
 
                Console.WriteLine("Processing EVENT [(Message: {0}) PartitionKey: {1}] at PartitionId: {2}",
                    data.Message,
                    eventData.PartitionKey,
                    context.Lease.PartitionId);
 
                foreach (KeyValuePair<string, object> p in eventData.Properties)
                {
                    if (!p.Key.Equals("ContentType"))
                    {
                        Console.WriteLine("  [Property: {0} = {1}]", p.Key, p.Value);
                    }
                }
            }
 
            return Task.FromResult<object>(null);
        }

        public Task CloseAsync(Microsoft.ServiceBus.Messaging.PartitionContext context, CloseReason reason)
        {
            throw new NotImplementedException();
        }
    }
}