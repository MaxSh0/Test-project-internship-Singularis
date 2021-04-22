using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using API_sing_inter.Models;

namespace API_sing_inter.Controllers
{
    [Route("crawler/")]
    [ApiController]
    public class ProcessController : Controller
    {
        public ProcessController(IProcessRepository processItems) 
        {
            ProcessItems = processItems;         
        } 
        public IProcessRepository ProcessItems { get; set; }
        
        
        [HttpGet]
        public IEnumerable<ProcessItem> GetAll() 
        {
            return ProcessItems.GetAll();
        }

        [HttpGet("result/{id}", Name = "GetProcess")]
        public IActionResult GetResultById(string id)
        {
            var item = ProcessItems.Find(id);
            if (item == null || item.status == 0|| item.status == 2)
            {
                return NotFound();
            }
            return new ObjectResult("Result - " + item.result);
        }

        [HttpGet("status/{id}")]
        public IActionResult GetStatusById(string id)
        {
            var item = ProcessItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult("Status - " + item.status.ToString());
        }

        [HttpPost("crawl")]
        public IActionResult CreateProcess([FromBody] ProcessItem item) 
        {
            if(item == null) 
            {
                return BadRequest();
            }
            ProcessItems.Add(item);
            TreatmentHTML pr = new TreatmentHTML();
            
            Thread WorkThread = new Thread(new ParameterizedThreadStart((x) =>{ pr.WordSearch(item); }));
            WorkThread.Start(item);
            
            return CreatedAtRoute("GetProcess", new { id = item.id }, item);
        }
    }
}
