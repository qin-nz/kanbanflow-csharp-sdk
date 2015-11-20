using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
    public class ChangedProperty
    {
        [JsonProperty("property")]
        public string property { get; set; }
        [JsonProperty("oldValue")]
        public object OldValue { get; set; }
        [JsonProperty("newValue")]
        public object NewValue { get; set; }
    }
}
