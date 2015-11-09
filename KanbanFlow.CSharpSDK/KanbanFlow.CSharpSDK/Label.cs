using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
   public class Label
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("pinned")]
        public bool Pinned { get; set; }
    }
}
