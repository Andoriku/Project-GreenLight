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
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return View(user);
        }
        public ActionResult About()
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
        public ActionResult AddPlants(int plantId, int projId)
        {
            Projects project = db.Projects.FirstOrDefault(pr => pr.id == projId);
            Plants plant = db.Plants.FirstOrDefault(pl => pl.id == plantId);
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
            return View(db.Plants.ToList());
        }
        public ActionResult SearchZip(FormCollection form)//<===pass in the argument they want to search by and the input string
        {
            int input = Convert.ToInt32(form["Input"]);
            List<Plants> plantList = new List<Plants>();
            try
            {
                Zone zone = db.HardinessZone.FirstOrDefault(z => z.zipcode == input);

                foreach (Plants plant in db.Plants)
                {
                    if (zone.zone == plant.hardinessZone.ToLower())
                    {
                        plant.favoriteList = null;
                        plant.projectList = null;
                        plantList.Add(plant);
                    }
                }
                return View("SearchResult", plantList);
            }
            catch
            {
                return RedirectToAction("Sorry");
            }
        }
        public ActionResult Sorry()
        {
            return View();
        }
        public ActionResult SearchBy(FormCollection form)//<===pass in the argument they want to search by and the input string
        {
            string selectedSearch = form["Search"].ToString();
            string input = form["Input"].ToString();
            return RedirectToAction("SearchResult","Developer",new { selected = selectedSearch, searchInput = input });   
        }
        public ActionResult SearchResult(string selected, string searchInput)
        {
            List<Plants> plants = new List<Plants>();
            switch (selected)
            {
                case "Bontanical Name":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.botanicalName == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult",plants);
                case "Common Name":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.commonName == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Plant Type":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.type == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Foliage Color (Fall)":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.fColorFall == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Foliage Color (Spring)":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.fColorSpring == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Flowers":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.flowers == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Hardiness Zone":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.hardinessZone == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult",plants);
                case "Soil Type":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.soilType == Convert.ToInt32(searchInput))
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                case "Light Requirements":
                    foreach (Plants Plant in db.Plants)
                    {
                        if (Plant.lightReq == searchInput)
                        {
                            plants.Add(Plant);
                        }
                    }
                    return View("SearchResult", plants);
                default:
                    return RedirectToAction("SearchView");
            }
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
            Plants plant  = db.Plants.FirstOrDefault(Plant => Plant.id == search.id);
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
