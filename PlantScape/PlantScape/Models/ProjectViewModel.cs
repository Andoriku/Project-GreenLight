using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class ProjectViewModel
    {
        public Projects Project { get; set; }
        public IEnumerable<Plants> Plants { get; set; }

    }
}