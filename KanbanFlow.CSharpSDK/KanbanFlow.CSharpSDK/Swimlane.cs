using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
   public  class Swimlane
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
   
    }
}
