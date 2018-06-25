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
    public class ParadaKitsController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ParadaKits
        public ActionResult Index()
        {
            var paradaKits = db.ParadaKits.Include(p => p.OrdemProducaoKit);
            return View(paradaKits.ToList());
        }

        // GET: ParadaKits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaKit paradaKit = db.ParadaKits.Find(id);
            if (paradaKit == null)
            {
                return HttpNotFound();
            }
            return View(paradaKit);
        }

        // GET: ParadaKits/Create
        public ActionResult Create()
        {
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio");
            return View();
        }

        // POST: ParadaKits/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,CodigoIdentificadorKit")] ParadaKit paradaKit)
        {
            if (ModelState.IsValid)
            {
                db.ParadaKits.Add(paradaKit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", paradaKit.CodigoIdentificadorKit);
            return View(paradaKit);
        }

        // GET: ParadaKits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaKit paradaKit = db.ParadaKits.Find(id);
            if (paradaKit == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", paradaKit.CodigoIdentificadorKit);
            return View(paradaKit);
        }

        // POST: ParadaKits/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParadaId,HoraParada,HoraRetorno,Motivo,Observacoes,CodigoIdentificadorKit")] ParadaKit paradaKit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paradaKit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", paradaKit.CodigoIdentificadorKit);
            return View(paradaKit);
        }

        // GET: ParadaKits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaKit paradaKit = db.ParadaKits.Find(id);
            if (paradaKit == null)
            {
                return HttpNotFound();
            }
            return View(paradaKit);
        }

        // POST: ParadaKits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParadaKit paradaKit = db.ParadaKits.Find(id);
            db.ParadaKits.Remove(paradaKit);
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
