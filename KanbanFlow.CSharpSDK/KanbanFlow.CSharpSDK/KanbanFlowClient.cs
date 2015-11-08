using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK
{
    public class KanbanFlowClient
    {
        HttpClient client;
        public KanbanFlowClient(string apiToken)
        {
            if (string.IsNullOrWhiteSpace(apiToken))
            {
                throw new ArgumentNullException(nameof(apiToken));
            }
            client = new HttpClient();
            string base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("apiToken:" + apiToken));
            client.BaseAddress = new Uri("https://kanbanflow.com/api/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
        }

        public async Task<Board> GetBoard()
        {
            var body = await client.GetStringAsync("board");
            return JsonConvert.DeserializeObject<Board>(body);
        }

        public async Task<User[]> GetUsers()
        {
            var body = await client.GetStringAsync("users");
            return JsonConvert.DeserializeObject<User[]>(body);
        }

        public async Task<Task> GetTask(string id)
        {
            throw new NotImplementedException();
        }
    }
}
