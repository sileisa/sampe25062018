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
    [Authorize]
    public class AtividadeOSController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: AtividadeOS
        public ActionResult Index()
        {
            return View(db.AtividadeOS.ToList().OrderBy(u => u.NomeAtvOs));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "AtividadeOSId,NomeAtvOs")] AtividadeOS atividadeOS)
        {
            var atividades = db.AtividadeOS.ToList();
            var x = db.AtividadeOS.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {
                cont++;

                if (atividadeOS.NomeAtvOs == atividades[i].NomeAtvOs)
                {
                    ViewBag.Error = "Atividade já existente";
                    break;
                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.AtividadeOS.Add(atividadeOS);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(db.AtividadeOS.ToList());
        }
        // GET: AtividadeOS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeOS atividadeOS = db.AtividadeOS.Find(id);
            if (atividadeOS == null)
            {
                return HttpNotFound();
            }
            return View(atividadeOS);
        }

        // GET: AtividadeOS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AtividadeOS/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AtividadeOSId,NomeAtvOs")] AtividadeOS atividadeOS)
        {
            if (ModelState.IsValid)
            {
                db.AtividadeOS.Add(atividadeOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(atividadeOS);
        }

        // GET: AtividadeOS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeOS atividadeOS = db.AtividadeOS.Find(id);
            if (atividadeOS == null)
            {
                return HttpNotFound();
            }
            return View(atividadeOS);
        }

        // POST: AtividadeOS/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AtividadeOSId,NomeAtvOs")] AtividadeOS atividadeOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atividadeOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(atividadeOS);
        }

        // GET: AtividadeOS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtividadeOS atividadeOS = db.AtividadeOS.Find(id);
            if (atividadeOS == null)
            {
                return HttpNotFound();
            }
            return View(atividadeOS);
        }

        // POST: AtividadeOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AtividadeOS atividadeOS = db.AtividadeOS.Find(id);
			var busca = db.FormularioOSAtividade.Where(o => o.AtividadeOSId == id);
			if (busca.Count() > 0)
			{
				ViewBag.Error = "Não é possível deletar esta Atividade, pois está sendo utilizada em outras partes do sistema.";
			}
			else { 
			db.AtividadeOS.Remove(atividadeOS);
            db.SaveChanges();
            return RedirectToAction("Index");
			}
			return View();
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
