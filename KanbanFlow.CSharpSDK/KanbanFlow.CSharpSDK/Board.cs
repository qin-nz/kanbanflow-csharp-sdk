using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
   public  class Board
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("columns")]
        public Column[] Columns { get; set; }
        [JsonProperty("swimlanes")]
        public Swimlane[] Swimlanes { get; set; }
    }
}
