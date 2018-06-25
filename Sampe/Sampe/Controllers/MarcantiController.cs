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
    public class MarcantiController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Marcanti
        public ActionResult Index()
        {
            return View(db.Marcantis.ToList());
        }

        // GET: Marcanti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcanti marcanti = db.Marcantis.Find(id);
            if (marcanti == null)
            {
                return HttpNotFound();
            }
            return View(marcanti);
        }

        // GET: Marcanti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Marcanti/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarcantiId,NomeEmpresa,Cnpj,Cep,Uf,Cidade,Rua,Bairro,Complemento,Numero,Telefone,Email")] Marcanti marcanti)
        {
            if (ModelState.IsValid)
            {
                db.Marcantis.Add(marcanti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marcanti);
        }

        // GET: Marcanti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcanti marcanti = db.Marcantis.Find(id);
            if (marcanti == null)
            {
                return HttpNotFound();
            }
            return View(marcanti);
        }

        // POST: Marcanti/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarcantiId,NomeEmpresa,Cnpj,Cep,Uf,Cidade,Rua,Bairro,Complemento,Numero,Telefone,Email")] Marcanti marcanti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marcanti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marcanti);
        }

        // GET: Marcanti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marcanti marcanti = db.Marcantis.Find(id);
            if (marcanti == null)
            {
                return HttpNotFound();
            }
            return View(marcanti);
        }

        // POST: Marcanti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marcanti marcanti = db.Marcantis.Find(id);
            db.Marcantis.Remove(marcanti);
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
