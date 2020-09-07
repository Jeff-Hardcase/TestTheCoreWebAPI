using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTheCoreWebAPI.Models
{
    public class LogItem
    {
        public string ID { get; set; }
        public string NT_id { get; set; }
        public string AgentSine { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int LogType { get; set; }
        public int ErrNumber { get; set; }
        public string FormName { get; set; }
    }
}
