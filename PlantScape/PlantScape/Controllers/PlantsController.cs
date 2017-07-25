using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantScape.Models;
using System.Data.SqlClient;
using System.IO;

namespace PlantScape.Controllers
{
    public class PlantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PlantViewModels
        public ActionResult Index()
        {
            return View(db.Plants.ToList());
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var path = "";
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(Server.MapPath("~/Content/uploads"), fileName);
                file.SaveAs(path);
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] List = line.Split(new char[] { ',' });
                    try
                    {
                        Plants plant = new Plants();
                        plant.id = Convert.ToInt32(List[0]);
                        plant.botanicalName = List[1];
                        plant.commonName = List[2];
                        plant.type = List[3];
                        plant.fColorSpring = List[4];
                        plant.fColorFall = List[5];
                        plant.flowers = List[6];
                        plant.leafType = List[7];
                        plant.hardinessZone = List[8];
                        plant.soilType = Convert.ToInt32(List[9]);
                        plant.lightReq = List[10];
                        plant.imageUrl = List[11];
                        db.Plants.Add(plant);
                        db.SaveChanges();
                    }
                    catch
                    {
                        continue;
                    }

                }

                return RedirectToAction("Index");
            }
        }
        // GET: PlantViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plants plantViewModel = db.Plants.Find(id);
            if (plantViewModel == null)
            {
                return HttpNotFound();
            }
            return View(plantViewModel);
        }

        // GET: PlantViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,botanicalName,commonName,type,fColorSpring,fColorFall,leafType,hardinessZone,soilType,lightReq")] Plants plant)
        {
            if (ModelState.IsValid)
            {
                db.Plants.Add(plant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        // GET: PlantViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plants plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: PlantViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,botanicalName,commonName,type,fColorSpring,fColorFall,leafType,hardinessZone,soilType,lightReq")] Plants plant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plant);
        }
         
        // GET: PlantViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plants plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: PlantViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plants plantViewModel = db.Plants.Find(id);
            db.Plants.Remove(plantViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
