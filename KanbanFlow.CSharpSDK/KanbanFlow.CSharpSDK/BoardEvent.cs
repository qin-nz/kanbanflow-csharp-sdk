using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
    public class BoardEvent
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public string TimestampString
        {
            get
            {
                return Timestamp.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
            }
            set
            {
                Timestamp = DateTimeOffset.Parse(value).ToLocalTime();
            }
        }

        [JsonIgnore]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("detailedEvents")]
        public DetailedEvent[] DetailedEvents { get; set; }
    }
}
