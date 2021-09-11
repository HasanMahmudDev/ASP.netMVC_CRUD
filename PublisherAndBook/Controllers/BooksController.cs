using PublisherAndBook.Models;
using PublisherAndBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublisherAndBook.Controllers
{
    public class BooksController : Controller
    {
       
        readonly PlaceDbContext db = new PlaceDbContext();

        // GET: Books
        public ActionResult Index(int pg = 1)
        {
            int totalPages = (int)Math.Ceiling((double)db.Books.Count() / 3);
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = pg;
            var data = db.Books.Include(x => x.Publisher).OrderBy(x => x.BookId).Skip((pg - 1) * 3).Take(3).ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Publishers = db.Publishers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(BookInputModel p)
        {
            if (ModelState.IsValid)
            {
                Book pr = new Book
                {
                    BookName = p.BookName,
                    Price = p.Price,
                    Discontinued = p.Discontinued,
                    PublisherId = p.PublisherId,
                    Picture = "demo.jpg"
                };
                if (p.Picture != null && p.Picture.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Path.Combine(filePath, fileName));
                    pr.Picture = fileName;
                }
                db.Books.Add(pr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Publishers = db.Publishers.ToList();
            return View(p);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Publishers = db.Publishers.ToList();
            var d = db.Books.First(x => x.BookId == id);
            ViewBag.CurrentPic = d.Picture;
            return View(new BookEditModel { BookId = d.BookId, BookName = d.BookName, Price = d.Price, Discontinued = d.Discontinued, PublisherId = d.PublisherId });
        }
        [HttpPost]
        public ActionResult Edit(BookEditModel p)
        {
            if (ModelState.IsValid)
            {
                Book pr = db.Books.First(x => x.BookId == p.BookId);
                pr.BookName = p.BookName;
                pr.Price = p.Price;
                pr.Discontinued = p.Discontinued;
                pr.PublisherId = p.PublisherId;
                if (p.Picture != null && p.Picture.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Uploads/");
                    string fileName = Guid.NewGuid() + Path.GetExtension(p.Picture.FileName);
                    p.Picture.SaveAs(Path.Combine(filePath, fileName));
                    pr.Picture = fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Publishers = db.Publishers.ToList();
            var d = db.Books.First(x => x.BookId == p.BookId);
            ViewBag.CurrentPic = d.Picture;
            return View(p);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Books.Include(x => x.Publisher).First(x => x.BookId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            var p = new Book { BookId = id };
            db.Entry(p).State = EntityState.Deleted;
            return RedirectToAction("Index");
        }
    }

}
