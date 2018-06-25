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
    public class EspecificacoesCopoController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: EspecificacoesCopo
        public ActionResult Index()
        {
            var especificacaoCopoes = db.EspecificacaoCopoes.Include(e => e.Cor).Include(e => e.OrdemProducaoCopo);
            return View(especificacaoCopoes.ToList());
        }

        // GET: EspecificacoesCopo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoCopo especificacaoCopo = db.EspecificacaoCopoes.Find(id);
            if (especificacaoCopo == null)
            {
                return HttpNotFound();
            }
            return View(especificacaoCopo);
        }

        // GET: EspecificacoesCopo/Create
        public ActionResult Create()
        {
            ViewBag.CorId = new SelectList(db.Cors, "CorId", "NomeCor");
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima");
            return View();
        }

        // POST: EspecificacoesCopo/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecificacaoCopoId,CorId,UniProd,LoteMaster,OrdemProducaoCopoId")] EspecificacaoCopo especificacaoCopo)
        {
            if (ModelState.IsValid)
            {
                db.EspecificacaoCopoes.Add(especificacaoCopo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CorId = new SelectList(db.Cors, "CorId", "NomeCor", especificacaoCopo.CorId);
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", especificacaoCopo.OrdemProducaoCopoId);
            return View(especificacaoCopo);
        }

        // GET: EspecificacoesCopo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoCopo especificacaoCopo = db.EspecificacaoCopoes.Find(id);
            if (especificacaoCopo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CorId = new SelectList(db.Cors, "CorId", "NomeCor", especificacaoCopo.CorId);
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", especificacaoCopo.OrdemProducaoCopoId);
            return View(especificacaoCopo);
        }

        // POST: EspecificacoesCopo/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecificacaoCopoId,CorId,UniProd,LoteMaster,OrdemProducaoCopoId")] EspecificacaoCopo especificacaoCopo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especificacaoCopo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CorId = new SelectList(db.Cors, "CorId", "NomeCor", especificacaoCopo.CorId);
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", especificacaoCopo.OrdemProducaoCopoId);
            return View(especificacaoCopo);
        }

        // GET: EspecificacoesCopo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecificacaoCopo especificacaoCopo = db.EspecificacaoCopoes.Find(id);
            if (especificacaoCopo == null)
            {
                return HttpNotFound();
            }
            return View(especificacaoCopo);
        }

        // POST: EspecificacoesCopo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EspecificacaoCopo especificacaoCopo = db.EspecificacaoCopoes.Find(id);
            db.EspecificacaoCopoes.Remove(especificacaoCopo);
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
