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
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            string searchId = user.Id;
            List<Projects> projects = new List<Projects>();
            foreach (var project in db.Projects)
            {
                if (searchId == project.devId)
                {
                    projects.Add(project);
                }
                else
                {
                    continue;
                }
            }
            return View(projects);
        }
        public ActionResult Browse()
        {

            return View(db.Plants.ToList());
        }
        // GET: Developer/Details/5
        public ActionResult Details(int id)
        {
            return View("Details","Plants");
        }
        public ActionResult SearchView()
        {
            return View(db.Plants.All(item => item.botanicalName != null));
        }
        public ActionResult SearchBy()//<===pass in the argument they want to search by and the input string
        {
            List<Plants> PlantsResult = new List<Plants>();

            return View(PlantsResult);
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
