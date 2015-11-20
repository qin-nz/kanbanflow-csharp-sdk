using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Subtask
    {
        [JsonIgnore]
        public Task Parent { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("finished")]
        public bool Finished { get; set; }

    }
}
