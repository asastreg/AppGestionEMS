using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;
using Microsoft.AspNet.Identity;

namespace AppGestionEMS.Controllers
{
	[Authorize(Roles = "alumno")]
	public class MisEvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MisEvaluaciones
        public ActionResult Index()
        {
                      string currentUserId = User.Identity.GetUserId();
			var evaluaciones = db.Evaluaciones.Include(e => e.Curso).Include(e => e.Grupo).Include(e => e.User).Where(p => p.UserId == currentUserId);
            return View(evaluaciones.ToList());
        }

        // GET: MisEvaluaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Create
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso");
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: MisEvaluaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CursoId,GrupoId,Tipo_Evalu,Nota_Pr,Nota_Ev_C,Nota_P1,Nota_P2,Nota_P3,Nota_P4,Nota_Final")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Evaluaciones.Add(evaluaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", evaluaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", evaluaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", evaluaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", evaluaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // POST: MisEvaluaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,CursoId,GrupoId,Tipo_Evalu,Nota_Pr,Nota_Ev_C,Nota_P1,Nota_P2,Nota_P3,Nota_P4,Nota_Final")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Curso", evaluaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Grupo", evaluaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // GET: MisEvaluaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // POST: MisEvaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            db.Evaluaciones.Remove(evaluaciones);
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
