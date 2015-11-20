using KanbanFlow.CSharpSDK.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        [JsonProperty("description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("color", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Color { get; set; }

        [JsonProperty("columnId")]
        public string ColumnId { get; set; }

        [JsonProperty("swimlaneId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SwimlaneId { get; set; }

        [JsonProperty("number", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Number? Number { get; set; }

        [JsonProperty("responsibleUserId", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ResponsibleUserId { get; set; }

        [JsonProperty("totalSecondsEstimate")]
        public int TotalSecondsEstimate { get; set; }

        [JsonProperty("totalSecondsSpent")]
        public int TotalSecondsSpent { get; set; }


        public async Task<User[]> GetCollaboratorsAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/collaborators");
            var users = JsonConvert.DeserializeObject<Collaborator[]>(content);
            string[] userIds = users.Select(u => u.Id).ToArray();
            return Board.Users.Where(u => userIds.Contains(u.Id)).ToArray();
        }

        //public async Task<Comment[]> GetCommentsAsync()
        //{
        //}


        public async Task<Subtask[]> GetSubtasksAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/subtasks");
            return JsonConvert.DeserializeObject<Subtask[]>(content);
        }

        public async System.Threading.Tasks.Task CreateSubtaskAsync(string name, bool finished = false)
        {
            Subtask subtask = new Subtask
            {
                Parent = this,
                Name = name,
                Finished = finished
            };
            var str = JsonConvert.SerializeObject(subtask);
            var res = await BoradClient.PostAsync($"tasks/{Id}/subtasks", new StringContent(str, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();
        }
        public async Task<Label[]> GetLabelsAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/labels");
            return JsonConvert.DeserializeObject<Label[]>(content);
        }

        public async System.Threading.Tasks.Task CreateLabelAsync(string name, bool pinned = false)
        {
            Label label = new Label { Name = name, Pinned = pinned };
            var str = JsonConvert.SerializeObject(label);
            var res = await BoradClient.PostAsync($"tasks/{Id}/labels", new StringContent(str, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();
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

        public async Task<Date[]> GetDateAsync()
        {
            var content = await BoradClient.GetStringAsync($"tasks/{Id}/dates");
            return JsonConvert.DeserializeObject<Date[]>(content);
        }

        public async System.Threading.Tasks.Task CreateOrUpdateDateAsync(DateTimeOffset time, string targetColumnId, string status = "active", string dateType = "dueDate")
        {
            Date date = new Date
            {
                DateType = dateType,
                Status = status,
                DueTimestamp = time,
                TargetColumnId = targetColumnId
            };
            var str = JsonConvert.SerializeObject(date);
            var res = await BoradClient.PostAsync($"tasks/{Id}/dates", new StringContent(str, Encoding.UTF8, "application/json"));
            res.EnsureSuccessStatusCode();
        }

        public async System.Threading.Tasks.Task DeleteCollaborator(string collaboratorsId)
        {
            await BoradClient.DeleteAsync($"tasks/{Id}/collaborators/by-user-id/{collaboratorsId}");
        }

        public async System.Threading.Tasks.Task DeleteComment(string commentId)
        {
            await BoradClient.DeleteAsync($"tasks/{Id}/comments/{commentId}");
        }
    }

    public class CreateSubtaskResponse
    {
        [JsonProperty("insertIndex")]
        public string InsertIndex { get; set; }
    }
}
