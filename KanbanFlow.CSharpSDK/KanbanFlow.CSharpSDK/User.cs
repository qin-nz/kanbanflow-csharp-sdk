using Newtonsoft.Json;
namespace KanbanFlow.CSharpSDK
{
    public class User
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
