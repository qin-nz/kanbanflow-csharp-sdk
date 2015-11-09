using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace KanbanFlow.CSharpSDK
{
    public class Task
    {
        [JsonIgnore]
        internal KanbanFlowClient BoradClient { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("columnId")]
        public string ColumnId { get; set; }

        [JsonProperty("swimlaneId")]
        public string SwimlaneId { get; set; }

        [JsonProperty("number")]
        public Number? Number { get; set; }

        [JsonProperty("responsibleUserId")]
        public string ResponsibleUserId { get; set; }

        [JsonProperty("totalSecondsEstimate")]
        public int TotalSecondsEstimate { get; set; }

        [JsonProperty("totalSecondsSpent")]
        public int TotalSecondsSpent { get; set; }

        [JsonIgnore]
        public List<SubTask> SubTasks { get; set; }
        [JsonIgnore]
        public List<Label> Labels { get; set; }
        [JsonIgnore]
        public object Collaborators { get; set; }
        [JsonIgnore]
        public object Comments { get; set; }
        [JsonIgnore]
        public object Dates { get; set; }

        private async System.Threading.Tasks.Task GetSubtasksAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/subtasks");
            SubTasks = JsonConvert.DeserializeObject<SubTask[]>(content).ToList();
        }

        public async System.Threading.Tasks.Task CreateSubtaskAsync(string name, bool finished = false)
        {
            if (SubTasks == null)
            {
                await GetSubtasksAsync();
            }
            SubTask subtask = new SubTask { Parent = this, Name = name, Finished = finished };
            var str = JsonConvert.SerializeObject(subtask);
            var res = await BoradClient.PostAsync($"tasks/{Id}/subtasks", new StringContent(str, Encoding.UTF8, "application/json"));
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var content = await res.Content.ReadAsStringAsync();
                JsonConvert.DeserializeObject(content);
                SubTasks.Add(subtask);
            }
        }
        private async System.Threading.Tasks.Task GetLabelsAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/labels");
            Labels = JsonConvert.DeserializeObject<Label[]>(content).ToList();
        }

        public async System.Threading.Tasks.Task CreateLabelAsync(string name, bool pinned = false)
        {
            if (Labels == null)
            {
                await GetLabelsAsync();
            }
            Label label = new Label { Name = name, Pinned = pinned };
            var str = JsonConvert.SerializeObject(label);
            var res = await BoradClient.PostAsync($"tasks/{Id}/labels", new StringContent(str, Encoding.UTF8, "application/json"));
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var content = await res.Content.ReadAsStringAsync();
                JsonConvert.DeserializeObject(content);
                Labels.Add(label);
            }
        }

        public async System.Threading.Tasks.Task UpdateAsync()
        {
            if (Id==null)
            {
                throw new NotSupportedException("Please first create this task.");
            }
            var str = JsonConvert.SerializeObject(this);
            await BoradClient.PostAsync($"tasks/{Id}", new StringContent(str, Encoding.UTF8, "application/json"));
        }
    }

    public class CreateSubtaskResponse
    {
        [JsonProperty("insertIndex")]
        public string InsertIndex { get; set; }
    }
}
