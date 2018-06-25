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
    public class ParadaRefugosController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ParadaRefugos
        public ActionResult Index()
        {
            var paradaRefugoes = db.ParadaRefugoes.Include(p => p.OrdemProducaoRefugo);
            return View(paradaRefugoes.ToList());
        }

        // GET: ParadaRefugos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaRefugo paradaRefugo = db.ParadaRefugoes.Find(id);
            if (paradaRefugo == null)
            {
                return HttpNotFound();
            }
            return View(paradaRefugo);
        }

        // GET: ParadaRefugos/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto");
            return View();
        }

        // POST: ParadaRefugos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParadaRefugoId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoRefugoId")] ParadaRefugo paradaRefugo)
        {
            if (ModelState.IsValid)
            {
                db.ParadaRefugoes.Add(paradaRefugo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", paradaRefugo.OrdemProducaoRefugoId);
            return View(paradaRefugo);
        }

        // GET: ParadaRefugos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaRefugo paradaRefugo = db.ParadaRefugoes.Find(id);
            if (paradaRefugo == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", paradaRefugo.OrdemProducaoRefugoId);
            return View(paradaRefugo);
        }

        // POST: ParadaRefugos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ParadaRefugoId,HoraParada,HoraRetorno,Motivo,Observacoes,OrdemProducaoRefugoId")] ParadaRefugo paradaRefugo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paradaRefugo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", paradaRefugo.OrdemProducaoRefugoId);
            return View(paradaRefugo);
        }

        // GET: ParadaRefugos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParadaRefugo paradaRefugo = db.ParadaRefugoes.Find(id);
            if (paradaRefugo == null)
            {
                return HttpNotFound();
            }
            return View(paradaRefugo);
        }

        // POST: ParadaRefugos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ParadaRefugo paradaRefugo = db.ParadaRefugoes.Find(id);
            db.ParadaRefugoes.Remove(paradaRefugo);
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
