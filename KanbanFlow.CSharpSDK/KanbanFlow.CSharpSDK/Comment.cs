using System;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Comment
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("authorUserId")]
        public string AuthorUserId { get; set; }

        [JsonProperty("createdTimestamp")]
        public DateTimeOffset CreatedTimestamp { get; set; }

        [JsonProperty("updatedTimestamp")]
        public DateTimeOffset? UpdatedTimestamp { get; set; }
    }
}
