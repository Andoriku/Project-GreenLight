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
    public class ClientController : Controller
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

        // GET: Customer
        public ActionResult Index()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            return View(user);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Browse()
        {
            return View(db.Plants.ToList());
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
        public ActionResult CreateProject()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject([Bind(Include = "projectName,devId,userComments")] Projects projects)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                projects.reqId = user.Id;
                db.Projects.Add(projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projects);
        }
        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult EditComment(int id)
        {
            Projects project = db.Projects.Find(id);
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment([Bind(Include = "userComments")] Projects search, int id)
        {
            
            Projects project = db.Projects.FirstOrDefault(p => p.id == id);
            project.userComments = search.userComments;
            db.Entry(project).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewQuotes","ProjectsView");
        }
        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
