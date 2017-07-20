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
        public List<int> numberOfEach { get; set; }
        public virtual ApplicationUser requester { get; set; }
        public virtual ApplicationUser developer { get; set; }
        public virtual ICollection<Plants> plantList { get; set; }

    }
}