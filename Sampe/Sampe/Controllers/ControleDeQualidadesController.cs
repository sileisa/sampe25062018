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
    public class ControleDeQualidadesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ControleDeQualidades
        public ActionResult Index()
        {
            var controleDeQualidades = db.ControleDeQualidades.Include(c => c.OrdemProducaoPeca);
            return View(controleDeQualidades.ToList());
        }

        // GET: ControleDeQualidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidade controleDeQualidade = db.ControleDeQualidades.Find(id);
            if (controleDeQualidade == null)
            {
                return HttpNotFound();
            }
            return View(controleDeQualidade);
        }

        // GET: ControleDeQualidades/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima");
            return View();
        }

        // POST: ControleDeQualidades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ControleDeQualidadeId,Ciclo,Hora,PesoDaPeca,Peso,Cor,Dimensao,Assinatura,Liberado,OrdemProducaoPecaId")] ControleDeQualidade controleDeQualidade)
        {
            if (ModelState.IsValid)
            {
                controleDeQualidade.ValidaInspecao();
                db.ControleDeQualidades.Add(controleDeQualidade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", controleDeQualidade.OrdemProducaoPecaId);
            return View(controleDeQualidade);
        }

        // GET: ControleDeQualidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidade controleDeQualidade = db.ControleDeQualidades.Find(id);
            if (controleDeQualidade == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", controleDeQualidade.OrdemProducaoPecaId);
            return View(controleDeQualidade);
        }

        // POST: ControleDeQualidades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ControleDeQualidadeId,Ciclo,Hora,PesoDaPeca,Peso,Cor,Dimensao,Assinatura,Liberado,OrdemProducaoPecaId")] ControleDeQualidade controleDeQualidade)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(controleDeQualidade).State = EntityState.Modified;
                if(controleDeQualidade.ValidaInspecao())db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoPecaId = new SelectList(db.OrdemProducaoPecas, "CodigoIdentificador", "MateriaPrima", controleDeQualidade.OrdemProducaoPecaId);
            return View(controleDeQualidade);
        }

        // GET: ControleDeQualidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidade controleDeQualidade = db.ControleDeQualidades.Find(id);
            if (controleDeQualidade == null)
            {
                return HttpNotFound();
            }
            return View(controleDeQualidade);
        }

        // POST: ControleDeQualidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControleDeQualidade controleDeQualidade = db.ControleDeQualidades.Find(id);
            db.ControleDeQualidades.Remove(controleDeQualidade);
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
