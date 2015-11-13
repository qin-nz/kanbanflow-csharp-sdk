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

        [JsonIgnore]
        internal Board Board { get; set; }

        [JsonIgnore]
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
        public List<SubTask> SubTasks => _subTasks ?? (_subTasks = GetSubtasks());
        private List<SubTask> _subTasks;

        [JsonIgnore]
        public List<Label> Labels => _labels ?? (_labels = GetLabels());
        private List<Label> _labels;

        [JsonIgnore]
        public object Collaborators { get; set; }
        [JsonIgnore]
        public object Comments { get; set; }
        [JsonIgnore]
        public List<Date> Dates { get; set; }

        private List<SubTask> GetSubtasks()
        {
            var task = BoradClient.GetStringAsync($"tasks/{Id}/subtasks");
            task.Wait();
            var content = task.Result;
            return JsonConvert.DeserializeObject<SubTask[]>(content).ToList();
        }

        public async System.Threading.Tasks.Task CreateSubtaskAsync(string name, bool finished = false)
        {
            if (_subTasks == null)
            {
                _subTasks = GetSubtasks();
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
        private List<Label> GetLabels()
        {

            var task = BoradClient.GetStringAsync($"tasks/{Id}/labels");
            task.Wait();
            var content = task.Result;
            return JsonConvert.DeserializeObject<Label[]>(content).ToList();
        }

        public async System.Threading.Tasks.Task CreateLabelAsync(string name, bool pinned = false)
        {
            if (_labels == null)
            {
                _labels = GetLabels();
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

        /// <summary>
        /// Update Task Information, but NOT include Labels,Collaborators,Comments,Dates.
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task UpdateAsync()
        {
            if (Board == null)
            {
                throw new NotSupportedException("Please first create this task.");
            }
            var str = JsonConvert.SerializeObject(this);
            await BoradClient.PostAsync($"tasks/{Id}", new StringContent(str, Encoding.UTF8, "application/json"));
        }

        public async System.Threading.Tasks.Task CreateOrUpdateDateAsync(DateTimeOffset time, string targetColumnId, string status = "active", string dateType = "dueDate")
        {
            Date date = new Date
            {
                DateType = dateType,
                Status = status,
                DueTimestamp = time.ToUniversalTime().ToString("O"),
                DueTimestampLocal = time.ToString("O"),
                TargetColumnId = targetColumnId
            };
            var str = JsonConvert.SerializeObject(date);
            await BoradClient.PostAsync($"tasks/{Id}/dates", new StringContent(str, Encoding.UTF8, "application/json"));
        }
        
    }

    public class CreateSubtaskResponse
    {
        [JsonProperty("insertIndex")]
        public string InsertIndex { get; set; }
    }
}
