using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KanbanFlow.CSharpSDK.Internal;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public class Board
    {
        private readonly KanbanFlowClient _client;

        internal Board(KanbanFlowClient client)
        {
            _client = client;
        }

        public string Id { get; internal set; }
        public string Name { get; internal set; }
        public User[] Users { get; internal set; }
        public Cell[] Cells { get; internal set; }

        public async Task<Task> CreateTaskAsync(Task task)
        {
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
                var taskId = JsonConvert.DeserializeObject<CreateTaskResponse>(content).TaskId;
                var newTask = await GetTask(taskId);
                newTask.Board = this;
                newTask.BoradClient = _client;
                currentCell.Tasks.Add(newTask);
                return newTask;
            }
            throw new HttpRequestException($"Status Code is {res.StatusCode}");
        }

        private async Task<Task> GetTask(string taskId)
        {
            var str = await _client.GetStringAsync($"tasks/{taskId}");
            var task = JsonConvert.DeserializeObject<Task>(str);
            task.Id = taskId;
            return task;
        }

        public async System.Threading.Tasks.Task MoveTaskAsync(Task task, Cell cell)
        {
            Cell destinationCell = Cells.FirstOrDefault(c => c.ColumnId == cell.ColumnId && c.SwimlaneId == cell.SwimlaneId);
            if (destinationCell == null)
            {
                throw new ArgumentException("ColumnId or SwimlaneId cannot be found in the board.", nameof(cell));
            }
            task.ColumnId = cell.ColumnId;
            task.SwimlaneId = cell.SwimlaneId;
            await task.UpdateAsync();
        }

        public async System.Threading.Tasks.Task MoveTaskAsync(Task task, string columnId, string swimlaneId)
        {
            Cell destinationCell = Cells.FirstOrDefault(c => c.ColumnId == columnId && c.SwimlaneId == swimlaneId);
            if (destinationCell == null)
            {
                throw new ArgumentOutOfRangeException(nameof(columnId), "new ColumnId or SwimlaneId cannot be found.");
            }
            await MoveTaskAsync(task, destinationCell);
        }
    }
}
