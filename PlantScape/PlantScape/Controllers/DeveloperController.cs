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
    public class DeveloperController : Controller
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
        // GET: Developer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Projects()
        {
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
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
            foreach (Projects proj in db.Projects.Where(p => p.devId == searchId))
            {
                if (searchId == proj.devId)
                {
                    projectList.Add(proj);
                }
                else
                {
                    continue;
                }
            }
            return projectList;
        }
        private List<Plants> GetPlants(string searchId)
        {
            List<Plants> plantList = new List<Plants>();
            ApplicationUser user = db.Users.FirstOrDefault(u => u.Id == searchId);
            int searchzip = user.zipCode;
            Zone zone = db.HardinessZone.FirstOrDefault(z => z.zipcode == searchzip);
            foreach (Plants plant in db.Plants)
            {
                if (zone.zone == plant.hardinessZone.ToLower())
                {
                    plant.favoriteList = null;
                    plant.projectList = null;
                    plantList.Add(plant);
                }
            }
            return plantList;
        }
        public ActionResult AddPlants(int id)
        {
            ProjectViewModel project = new ProjectViewModel();
            project.Project = db.Projects.FirstOrDefault(p => p.id == id);
            project.Plants = GetPlants(project.Project.reqId);
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Check Why searchPlant id is not being pushed to the back end
        public ActionResult AddPlants(int id, [Bind(Include = "plantId")] Plants searchPlant)
        {
            Projects project = db.Projects.FirstOrDefault(pr => pr.id == id);
            Plants plant = db.Plants.FirstOrDefault(pl => pl.id == searchPlant.id);
                    db.Plants.Attach(plant);
                    project.plantList.Add(plant);
                    db.Entry(project).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
           
            return RedirectToAction("AddPlants");
        }
        public ActionResult Browse()
        {

            return View(db.Plants.ToList());
        }
        // GET: Developer/Details/5
        public ActionResult Details(int id)
        {
            return View("Details", "Plants");
        }
        public ActionResult SearchView()
        {
            return View(db.Plants.All(item => item.botanicalName != null));
        }
        public ActionResult SearchBy()//<===pass in the argument they want to search by and the input string
        {
            List<Plants> searchResult = new List<Plants>();
            foreach(Plants plant in db.Plants)
            {
                
            }

            return View(searchResult);
        }
     public ActionResult AddToFavorites(int id)
        {
            Plants plants = db.Plants.Find(id);
            return View(plants);
        }
        [HttpPost]
        public ActionResult AddToFavorites([Bind(Include = "id")] Plants search)
        {
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            Plants plant = db.Plants.FirstOrDefault(Plant => Plant.id == search.id);
            db.Plants.Attach(plant);
            user.favoriteList.Add(plant);
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Browse");
        }
        public ActionResult ViewFavorites()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            string searchId = user.Id;
            try
            {
                var query = from plant in db.Plants
                            where plant.favoriteList.Any(faveuser => faveuser.Id == searchId)
                            select plant;
                return View(query);
            }
            catch
            {
                return RedirectToAction("Index");
            }
           
        }
        // GET: Developer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Developer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Developer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Developer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Developer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
