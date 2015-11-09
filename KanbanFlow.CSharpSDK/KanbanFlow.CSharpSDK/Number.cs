using Newtonsoft.Json;

namespace KanbanFlow.CSharpSDK
{
    public struct Number
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
        [JsonProperty("value")]
        public int Value { get; set; }

        public override string ToString()
        {
            return Prefix + Value;
        }
    }
}
