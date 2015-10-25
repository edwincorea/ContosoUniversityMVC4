using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using AutoMapper;
using ContosoUniversity.ViewModels;

namespace ContosoUniversity.Controllers
{
    public class StudentController : BaseController
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public async Task<ActionResult> Index()
        {
            return View(await db.Students.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentViewModel studentVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<StudentViewModel, Student>();
                    Student student = Mapper.Map<Student>(studentVM);
                    db.Students.Add(student);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                Log.Error(dex);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(studentVM);
        }

        // GET: Student/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = await db.Students.FindAsync(id);
            Mapper.CreateMap<Student, StudentViewModel>();
            StudentViewModel studentVM = Mapper.Map<StudentViewModel>(student);

            if (student == null)
            {
                return HttpNotFound();
            }
            return View(studentVM);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentViewModel studentVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<StudentViewModel, Student>();
                    Student student = Mapper.Map<Student>(studentVM);
                    db.Entry(student).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                Log.Error(dex);
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(studentVM);
        }

        // GET: Student/Delete/5
        public async Task<ActionResult> Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }            
            Student student = await db.Students.FindAsync(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Student student = await db.Students.FindAsync(id);
                db.Students.Remove(student);
                await db.SaveChangesAsync();
            }
            catch (DataException dex)
            {
                Log.Error(dex);
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
