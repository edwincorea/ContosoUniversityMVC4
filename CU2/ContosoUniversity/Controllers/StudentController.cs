using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.Models;
using ContosoUniversity.DAL;

namespace ContosoUniversity.Controllers
{
   public class StudentController : Controller
   {
      private SchoolContext db = new SchoolContext();

      //
      // GET: /Student/

      public ActionResult Index()
      {
         return View(db.Students.ToList());
      }

      //
      // GET: /Student/Details/5

      public ActionResult Details(int id = 0)
      {
         Student student = db.Students.Find(id);
         if (student == null)
         {
            return HttpNotFound();
         }
         return View(student);
      }

      //
      // GET: /Student/Create

      public ActionResult Create()
      {
         return View(new Student { FirstMidName = "Rick", LastName = "Anderson", EnrollmentDate = DateTime.Now });
      }

      //
      // POST: /Student/Create

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(
         [Bind(Include = "LastName, FirstMidName, EnrollmentDate")]
         Student student)
      {
         try
         {
            if (ModelState.IsValid)
            {
               db.Students.Add(student);
               db.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
         }
         return View(student);
      }

      //
      // GET: /Student/Edit/5

      public ActionResult Edit(int id = 0)
      {
         Student student = db.Students.Find(id);
         if (student == null)
         {
            return HttpNotFound();
         }
         return View(student);
      }

      //
      // POST: /Student/Edit/5

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(
         [Bind(Include = "LastName, FirstMidName, EnrollmentDate")]
         Student student)
      {
         try
         {
            if (ModelState.IsValid)
            {
               db.Entry(student).State = EntityState.Modified;
               db.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         catch (DataException /* dex */)
         {
            //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
         }
         return View(student);
      }

      //
      // GET: /Student/Delete/5

      public ActionResult Delete(int id = 0)
      {
         Student student = db.Students.Find(id);
         if (student == null)
         {
            return HttpNotFound();
         }
         return View(student);
      }

      //
      // POST: /Student/Delete/5

      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Student student = db.Students.Find(id);
         db.Students.Remove(student);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      protected override void Dispose(bool disposing)
      {
         db.Dispose();
         base.Dispose(disposing);
      }
   }
}