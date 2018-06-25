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
    public class ParadasCopoController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ParadasCopo
        public ActionResult Index()
        {
            var paradaCopoes = db.ParadaCopoes.Include(p => p.OrdemProducaoCopo);
            return View(paradaCopoes.ToList());
        }

        // GET: ParadasCopo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaCopo paradaCopo = db.ParadaCopoes.Find(id);
            if (paradaCopo == null)
            {
                return HttpNotFound();
            }
            return View(paradaCopo);
        }

        // GET: ParadasCopo/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima");
            return View();
        }

        // POST: ParadasCopo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoCopoId")] ParadaCopo paradaCopo)
        {
            if (ModelState.IsValid)
            {
                db.ParadaCopoes.Add(paradaCopo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", paradaCopo.OrdemProducaoCopoId);
            return View(paradaCopo);
        }

        // GET: ParadasCopo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaCopo paradaCopo = db.ParadaCopoes.Find(id);
            if (paradaCopo == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", paradaCopo.OrdemProducaoCopoId);
            return View(paradaCopo);
        }

        // POST: ParadasCopo/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoCopoId")] ParadaCopo paradaCopo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paradaCopo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", paradaCopo.OrdemProducaoCopoId);
            return View(paradaCopo);
        }

        // GET: ParadasCopo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaCopo paradaCopo = db.ParadaCopoes.Find(id);
            if (paradaCopo == null)
            {
                return HttpNotFound();
            }
            return View(paradaCopo);
        }

        // POST: ParadasCopo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParadaCopo paradaCopo = db.ParadaCopoes.Find(id);
            db.ParadaCopoes.Remove(paradaCopo);
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
