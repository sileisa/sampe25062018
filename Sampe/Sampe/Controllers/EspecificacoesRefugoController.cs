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
    public class EspecificacoesRefugoController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: EspecificacoesRefugo
        public ActionResult Index()
        {
            var especificacaoRefugoes = db.EspecificacaoRefugoes.Include(e => e.OrdemProducaoRefugo);
            return View(especificacaoRefugoes.ToList());
        }

        // GET: EspecificacoesRefugo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoRefugo especificacaoRefugo = db.EspecificacaoRefugoes.Find(id);
            if (especificacaoRefugo == null)
            {
                return HttpNotFound();
            }
            return View(especificacaoRefugo);
        }

        // GET: EspecificacoesRefugo/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto");
            return View();
        }

        // POST: EspecificacoesRefugo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecificacaoRefugoId,Material,Cor,Peso,Limpeza,Status,OrdemProducaoRefugoId")] EspecificacaoRefugo especificacaoRefugo)
        {
            if (ModelState.IsValid)
            {
                db.EspecificacaoRefugoes.Add(especificacaoRefugo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", especificacaoRefugo.OrdemProducaoRefugoId);
            return View(especificacaoRefugo);
        }

        // GET: EspecificacoesRefugo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoRefugo especificacaoRefugo = db.EspecificacaoRefugoes.Find(id);
            if (especificacaoRefugo == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", especificacaoRefugo.OrdemProducaoRefugoId);
            return View(especificacaoRefugo);
        }

        // POST: EspecificacoesRefugo/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecificacaoRefugoId,Material,Cor,Peso,Limpeza,Status,OrdemProducaoRefugoId")] EspecificacaoRefugo especificacaoRefugo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especificacaoRefugo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoRefugoId = new SelectList(db.OrdemProducaoRefugoes, "OrdemProducaoRefugoId", "Produto", especificacaoRefugo.OrdemProducaoRefugoId);
            return View(especificacaoRefugo);
        }

        // GET: EspecificacoesRefugo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoRefugo especificacaoRefugo = db.EspecificacaoRefugoes.Find(id);
            if (especificacaoRefugo == null)
            {
                return HttpNotFound();
            }
            return View(especificacaoRefugo);
        }

        // POST: EspecificacoesRefugo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EspecificacaoRefugo especificacaoRefugo = db.EspecificacaoRefugoes.Find(id);
            db.EspecificacaoRefugoes.Remove(especificacaoRefugo);
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
