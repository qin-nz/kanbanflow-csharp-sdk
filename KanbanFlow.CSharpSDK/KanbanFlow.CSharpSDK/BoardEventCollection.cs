using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KanbanFlow.CSharpSDK
{
    [JsonObject]
    public class BoardEventCollection : ICollection<BoardEvent>
    {
        [JsonProperty("eventsLimited")]
        public bool EventsLimited { get; set; }

        [JsonProperty("events")]
        public BoardEvent[] Events { get; set; }

        public int Count
        {
            get
            {
                return Events.Length;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return true;
            }
        }

        public void Add(BoardEvent item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(BoardEvent item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(BoardEvent[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(BoardEvent item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<BoardEvent> GetEnumerator()
        {
            for (int i = 0; i < Events.Length; i++)
            {
                yield return Events[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0; i < Events.Length; i++)
            {
                yield return Events[i];
            }
        }
    }
}
