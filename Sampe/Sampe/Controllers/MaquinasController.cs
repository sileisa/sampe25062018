using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sampe;
using Sampe.Models;

namespace Sampe.Controllers
{
	[Authorize(Roles = "Acesso Total, Acesso Administrador,Acesso Supervisor,Acesso Produção")]
	public class MaquinasController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: Maquinas
		public ActionResult Index()
		{
			return View(db.Maquinas.ToList().OrderBy(u => u.NomeMaquina));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Index([Bind(Include = "MaquinaId,NomeMaquina")] Maquina maquina)
		{
			var maquinas = db.Maquinas.ToList();
			var x = db.Maquinas.Count();
			var cont = 0;
			for (var i = 0; i <= cont; i++)
			{
				cont++;

				if (maquina.NomeMaquina == maquinas[i].NomeMaquina)
				{

					ViewBag.Error = "Máquina já existente";
					break;
				}
				else if (cont == x)
				{
					if (ModelState.IsValid)
					{
						db.Maquinas.Add(maquina);
						db.SaveChanges();
						return RedirectToAction("Index");
					}
				}
			}

			return View(db.Maquinas.ToList());

		}
		// GET: Maquinas/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Maquina maquina = db.Maquinas.Find(id);
			if (maquina == null)
			{
				return HttpNotFound();
			}
			return View(maquina);
		}

		// GET: Maquinas/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Maquinas/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "MaquinaId,NomeMaquina")] Maquina maquina)
		{
			if (ModelState.IsValid)
			{
				db.Maquinas.Add(maquina);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(maquina);
		}

		// GET: Maquinas/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Maquina maquina = db.Maquinas.Find(id);
			if (maquina == null)
			{
				return HttpNotFound();
			}
			return View(maquina);
		}

		// POST: Maquinas/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "MaquinaId,NomeMaquina")] Maquina maquina)
		{
			if (ModelState.IsValid)
			{
				db.Entry(maquina).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(maquina);
		}

		// GET: Maquinas/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Maquina maquina = db.Maquinas.Find(id);
			if (maquina == null)
			{
				return HttpNotFound();
			}
			return View(maquina);
		}

		// POST: Maquinas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Maquina maquina = db.Maquinas.Find(id);

			var busca = db.OrdemProducaoPecas.Where(o => o.MaquinaId == id).ToList();
			var busca2 = db.OrdemProducaoCopoes.Where(o => o.MaquinaId == id).ToList();
			var busca3 = db.FormularioTrocaMoldes.Where(o => o.MaquinaId == id).ToList();
			var busca4 = db.FormularioOrdemServicoes.Where(o => o.MaquinaId == id).ToList();
			if ((busca.Count() > 0) || (busca2.Count() > 0) || (busca3.Count() > 0) || (busca4.Count() > 0))
			{
				ViewBag.Error = "Não é possível deletar esta Máquina, pois está sendo utilizada em outras partes do sistema.";
			}
			else
			{
				db.Maquinas.Remove(maquina);
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
