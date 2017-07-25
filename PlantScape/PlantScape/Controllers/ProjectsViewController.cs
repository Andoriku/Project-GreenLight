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
            ProjectsViewModel projects = new ProjectsViewModel();
            projects.Projects = GetProjects();
            foreach (var project in projects.Projects)
            {
                project.plantList = GetPlants();
            }



            return View(projects);
        }

        private List<Projects> GetProjects()
        {
            List<Projects> projectList = new List<Projects>();
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            string searchId = user.Id;
            foreach (var project in db.Projects)
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
        private List<Plants> GetPlants()
        {
            List<Plants> plantList = new List<Plants>();
            return plantList;
        }

    }
}