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
	public class CorPecasController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: CorPecas
		public ActionResult Index()
		{
			return View(db.CorPecas.ToList().OrderBy(u => u.NomeCorPeca));
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CorId,NomeCorPeca")] CorPeca cor)
        {
            var cores = db.CorPecas.ToList();
            var x = db.CorPecas.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {
                cont++;

                if (cor.NomeCorPeca == cores[i].NomeCorPeca)
                {

                    ViewBag.Error = "Cor já existente";
                    break;
                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.CorPecas.Add(cor);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(db.CorPecas.ToList());
        }
        // GET: CorPecas/Details/5
        public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CorPeca corPeca = db.CorPecas.Find(id);
			if (corPeca == null)
			{
				return HttpNotFound();
			}
			return View(corPeca);
		}

		// GET: CorPecas/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CorPecas/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CorId,NomeCor")] CorPeca corPeca)
		{
			if (ModelState.IsValid)
			{
				db.CorPecas.Add(corPeca);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(corPeca);
		}

		// GET: CorPecas/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CorPeca corPeca = db.CorPecas.Find(id);
			if (corPeca == null)
			{
				return HttpNotFound();
			}
			return View(corPeca);
		}

		// POST: CorPecas/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CorId,NomeCor")] CorPeca corPeca)
		{
			if (ModelState.IsValid)
			{
				db.Entry(corPeca).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(corPeca);
		}

		// GET: CorPecas/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CorPeca corPeca = db.CorPecas.Find(id);
			if (corPeca == null)
			{
				return HttpNotFound();
			}
			return View(corPeca);
		}

		// POST: CorPecas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			CorPeca corPeca = db.CorPecas.Find(id);
			var busca = db.OrdemProducaoPecas.Where(o => o.CorPecaId == id);
			var busca2 = db.Especificacaos.Where(o => o.CorPecaId == id);
			var busca3 = db.EspecificacaoRefugoes.Where(o => o.CorPecaId == id);
			if ((busca.Count() > 0) || (busca2.Count() > 0) || (busca3.Count() > 0))
			{
				ViewBag.Error = "Não é possível deletar esta Cor, pois está sendo utilizada em outras partes do sistema.";
			}
			else
			{
				db.CorPecas.Remove(corPeca);
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
