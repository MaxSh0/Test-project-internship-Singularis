using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace API_sing_inter.Models
{
    public class ProcessRepository:IProcessRepository
    {
        private static ConcurrentDictionary<string, ProcessItem> _todos = new ConcurrentDictionary<string, ProcessItem>();
        public ProcessRepository() 
        {
           
        }

        public IEnumerable<ProcessItem> GetAll() 
        {
            return _todos.Values;
        }

        public void Add(ProcessItem item) 
        {
            item.id = Guid.NewGuid().ToString();
            _todos[item.id] = item;
        }

        public ProcessItem Find(string id) 
        {
            ProcessItem item;
            _todos.TryGetValue(id, out item);
            return item;
        }

        public ProcessItem Remove(string id) 
        {
            ProcessItem item;
            _todos.TryRemove(id, out item);
            return item;
        }

        public void Update(ProcessItem item) 
        {
            _todos[item.id] = item;
        }
    }
}
