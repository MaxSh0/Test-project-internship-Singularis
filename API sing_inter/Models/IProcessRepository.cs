using System.Collections.Generic;

namespace API_sing_inter.Models
{
    public interface IProcessRepository
    {
        void Add(ProcessItem item); 
        IEnumerable<ProcessItem> GetAll();
        ProcessItem Find(string id);
        ProcessItem Remove(string key);
        void Update(ProcessItem item);
        
    }
}
