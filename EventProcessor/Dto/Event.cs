using System.Runtime.Serialization;

namespace EventProcessor.Dto
{
    [DataContract]
        public class Event
        {
            [DataMember]
            public string Message { get; set; }
        }
    }