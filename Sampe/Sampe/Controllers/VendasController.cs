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
    public class VendasController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Vendas
        public ActionResult Index()
        {
            var vendas = db.Vendas.Include(v => v.EspecificacaoCopo).Include(v => v.ExpedicaoCopo);
            return View(vendas.ToList());
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            ViewBag.EspecificacaoCopoId = new SelectList(db.EspecificacaoCopoes, "EspecificacaoCopoId", "LoteMaster");
            ViewBag.ExpedicaoCopoId = new SelectList(db.ExpedicaoCopoes, "ExpedicaoId", "ExpedicaoId");
            return View();
        }

        // POST: Vendas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VendaId,ValorUnitario,Subtotal,ExpedicaoCopoId,EspecificacaoCopoId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Vendas.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EspecificacaoCopoId = new SelectList(db.EspecificacaoCopoes, "EspecificacaoCopoId", "LoteMaster", venda.EspecificacaoCopoId);
            ViewBag.ExpedicaoCopoId = new SelectList(db.ExpedicaoCopoes, "ExpedicaoId", "ExpedicaoId", venda.ExpedicaoCopoId);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.EspecificacaoCopoId = new SelectList(db.EspecificacaoCopoes, "EspecificacaoCopoId", "LoteMaster", venda.EspecificacaoCopoId);
            ViewBag.ExpedicaoCopoId = new SelectList(db.ExpedicaoCopoes, "ExpedicaoId", "ExpedicaoId", venda.ExpedicaoCopoId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VendaId,ValorUnitario,Subtotal,ExpedicaoCopoId,EspecificacaoCopoId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EspecificacaoCopoId = new SelectList(db.EspecificacaoCopoes, "EspecificacaoCopoId", "LoteMaster", venda.EspecificacaoCopoId);
            ViewBag.ExpedicaoCopoId = new SelectList(db.ExpedicaoCopoes, "ExpedicaoId", "ExpedicaoId", venda.ExpedicaoCopoId);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = db.Vendas.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = db.Vendas.Find(id);
            db.Vendas.Remove(venda);
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
