using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChamealeonApp.Models.DTOs
{
    //Author: Burhan
    public class ErrorDTO
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
    }
}