using System;
using System.Threading.Tasks;
using KanbanFlow.CSharpSDK.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KanbanFlow.CSharpSDK.UnitTest
{
    [TestClass]
    public class KanbanFlowTest
    {
        public static string apiToken = "";
        Board _board;
        [TestInitialize]
        public void InitializeGetBoard()
        {
            KanbanFlowClient account = new KanbanFlowClient(apiToken);
            var task = account.GetBoardAsync();
            task.Wait();
            _board = task.Result;
        }
        [TestMethod]
        public void CheckBoard()
        {
            Assert.IsNotNull(_board.Name);
            Assert.IsNotNull(_board.Users);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task CreateTask()
        {
            await _board.CreateTaskAsync(new Task
            {
                Name = "中文测试",
                Description = "test Chinese",
                ColumnId = _board.Cells[0].ColumnId,
                SwimlaneId = _board.Cells[0].SwimlaneId
            });
        }
    }
}
