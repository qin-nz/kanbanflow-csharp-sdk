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
        public string DueTimestampString
        {
            get
            {
                return DueTimestamp.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ");
            }
            set
            {
                DueTimestamp = DateTimeOffset.Parse(value).ToLocalTime();
            }
        }

        [JsonProperty("dueTimestampLocal", Required = Required.Default)]
        public string DueTimestampLocalString
        {
            get
            {
                var time = DueTimestamp.ToLocalTime();
                var date = time.ToString("yyyy-MM-ddThh:mm:ss");
                string offset = "";
                if (time.Offset.Hours >= 0)
                {
                    offset = $"+{time.Offset.Hours.ToString("D2")}:{time.Offset.Minutes.ToString("D2")}";
                }
                else if (time.Offset.Hours < 0)
                {
                    offset = $"{time.Offset.Hours.ToString("D2")}:{time.Offset.Minutes.ToString("D2")}";
                }
                return date + offset;
            }
            set
            {
                DueTimestamp = DateTimeOffset.Parse(value).ToLocalTime();
            }
        }

        [JsonIgnore]
        public DateTimeOffset DueTimestamp { get; set; }

        [JsonProperty("targetColumnId", Required = Required.Always)]
        public string TargetColumnId { get; set; }
    }
}
