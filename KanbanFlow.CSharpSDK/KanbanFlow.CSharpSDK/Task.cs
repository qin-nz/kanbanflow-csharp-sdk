using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
    public class Task
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string  Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("columnId")]
        public string ColumnId { get; set; }

        [JsonProperty("swimlaneId")]
        public string SwimlaneId { get; set; }

        [JsonProperty ("number")]
        public object Number { get; set; }

        [JsonProperty("responsibleUserId")]
        public string responsibleUserId { get; set; }

        [JsonProperty("totalSecondsEstimate")]
        public int TotalSecondsEstimate { get; set; }

        [JsonProperty("totalSecondsSpent")]
        public int TotalSecondsSpent { get; set; }

        /// <summary>
        /// Optional. Can only be used if column is date grouped. Valid format is YYYY-MM-DD, e.g. 2010-12-31. Use null or empty string if you want it grouped as unknown date. If not set and column date grouped, then it will automatically use today's date for the GMT timezone.
        /// </summary>
        [JsonProperty("dateGrouping")]
        public string DateGrouping { get; set; }
    }
}
