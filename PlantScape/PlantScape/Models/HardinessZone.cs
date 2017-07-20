using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class HardinessZone
    {
        public HardinessZone(string[] csvarray)
        {
            this.zipcode = Convert.ToInt32(csvarray[0]);
            this.zone = csvarray[1];

        }

        [Key]
        public int zipcode { get; set; }
        public string zone {get;set;}
    }
}