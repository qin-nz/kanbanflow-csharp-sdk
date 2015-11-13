using System;
using System.Linq;
using System.Threading.Tasks;
using KanbanFlow.CSharpSDK.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KanbanFlow.CSharpSDK.UnitTest
{
    [TestClass]
    public class KanbanFlowTest
    {
        public static string ApiToken = "";
        private static Board _board;
        private static Task _testTask;
        [ClassInitialize]
        public static void InitializeGetBoard(TestContext context)
        {
            KanbanFlowClient account = new KanbanFlowClient(ApiToken);
            var t0 = account.GetBoardAsync();
            t0.Wait();
            _board = t0.Result;
            var t1 = CreateTask();
            t1.Wait();
            _testTask = t1.Result;
        }
        private static async Task<Task> CreateTask()
        {
            return await _board.CreateTaskAsync(new Task
            {
                Name = "中文测试",
                Description = "test Chinese",
                ColumnId = _board.Cells[0].ColumnId,
                SwimlaneId = _board.Cells[0].SwimlaneId
            });
        }

        [TestMethod]
        public void CheckBoard()
        {
            Assert.IsNotNull(_board.Name);
            Assert.IsNotNull(_board.Cells);
            Assert.IsNotNull(_board.Users);
        }



        [TestMethod]
        public async System.Threading.Tasks.Task CreatSubtasks()
        {
            await _testTask.CreateSubtaskAsync("子任务", true);
        }

        [TestMethod]
        public void GetSubtask()
        {
            var subtasks = _testTask.SubTasks;
            Assert.AreEqual("子任务", subtasks[0].Name);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateDate()
        {
            await _testTask.CreateOrUpdateDateAsync(DateTimeOffset.Now.AddDays(1.3), _board.Cells.Last().ColumnId);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task UpdateDate()
        {
            await _testTask.CreateOrUpdateDateAsync(DateTimeOffset.Now.AddDays(10), _board.Cells.Last().ColumnId);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task MoveTask()
        {
            await _board.MoveTaskAsync(_testTask, _board.Cells[3].ColumnId, _board.Cells[0].SwimlaneId);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task DeleteTask()
        {
            await _board.DeleteTaskAsync(_testTask);
        }
    }
}
