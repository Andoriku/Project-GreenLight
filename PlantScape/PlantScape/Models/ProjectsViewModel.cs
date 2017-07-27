using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlantScape.Models
{
    public class ProjectsViewModel
    {
        public IEnumerable<Projects> Projects { get; set; }
        public IQueryable<Plants> Plants { get; set; }

    }
}