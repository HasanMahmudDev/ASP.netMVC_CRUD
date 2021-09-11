using PublisherAndBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublisherAndBook.Controllers
{
    public class PublishersController : Controller
    {
        readonly PlaceDbContext db = new PlaceDbContext();
        // GET: Publishers
        public ActionResult Index()
        {
            return View(db.Publishers.ToList());
        }
        //Create Section
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Publisher p)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(p);
                db.SaveChanges();
                return PartialView("_Result", true);
            }
            return PartialView("_Result", false);
        }
        //Edit Section
        public ActionResult Edit(int id)
        {
            return View(db.Publishers.First(x => x.PublisherId == id));
        }
        [HttpPost]
        public ActionResult Edit(Publisher p)
        {
            if (ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Result", true);
            }
            return PartialView("_Result", false);
        }
        //Delete Section
        public ActionResult Delete(int id)
        {
            return View(db.Publishers.First(x => x.PublisherId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var publisher = new Publisher { PublisherId = id };
            if (!db.Books.Any(x => x.PublisherId == id))
            {
                db.Entry(publisher).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Cannot delete. Publisher has related Book.");
            return View(db.Publishers.First(x => x.PublisherId == id));
        }
    }
}