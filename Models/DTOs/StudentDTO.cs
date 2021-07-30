using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_api.Models.DTOs
{
    public class StudentDTO
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string DateOfBirth {get; set;}
        public float AverageMark {get; set;}
        public List<int> ClassIds {get; set;}
    }
}