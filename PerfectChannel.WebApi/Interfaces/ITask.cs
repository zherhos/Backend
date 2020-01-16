using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectChannel.WebApi.Models
{
    public interface ITask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
    