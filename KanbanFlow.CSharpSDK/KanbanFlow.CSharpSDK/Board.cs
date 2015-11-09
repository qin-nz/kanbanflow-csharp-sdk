using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using KanbanFlow.CSharpSDK.Internal;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Board
    {
        private KanbanFlowClient _client;

        internal Board(KanbanFlowClient client)
        {
            _client = client;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public User[] Users { get; internal set; }
        public Cell[] Cells { get; internal set; }
        public async System.Threading.Tasks.Task CreateTaskAsync(Task task)
        {
            //TODO:Check columnId & swimlaneId
            Cell currentCell = Cells.FirstOrDefault(c => c.ColumnId == task.ColumnId && c.SwimlaneId == task.SwimlaneId);
            if (currentCell == null)
            {
                throw new ArgumentOutOfRangeException(nameof(task), "Task is not belongs to a column or a swimlane.");
            }
            var str = JsonConvert.SerializeObject(task);
            var res = await _client.PostAsync($"tasks", new StringContent(str, Encoding.UTF8, "application/json"));
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var content = await res.Content.ReadAsStringAsync();
                var taskid = JsonConvert.DeserializeObject<CreateTaskResponse>(content).TaskId;
                task.Id = taskid;
                currentCell.Tasks.Add(task);
            }
        }
    }
}
