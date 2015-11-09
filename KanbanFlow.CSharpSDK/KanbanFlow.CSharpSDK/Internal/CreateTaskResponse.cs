using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK.Internal
{
   internal class CreateTaskResponse
    {
        [JsonProperty("taskId")]
        public string TaskId { get; set; }
    }
}
