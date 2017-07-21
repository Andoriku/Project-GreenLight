using PlantScape.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantScape.Controllers
{
    public class ZoneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: HardinessZone
        public ActionResult Index()
        {
            return View();
        }
        // POST: HardinessZone
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            int counter = 0;
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
                        Zone hardinessZone = new Zone();
                        hardinessZone.zipcode = Convert.ToInt32(List[0]);
                        hardinessZone.zone = List[1];
                        db.HardinessZone.Add(hardinessZone);
                        db.SaveChanges();
                        counter += 1;
                        if (counter == 1000)
                        {
                            Console.WriteLine("1000 more done...");
                            counter = 0;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}