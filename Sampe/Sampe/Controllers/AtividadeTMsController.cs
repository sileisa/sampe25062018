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
	public class AtividadeTMsController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: AtividadeTMs
		public ActionResult Index()
		{
			return View(db.AtividadeTMs.ToList().OrderBy(u => u.NomeAtvTm));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index([Bind(Include = "AtividadeTMId,NomeAtvTm")] AtividadeTM atividadeTm)
		{
			var atividades = db.AtividadeTMs.ToList();
			var x = db.AtividadeTMs.Count();
			var cont = 0;
			for (var i = 0; i <= cont; i++)
			{
				cont++;

				if (atividadeTm.NomeAtvTm == atividades[i].NomeAtvTm)
				{
					ViewBag.Error = "Atividade já existente";
					break;
				}
				else if (cont == x)
				{
					if (ModelState.IsValid)
					{
						db.AtividadeTMs.Add(atividadeTm);
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
			}

			return View(db.AtividadeTMs.ToList());
		}

		// GET: AtividadeTMs/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AtividadeTM atividadeTM = db.AtividadeTMs.Find(id);
			if (atividadeTM == null)
			{
				return HttpNotFound();
			}
			return View(atividadeTM);
		}

		// GET: AtividadeTMs/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: AtividadeTMs/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "AtividadeTMId,NomeAtvTm")] AtividadeTM atividadeTM)
		{
			if (ModelState.IsValid)
			{
				db.AtividadeTMs.Add(atividadeTM);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(atividadeTM);
		}

		// GET: AtividadeTMs/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AtividadeTM atividadeTM = db.AtividadeTMs.Find(id);
			if (atividadeTM == null)
			{
				return HttpNotFound();
			}
			return View(atividadeTM);
		}

		// POST: AtividadeTMs/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "AtividadeTMId,NomeAtvTm")] AtividadeTM atividadeTM)
		{
			if (ModelState.IsValid)
			{
				db.Entry(atividadeTM).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(atividadeTM);
		}

		// GET: AtividadeTMs/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			AtividadeTM atividadeTM = db.AtividadeTMs.Find(id);
			if (atividadeTM == null)
			{
				return HttpNotFound();
			}
			return View(atividadeTM);
		}

		// POST: AtividadeTMs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			AtividadeTM atividadeTM = db.AtividadeTMs.Find(id);
			var busca = db.FormularioTMAtividade.Where(o => o.AtividadeTMId == id);
			if (busca.Count() > 0)
			{
				ViewBag.Error = "Não é possível deletar esta Atividade, pois está sendo utilizada em outras partes do sistema.";
			}
			else
			{
				db.AtividadeTMs.Remove(atividadeTM);
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
