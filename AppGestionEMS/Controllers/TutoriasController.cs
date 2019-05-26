using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;

namespace AppGestionEMS.Controllers
{
	[Authorize(Roles = "admin, profesor, alumno")]
    public class TutoriasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tutorias
        public ActionResult Index()
        {
            var tutorias = db.Tutorias.Include(t => t.Curso).Include(t => t.Practica).Include(t => t.User);
            return View(tutorias.ToList());
        }

		// GET: Tutorias/Details/5
		[Authorize(Roles = "admin, profesor, alumno")]
		public ActionResult Details(string user, int? curso, int? practica)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorias tutorias = db.Tutorias.Find(user, curso, practica);
            if (tutorias == null)
            {
                return HttpNotFound();
            }
            return View(tutorias);
        }

		// GET: Tutorias/Create
		[Authorize(Roles = "admin, profesor")]
		public ActionResult Create()
        {
			var profesores = from user in db.Users
						  from u_r in user.Roles
						  join rol in db.Roles on u_r.RoleId equals rol.Id
						  where rol.Name == "profesor"
						  select user.UserName;
			ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso");
            ViewBag.PracticaId = new SelectList(db.Practicas, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users.Where(u => profesores.Contains(u.UserName)), "Id", "Name");
            return View();
        }

        // POST: Tutorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CursoId,PracticaId")] Tutorias tutorias)
        {
            if (ModelState.IsValid)
            {
                db.Tutorias.Add(tutorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", tutorias.CursoId);
            ViewBag.PracticaId = new SelectList(db.Practicas, "Id", "Nombre", tutorias.PracticaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tutorias.UserId);
            return View(tutorias);
        }

		// GET: Tutorias/Edit/5
		[Authorize(Roles = "admin, profesor")]
		public ActionResult Edit(string user, int? curso, int? practica)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorias tutorias = db.Tutorias.Find(user, curso, practica);
            if (tutorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", tutorias.CursoId);
            ViewBag.PracticaId = new SelectList(db.Practicas, "Id", "Nombre", tutorias.PracticaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tutorias.UserId);
            return View(tutorias);
        }

        // POST: Tutorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,CursoId,PracticaId")] Tutorias tutorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", tutorias.CursoId);
            ViewBag.PracticaId = new SelectList(db.Practicas, "Id", "Nombre", tutorias.PracticaId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", tutorias.UserId);
            return View(tutorias);
        }

		// GET: Tutorias/Delete/5
		[Authorize(Roles = "admin, profesor")]
		public ActionResult Delete(string user, int? curso, int? practica)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorias tutorias = db.Tutorias.Find(user, curso, practica);
            if (tutorias == null)
            {
                return HttpNotFound();
            }
            return View(tutorias);
        }

        // POST: Tutorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string user, int? curso, int? practica)
        {
            Tutorias tutorias = db.Tutorias.Find(user, curso, practica);
            db.Tutorias.Remove(tutorias);
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
