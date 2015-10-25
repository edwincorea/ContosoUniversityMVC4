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
   public class CourseController : Controller
   {
      private SchoolContext db = new SchoolContext();

      //
      // GET: /Course/

      public ActionResult Index()
      {
         var courses = db.Courses.Include(c => c.Department);
         return View(courses.ToList());
      }

      //
      // GET: /Course/Details/5

      public ActionResult Details(int id = 0)
      {
         Course course = db.Courses.Find(id);
         if (course == null)
         {
            return HttpNotFound();
         }
         return View(course);
      }

      public ActionResult Create()
      {
         PopulateDepartmentsDropDownList(2);
         return View(new Course { CourseID = 3333, Credits = 3, Title = "Algebra II" });
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create(
         [Bind(Include = "CourseID,Title,Credits,DepartmentID")]
   Course course)
      {
         try
         {
            if (ModelState.IsValid)
            {
               db.Courses.Add(course);
               db.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         catch (DataException dex )
         {
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
         PopulateDepartmentsDropDownList(course.DepartmentID);
         return View(course);
      }

      public ActionResult Edit(int id)
      {
         Course course = db.Courses.Find(id);
         PopulateDepartmentsDropDownList(course.DepartmentID);
         return View(course);
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit(
        //  [Bind(Include = "CourseID,Title,Credits,DepartmentID")]
    Course course)
      {
         try
         {
            if (ModelState.IsValid)
            {
               db.Entry(course).State = EntityState.Modified;
               db.SaveChanges();
               return RedirectToAction("Index");
            }
         }
         catch (DataException dex)
         {
            ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
         }
         PopulateDepartmentsDropDownList(course.DepartmentID);
         return View(course);
      }

      private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
      {
         var departmentsQuery = from d in db.Departments
                                orderby d.Name
                                select d;
         ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID", "Name", selectedDepartment);
      }

      //
      // GET: /Course/Delete/5

      public ActionResult Delete(int id = 0)
      {
         Course course = db.Courses.Find(id);
         if (course == null)
         {
            return HttpNotFound();
         }
         return View(course);
      }

      //
      // POST: /Course/Delete/5

      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         Course course = db.Courses.Find(id);
         db.Courses.Remove(course);
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