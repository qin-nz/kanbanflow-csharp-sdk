using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class DetailedEvent
    {
        [JsonProperty("eventType")]
        public string eventType { get; set; }
        [JsonProperty("taskId")]
        public string TaskId { get; set; }
        [JsonProperty("changedProperties")]
        public ChangedProperty[] ChangedProperties { get; set; }
    }
}
