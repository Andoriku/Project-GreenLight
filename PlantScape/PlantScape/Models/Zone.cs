using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class Zone
    {
        public Zone()
        {

        }
        public Zone(string[] csvarray)
        {

            this.zipcode = Convert.ToInt32(csvarray[0]);
            this.zone = csvarray[1];

        }

        [Key]
        public int id { get; set; }
        public int zipcode { get; set; }
        public string zone { get; set; }
    }
}