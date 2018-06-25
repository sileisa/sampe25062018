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
    public class KitPecasController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: KitPecas
        public ActionResult Index()
        {
            var kitPecas = db.KitPecas.Include(k => k.OrdemProducaoKit).Include(k => k.OrdemProducaoPeca);
            return View(kitPecas.ToList());
        }

        // GET: KitPecas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitPeca kitPeca = db.KitPecas.Find(id);
            if (kitPeca == null)
            {
                return HttpNotFound();
            }
            return View(kitPeca);
        }

        // GET: KitPecas/Create
        public ActionResult Create()
        {
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio");
            ViewBag.CodigoIdentificador = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima");
            return View();
        }

        // POST: KitPecas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KitPecaId,CodigoIdentificador,CodigoIdentificadorKit")] KitPeca kitPeca)
        {
            if (ModelState.IsValid)
            {
                db.KitPecas.Add(kitPeca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", kitPeca.CodigoIdentificadorKit);
            ViewBag.CodigoIdentificador = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", kitPeca.CodigoIdentificador);
            return View(kitPeca);
        }

        // GET: KitPecas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitPeca kitPeca = db.KitPecas.Find(id);
            if (kitPeca == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", kitPeca.CodigoIdentificadorKit);
            ViewBag.CodigoIdentificador = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", kitPeca.CodigoIdentificador);
            return View(kitPeca);
        }

        // POST: KitPecas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KitPecaId,CodigoIdentificador,CodigoIdentificadorKit")] KitPeca kitPeca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kitPeca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoIdentificadorKit = new SelectList(db.OrdemProducaoKits, "CodigoIdentificadorKit", "ProdIncio", kitPeca.CodigoIdentificadorKit);
            ViewBag.CodigoIdentificador = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", kitPeca.CodigoIdentificador);
            return View(kitPeca);
        }

        // GET: KitPecas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KitPeca kitPeca = db.KitPecas.Find(id);
            if (kitPeca == null)
            {
                return HttpNotFound();
            }
            return View(kitPeca);
        }

        // POST: KitPecas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KitPeca kitPeca = db.KitPecas.Find(id);
            db.KitPecas.Remove(kitPeca);
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
