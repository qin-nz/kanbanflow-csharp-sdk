using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Date
    {
        [DefaultValue("dueDate")]
        [JsonProperty("dateType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DateType { get; set; } = "dueDate";


        [DefaultValue("active")]
        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Status { get; set; } = "active";


        [JsonProperty("dueTimestamp", Required = Required.AllowNull)]
        public string DueTimestamp { get; set; }


        [JsonProperty("dueTimestampLocal")]
        public string DueTimestampLocal { get; set; }


        [JsonProperty("targetColumnId", Required = Required.Always)]
        public string TargetColumnId { get; set; }
    }
}
