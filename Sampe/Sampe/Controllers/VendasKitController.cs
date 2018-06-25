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
    public class VendasKitController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: VendasKit
        public ActionResult Index()
        {
            var vendaKits = db.VendaKits.Include(v => v.Especificacao).Include(v => v.ExpedicaoKit);
            return View(vendaKits.ToList());
        }

        // GET: VendasKit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaKit vendaKit = db.VendaKits.Find(id);
            if (vendaKit == null)
            {
                return HttpNotFound();
            }
            return View(vendaKit);
        }

        // GET: VendasKit/Create
        public ActionResult Create()
        {
            ViewBag.EspecificacaoId = new SelectList(db.Especificacaos, "EspecificacaoId", "TipoKit");
            ViewBag.ExpedicaoKitId = new SelectList(db.ExpedicaoKits, "ExpedicaoKitId", "ExpedicaoKitId");
            return View();
        }

        // POST: VendasKit/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendaKitId,ValorUnitario,Subtotal,ExpedicaoKitId,EspecificacaoId")] VendaKit vendaKit)
        {
            if (ModelState.IsValid)
            {
                db.VendaKits.Add(vendaKit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecificacaoId = new SelectList(db.Especificacaos, "EspecificacaoId", "TipoKit", vendaKit.EspecificacaoId);
            ViewBag.ExpedicaoKitId = new SelectList(db.ExpedicaoKits, "ExpedicaoKitId", "ExpedicaoKitId", vendaKit.ExpedicaoKitId);
            return View(vendaKit);
        }

        // GET: VendasKit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaKit vendaKit = db.VendaKits.Find(id);
            if (vendaKit == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecificacaoId = new SelectList(db.Especificacaos, "EspecificacaoId", "TipoKit", vendaKit.EspecificacaoId);
            ViewBag.ExpedicaoKitId = new SelectList(db.ExpedicaoKits, "ExpedicaoKitId", "ExpedicaoKitId", vendaKit.ExpedicaoKitId);
            return View(vendaKit);
        }

        // POST: VendasKit/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VendaKitId,ValorUnitario,Subtotal,ExpedicaoKitId,EspecificacaoId")] VendaKit vendaKit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendaKit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EspecificacaoId = new SelectList(db.Especificacaos, "EspecificacaoId", "TipoKit", vendaKit.EspecificacaoId);
            ViewBag.ExpedicaoKitId = new SelectList(db.ExpedicaoKits, "ExpedicaoKitId", "ExpedicaoKitId", vendaKit.ExpedicaoKitId);
            return View(vendaKit);
        }

        // GET: VendasKit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendaKit vendaKit = db.VendaKits.Find(id);
            if (vendaKit == null)
            {
                return HttpNotFound();
            }
            return View(vendaKit);
        }

        // POST: VendasKit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendaKit vendaKit = db.VendaKits.Find(id);
            db.VendaKits.Remove(vendaKit);
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
