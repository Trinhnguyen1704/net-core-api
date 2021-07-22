using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models
{
    public class TodoItem
    {
        public long Id {get;set;}
        public string Name {get; set;}
        public bool isComplete {get;set ;}
    }
}