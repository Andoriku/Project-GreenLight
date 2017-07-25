using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class Projects
    {
        public Projects()
        {
            this.plantList = new HashSet<Plants>();
        }
        [Key]
        public int id { get; set; }
        public string projectName { get; set; }
        public Byte[] image { get; set; }
        public string displayImage { get; set; }
        public virtual string reqId { get; set; }
        public virtual string devId { get; set; }
        public virtual ICollection<Plants> plantList { get; set; }
        public string userComments { get; set; }
    }
}