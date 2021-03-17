using System.Runtime.Serialization;

namespace EventHubDemoSender.Dto
{
    [DataContract]
    public class CardStatusEvent
    {
        [DataMember] 
        public string Name { get; set; }
        [DataMember] 
        public string Status { get; set; }
        [DataMember] 
        public string Message { get; set; }
    }
}