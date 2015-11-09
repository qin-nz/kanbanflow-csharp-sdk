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

       static Board board;
        public Board GetBoard()
        {
            //TODO: 处理并发
            if (board !=null)
            {
                return board;
            }
            board = new Board(this);
            Parallel.Invoke(
 async () =>
             {
                 var body = await GetStringAsync("board");
                 var boardResponse = JsonConvert.DeserializeObject<GetBoardResponse>(body);
                 board.Id = boardResponse.Id;
                 board.Name = boardResponse.Name;
             },
 async () =>
             {
                 var body = await GetStringAsync("users");
                 var users = JsonConvert.DeserializeObject<User[]>(body);
                 board.Users = users;
             },
                async () =>
                {
                    var body = await GetStringAsync("tasks");
                    var cells = JsonConvert.DeserializeObject<Cell[]>(body);
                    foreach (var cell in cells)
                    {
                        foreach (var task in cell.Tasks)
                        {
                            task.BoradClient = this;
                        }
                    }
                    board.Cells = cells;
                }
         );
            return board;
        }
    }
}
