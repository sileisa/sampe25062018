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
	public class ExpectativasController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: Expectativas
		public ActionResult Index()
		{
			return View(db.Expectativas.ToList().OrderBy(u => u.Produto));
		}

		// GET: Expectativas/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Expectativa expectativa = db.Expectativas.Find(id);
			if (expectativa == null)
			{
				return HttpNotFound();
			}
			return View(expectativa);
		}

		// GET: Expectativas/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Expectativas/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ExpectativaId,Produto,CavidadeMolde,PesoPecaAproximado,PesoPecaCompleta,Ciclo,ProducaoEsperada,ProdInicio,ProdFim")] Expectativa expectativa)
		{
			if (ModelState.IsValid)
			{
				db.Expectativas.Add(expectativa);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(expectativa);
		}

		// GET: Expectativas/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Expectativa expectativa = db.Expectativas.Find(id);
			if (expectativa == null)
			{
				return HttpNotFound();
			}
			return View(expectativa);
		}

		// POST: Expectativas/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ExpectativaId,Produto,CavidadeMolde,PesoPecaAproximado,PesoPecaCompleta,Ciclo,ProducaoEsperada,ProdInicio,ProdFim")] Expectativa expectativa)
		{
			if (ModelState.IsValid)
			{
				db.Entry(expectativa).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(expectativa);
		}

		// GET: Expectativas/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Expectativa expectativa = db.Expectativas.Find(id);
			if (expectativa == null)
			{
				return HttpNotFound();
			}
			return View(expectativa);
		}

		// POST: Expectativas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Expectativa expectativa = db.Expectativas.Find(id);
			var busca = db.OrdemProducaoPecas.Where(o => o.ExpectativaId == id).ToList();
			var busca2 = db.OrdemProducaoCopoes.Where(o => o.ExpectativaId == id).ToList();		
			if ((busca.Count() > 0) || (busca2.Count() > 0))
			{
				ViewBag.Error = "Não é possível deletar esta Máquina, pois está sendo utilizada em outras partes do sistema.";
				return View();
			}
			else
			{
				db.Expectativas.Remove(expectativa);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
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
