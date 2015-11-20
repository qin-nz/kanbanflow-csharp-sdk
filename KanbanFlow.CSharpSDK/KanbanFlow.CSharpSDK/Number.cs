using System.ComponentModel;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    /// <summary>
    /// Numbering of tasks. You can enable this feature at https://kanbanflow.com/administration/board/(you-board-id)/task-numbering 
    /// </summary>
    public struct Number
    {
        [DefaultValue("")]
        [JsonProperty("prefix", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Prefix { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }

        public override string ToString()
        {
            return Prefix + Value;
        }

        public static implicit operator Number(int i)
        {
            return new Number
            {
                Prefix = "",
                Value = i
            };
        }
    }
}
