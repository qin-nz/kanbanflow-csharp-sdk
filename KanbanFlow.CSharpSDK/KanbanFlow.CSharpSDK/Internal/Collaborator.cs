using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanFlow.CSharpSDK.Internal
{
    internal class Collaborator
    {
        [JsonProperty]
        public string Id { get; set; }
    }
}
