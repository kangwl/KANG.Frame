using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KANG.MVC.Test.Models;

namespace KANG.MVC.Test.Controllers
{
    public class CourseController : Controller
    {
        private KANGMVCTestContext db = new KANGMVCTestContext();

        // GET: Course
        public async Task<ActionResult> Index()
        {
            return View(await db.Course_Model.ToListAsync());
        }

        // GET: Course/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Model course_Model = await db.Course_Model.FindAsync(id);
            if (course_Model == null)
            {
                return HttpNotFound();
            }
            return View(course_Model);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Price")] Course_Model course_Model)
        {
            if (ModelState.IsValid)
            {
                db.Course_Model.Add(course_Model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(course_Model);
        }

        // GET: Course/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Model course_Model = await db.Course_Model.FindAsync(id);
            if (course_Model == null)
            {
                return HttpNotFound();
            }
            return View(course_Model);
        }

        // POST: Course/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Price")] Course_Model course_Model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course_Model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course_Model);
        }

        // GET: Course/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course_Model course_Model = await db.Course_Model.FindAsync(id);
            if (course_Model == null)
            {
                return HttpNotFound();
            }
            return View(course_Model);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Course_Model course_Model = await db.Course_Model.FindAsync(id);
            db.Course_Model.Remove(course_Model);
            await db.SaveChangesAsync();
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
