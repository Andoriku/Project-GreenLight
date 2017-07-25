using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class Plants
    {
        public Plants()
        {
            this.projectList = new HashSet<Projects>();
        }
        public Plants(string[] csvarray)
        {
            this.id = Convert.ToInt32(csvarray[0]);
            this.botanicalName = csvarray[1];
            this.commonName = csvarray[2];
            this.type = csvarray[3];
            this.fColorSpring = csvarray[4];
            this.fColorFall = csvarray[5];
            this.flowers = csvarray[6];
            this.leafType = csvarray[7];
            this.hardinessZone = csvarray[8];
            this.soilType = Convert.ToInt32(csvarray[9]);
            this.lightReq = csvarray[10];
            this.imageUrl = csvarray[11];
            this.projectList = new HashSet<Projects>();
            this.favoriteList = new HashSet<ApplicationUser>();
        }
        [Key]
        public int id { get; set; }
        public string botanicalName { get; set; }
        public string commonName { get; set; }
        public string type { get; set; }
        public string fColorSpring { get; set; }
        public string fColorFall { get; set; }
        public string flowers { get; set; }
        public string leafType { get; set; }
        public string hardinessZone { get; set; }
        public int soilType { get; set; }
        public string lightReq { get; set; }
        public string imageUrl { get; set; }
        public virtual ICollection<Projects> projectList { get; set; }
        public virtual ICollection<ApplicationUser> favoriteList { get; set; }
        public bool isSelected { get; set; }
    }
}