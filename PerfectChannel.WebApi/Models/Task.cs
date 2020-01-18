using PerfectChannel.WebApi.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PerfectChannel.WebApi.Models
{
    public class Task : ITask, IValidate    
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        [NotMapped]
        public bool IsValid 
        { 
            get
            {
                if(Description == null || Description.Trim().Length == 0)
                {
                    return false;
                }

                if (Status == null || Status.Trim().Length == 0)
                {
                    return false;
                }

                if (Status != "Completed" && Status != "Pending")
                {
                    return false;
                }

                return true;
            }
        }
    }
}
