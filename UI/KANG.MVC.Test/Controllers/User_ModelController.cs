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
    public class User_ModelController : Controller
    {
        private KANGMVCTestContext db = new KANGMVCTestContext();

        // GET: User_Model
        public async Task<ActionResult> Index()
        {
            return View(await db.User_Model.ToListAsync());
        }

        // GET: User_Model/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Model user_Model = await db.User_Model.FindAsync(id);
            if (user_Model == null)
            {
                return HttpNotFound();
            }
            return View(user_Model);
        }

        // GET: User_Model/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_Model/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name")] User_Model user_Model)
        {
            if (ModelState.IsValid)
            {
                db.User_Model.Add(user_Model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(user_Model);
        }

        // GET: User_Model/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Model user_Model = await db.User_Model.FindAsync(id);
            if (user_Model == null)
            {
                return HttpNotFound();
            }
            return View(user_Model);
        }

        // POST: User_Model/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name")] User_Model user_Model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Model).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user_Model);
        }

        // GET: User_Model/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Model user_Model = await db.User_Model.FindAsync(id);
            if (user_Model == null)
            {
                return HttpNotFound();
            }
            return View(user_Model);
        }

        // POST: User_Model/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User_Model user_Model = await db.User_Model.FindAsync(id);
            db.User_Model.Remove(user_Model);
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
