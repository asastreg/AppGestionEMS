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
    public class AsignacionDocentesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AsignacionDocentes
        public ActionResult Index()
        {
            var asignacionDocentes = db.AsignacionDocentes.Include(a => a.Curso).Include(a => a.Grupo).Include(a => a.User);
            return View(asignacionDocentes.ToList());
        }

		// GET: AsignacionDocentes/Details/5
		[Authorize(Roles = "admin, profesor, alumno")]
		public ActionResult Details(int? curso, int? grupo, string user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocentes asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            if (asignacionDocentes == null)
            {
                return HttpNotFound();
            }
            return View(asignacionDocentes);
        }

		// GET: AsignacionDocentes/Create
		[Authorize(Roles = "admin")]
		public ActionResult Create()
        {
            var profesores = from user in db.Users
                             from u_r in user.Roles
                             join rol in db.Roles on u_r.RoleId equals rol.Id
                             where rol.Name == "profesor"
                             select user.UserName;
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso");
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo");
            ViewBag.UserId = new SelectList(db.Users.Where(u => profesores.Contains(u.UserName)), "Id", "Name");
            return View();
        }

        // POST: AsignacionDocentes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CursoId,GrupoId")] AsignacionDocentes asignacionDocentes)
        {
            if (ModelState.IsValid)
            {
                db.AsignacionDocentes.Add(asignacionDocentes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", asignacionDocentes.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", asignacionDocentes.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", asignacionDocentes.UserId);
            return View(asignacionDocentes);
        }

		// GET: AsignacionDocentes/Edit/5
		[Authorize(Roles = "admin")]
		public ActionResult Edit(int? curso, int? grupo, string user)
        {
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocentes asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            if (asignacionDocentes == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", asignacionDocentes.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", asignacionDocentes.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", asignacionDocentes.UserId);
            return View(asignacionDocentes);
        }

        // POST: AsignacionDocentes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,CursoId,GrupoId")] AsignacionDocentes asignacionDocentes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asignacionDocentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", asignacionDocentes.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", asignacionDocentes.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", asignacionDocentes.UserId);
            return View(asignacionDocentes);
        }

		// GET: AsignacionDocentes/Delete/5
		[Authorize(Roles = "admin")]
		public ActionResult Delete(int? curso, int? grupo, string user)
        {
            if (curso == null || grupo == null ||user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsignacionDocentes asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            if (asignacionDocentes == null)
            {
                return HttpNotFound();
            }
            return View(asignacionDocentes);
        }

        // POST: AsignacionDocentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int curso, int grupo, string user)
        {
            AsignacionDocentes asignacionDocentes = db.AsignacionDocentes.Find(user, curso, grupo);
            db.AsignacionDocentes.Remove(asignacionDocentes);
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
