using System.Collections.Generic;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Cell
    {
        [JsonProperty("columnId")]
        public string ColumnId { get; set; }

        [JsonProperty("columnName")]
        public string ColumnName { get; set; }

        [JsonProperty("swimlaneId")]
        public string SwimlaneId { get; set; }

        [JsonProperty("SwimlaneName")]
        public string SwimlaneName { get; set; }

        [JsonProperty("tasks")]
        public List<Task> Tasks { get; set; }

    }
}
