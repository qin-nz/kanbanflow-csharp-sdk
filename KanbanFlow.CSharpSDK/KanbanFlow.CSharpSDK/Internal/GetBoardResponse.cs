using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK.Internal
{
    internal class GetBoardResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("columns")]
        public GetColumnResponse[] Columns { get; set; }
        [JsonProperty("swimlanes")]
        public GetSwimlaneResponse[] Swimlanes { get; set; }
    }
    internal class GetColumnResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
    }
    internal class GetSwimlaneResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
    }
}
