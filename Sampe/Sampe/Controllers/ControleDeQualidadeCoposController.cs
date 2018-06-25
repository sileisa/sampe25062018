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
    public class ControleDeQualidadeCoposController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ControleDeQualidadeCopos
        public ActionResult Index()
        {
            var controleDeQualidadeCopoes = db.ControleDeQualidadeCopoes.Include(c => c.OrdemProducaoCopo).Include(c => c.Usuario);
            return View(controleDeQualidadeCopoes.ToList());
        }

        // GET: ControleDeQualidadeCopos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidadeCopo controleDeQualidadeCopo = db.ControleDeQualidadeCopoes.Find(id);
            if (controleDeQualidadeCopo == null)
            {
                return HttpNotFound();
            }
            return View(controleDeQualidadeCopo);
        }

        // GET: ControleDeQualidadeCopos/Create
        public ActionResult Create()
        {
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario");
            return View();
        }

        // POST: ControleDeQualidadeCopos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ControleDeQualidadeCopoId,Ciclo,HoraInicial,HoraFinal,PesoDaPeca,PesoDaPeca2,Peso,Cor,Dimensao,Assinatura,Liberado,OrdemProducaoCopoId,UsuarioId")] ControleDeQualidadeCopo controleDeQualidadeCopo)
        {
            if (ModelState.IsValid)
            {
                db.ControleDeQualidadeCopoes.Add(controleDeQualidadeCopo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", controleDeQualidadeCopo.OrdemProducaoCopoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", controleDeQualidadeCopo.UsuarioId);
            return View(controleDeQualidadeCopo);
        }

        // GET: ControleDeQualidadeCopos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidadeCopo controleDeQualidadeCopo = db.ControleDeQualidadeCopoes.Find(id);
            if (controleDeQualidadeCopo == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", controleDeQualidadeCopo.OrdemProducaoCopoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", controleDeQualidadeCopo.UsuarioId);
            return View(controleDeQualidadeCopo);
        }

        // POST: ControleDeQualidadeCopos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ControleDeQualidadeCopoId,Ciclo,HoraInicial,HoraFinal,PesoDaPeca,PesoDaPeca2,Peso,Cor,Dimensao,Assinatura,Liberado,OrdemProducaoCopoId,UsuarioId")] ControleDeQualidadeCopo controleDeQualidadeCopo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(controleDeQualidadeCopo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrdemProducaoCopoId = new SelectList(db.OrdemProducaoCopoes, "CodigoIdentificador", "MateriaPrima", controleDeQualidadeCopo.OrdemProducaoCopoId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", controleDeQualidadeCopo.UsuarioId);
            return View(controleDeQualidadeCopo);
        }

        // GET: ControleDeQualidadeCopos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ControleDeQualidadeCopo controleDeQualidadeCopo = db.ControleDeQualidadeCopoes.Find(id);
            if (controleDeQualidadeCopo == null)
            {
                return HttpNotFound();
            }
            return View(controleDeQualidadeCopo);
        }

        // POST: ControleDeQualidadeCopos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ControleDeQualidadeCopo controleDeQualidadeCopo = db.ControleDeQualidadeCopoes.Find(id);
            db.ControleDeQualidadeCopoes.Remove(controleDeQualidadeCopo);
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
