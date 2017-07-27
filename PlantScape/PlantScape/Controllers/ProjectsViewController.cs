using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PlantScape.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantScape.Controllers
{
    public class ProjectsViewController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: ProjectView
        public ActionResult ViewQuotes()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            string searchId = user.Id;
            List<ProjectsViewModel> projectList = new List<ProjectsViewModel>();
            ProjectsViewModel projects = new ProjectsViewModel();
            projects.Projects = GetProjects(searchId);
            projectList.Add(projects);
            return View(projectList);
        }

        private List<Projects> GetProjects(string searchId)
        {
            List<Projects> projectList = new List<Projects>();
            foreach (Projects project in db.Projects.Where(p => p.reqId == searchId))
            {
                if (searchId == project.reqId)
                {
                    projectList.Add(project);
                }
                else
                {
                    continue;
                }
            }
            return projectList;
        }
        private List<Plants> GetPlants(Projects project)
        {
            List<Plants> plantList = new List<Plants>();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == project.reqId);
            int searchzip = user.zipCode;
            Zone zone = db.HardinessZone.FirstOrDefault(z => z.zipcode == searchzip);
           foreach(Plants plant in db.Plants)
            {
                if(zone.zone == plant.hardinessZone)
                {
                    plantList.Add(plant);
                }
            }
            return plantList;
        }

    }
}