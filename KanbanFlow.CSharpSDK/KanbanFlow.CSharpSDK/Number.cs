using System.ComponentModel;
using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
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
