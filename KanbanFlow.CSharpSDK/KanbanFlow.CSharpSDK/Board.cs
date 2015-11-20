using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Collections;
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

        /// <summary>
        /// Get Users Who can access the board.
        /// </summary>
        public User[] Users { get; internal set; }

        /// <summary>
        /// Get Cells of the board. Cell equals to Column if you disable swimeline. Otherwise cell means a cross point for a column and a swimline.
        /// </summary>
        public Cell[] Cells { get; internal set; }

        /// <summary>
        /// Get columns Id
        /// </summary>
        public string[] ColumnsId => Cells.Select(c => c.ColumnId).Distinct().ToArray();

        public async Task<Task> CreateTaskAsync(Task task)
        {
            if (task.ColumnId == null)
            {
                task.ColumnId = ColumnsId[0];
            }
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

        public async System.Threading.Tasks.Task DeleteTaskAsync(Task task)
        {
            Cell cell = Cells.FirstOrDefault(c => c.Tasks.Contains(task));
            await _client.DeleteAsync($"tasks/{task.Id}");
            cell?.Tasks.Remove(task);
        }

        public async Task<BoardEventCollection> GetEventsAsync(DateTimeOffset from, DateTimeOffset to)
        {
            var str = await _client.GetStringAsync($"board/events?from={from.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ")}&to={to.ToUniversalTime().ToString("yyyy-MM-ddThh:mm:ssZ")}");
            return JsonConvert.DeserializeObject<BoardEventCollection>(str);
        }

        /// <summary>
        /// Get all tasks from kanbanflow.com and update <c>Cells</c> property.
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task RefreshTasks()
        {
            var cells = await _client.GetAllTasks(this);
            Cells = cells;
        }
    }
}
