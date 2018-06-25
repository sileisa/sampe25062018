using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sampe.Models;

namespace Sampe.Controllers
{
    public class ParadasController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Paradas
        public ActionResult Index()
        {
            var paradas = db.Paradas.Include(p => p.OrdemProducaoPeca);
            return View(paradas.ToList());
        }

        // GET: Paradas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            return View(parada);
        }

        // GET: Paradas/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima");
            return View();
        }

        // POST: Paradas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoPecaId")] Parada parada)
        {
            if (ModelState.IsValid)
            {
                db.Paradas.Add(parada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", parada.OrdemProducaoPecaId);
            return View(parada);
        }

        // GET: Paradas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", parada.OrdemProducaoPecaId);
            return View(parada);
        }

        // POST: Paradas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoPecaId")] Parada parada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(parada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", parada.OrdemProducaoPecaId);
            return View(parada);
        }

        // GET: Paradas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parada parada = db.Paradas.Find(id);
            if (parada == null)
            {
                return HttpNotFound();
            }
            return View(parada);
        }

        // POST: Paradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Parada parada = db.Paradas.Find(id);
            db.Paradas.Remove(parada);
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
