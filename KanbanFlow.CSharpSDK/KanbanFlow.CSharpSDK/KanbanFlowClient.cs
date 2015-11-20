using KanbanFlow.CSharpSDK.Internal;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
    public class KanbanFlowClient : HttpClient
    {
        /// <summary>
        /// A HttpClient with apiToken of KanbanFlow
        /// </summary>
        /// <param name="apiToken"></param>
        /// <param name="baseAddress"></param>
        public KanbanFlowClient(string apiToken, string baseAddress = "https://kanbanflow.com/api/v1/")
        {
            if (string.IsNullOrWhiteSpace(apiToken))
            {
                throw new ArgumentNullException(nameof(apiToken));
            }
            string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("apiToken:" + apiToken));
            BaseAddress = new Uri(baseAddress);
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        }

        /// <summary>
        /// Get the board can be accessed by apiToken.
        /// </summary>
        /// <remarks>
        /// You can use this board to do something. Or you can use KanbanFlowClient to request manually.
        /// </remarks>
        /// <returns></returns>
        public async Task<Board> GetBoardAsync()
        {
            Board board = new Board(this);
            var boardResponse = await GetBoardInfo();
            board.Id = boardResponse.Id;
            board.Name = boardResponse.Name;
            board.Users = await GetUsers();
            board.Cells = await GetAllTasks(board);
            return board;
        }

        private async Task<GetBoardResponse> GetBoardInfo()
        {
            var body = await GetStringAsync("board");
            return JsonConvert.DeserializeObject<GetBoardResponse>(body);

        }

        private async Task<User[]> GetUsers()
        {
            var body = await GetStringAsync("users");
            return JsonConvert.DeserializeObject<User[]>(body);
        }

        internal async Task<Cell[]> GetAllTasks(Board board)
        {
            var body = await GetStringAsync("tasks");
            var cells = JsonConvert.DeserializeObject<Cell[]>(body);
            foreach (var cell in cells)
            {
                foreach (var task in cell.Tasks)
                {
                    task.Board = board;
                    task.BoradClient = this;
                }
            }
            return cells;
        }
    }
}
