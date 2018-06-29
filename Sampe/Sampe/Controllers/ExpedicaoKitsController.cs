using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Sampe.Models;

namespace Sampe.Controllers
{
	public class ExpedicaoKitsController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: ExpedicaoKits
		public ActionResult Index(int? page)
		{
			var expedicaoKits = db.ExpedicaoKits.Include(e => e.Cliente).Include(e => e.Marcanti).OrderByDescending(e=>e.Vencimento);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			return View(expedicaoKits.ToPagedList(pageNumber, pageSize));
		}

		// GET: ExpedicaoKits/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ExpedicaoKit expedicaoKit = db.ExpedicaoKits.Find(id);
			var busca = from venda in db.VendaKits
						where venda.ExpedicaoKitId == id
						select venda;
			expedicaoKit.VendasKit = busca.Include(o => o.Especificacao).Include(o => o.Especificacao.TipoKit).ToList();
			if (expedicaoKit == null)
			{
				return HttpNotFound();
			}
			return View(expedicaoKit);
		}

        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoKits/Create
        public ActionResult Create()
		{
			ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
			ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa");
			ViewBag.Kits = db.Especificacaos.Select(p => new SelectListItem
			{
				Text = p.OrdemProducaoKit.CodigoIdentificadorKit + ", Tipo: " + p.TipoKit + ", Quantidade: " + p.QuantProduzido,
				Value = p.EspecificacaoId.ToString(),
			});
			return View();
		}

		// POST: ExpedicaoKits/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ExpedicaoKitId,ValorTotal,Vencimento,ClienteId,MarcantiId")] ExpedicaoKit expedicaoKit)
		{
			var a = Request.Form["Kits"];
			var c = Request.Form["VendaKit.Quantidade"];
			expedicaoKit.MarcantiId = 1;
			//if (ModelState.IsValid)
			//{
			if (a != null)
			{
				var kit = a.Split(',').Select(Int32.Parse).ToList();


				List<string> b = new List<string>(Request.Form.GetValues("VendaKit.ValorUnitario"));
				var val = b.Select(Double.Parse).ToList();

				var quant = c.Split(',').Select(Int32.Parse).ToList();
				List<VendaKit> vendas = new List<VendaKit>();
				for (var x = 0; x < kit.Count(); x++)
				{
					VendaKit v1 = new VendaKit();
					v1.ValorUnitario = val[x];
					v1.ExpedicaoKitId = expedicaoKit.ExpedicaoKitId;
					v1.EspecificacaoId = kit[x];
					v1.Quantidade = quant[x];
					var teste = db.Especificacaos.Find(v1.EspecificacaoId);
					v1.CalcSubtotal(val[x], quant[x]);
					expedicaoKit.CalcValorTotal(v1.Subtotal);
					vendas.Add(v1);
				}
				expedicaoKit.VendasKit = vendas;
			}


			db.ExpedicaoKits.Add(expedicaoKit);
			db.SaveChanges();
			return RedirectToAction("Index");
			//}

			/*ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoKit.ClienteId);
			ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoKit.MarcantiId);
			return View(expedicaoKit);*/
		}

        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoKits/Edit/5
        public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ExpedicaoKit expedicaoKit = db.ExpedicaoKits.Find(id);
			if (expedicaoKit == null)
			{
				return HttpNotFound();
			}
			ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoKit.ClienteId);
			ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoKit.MarcantiId);
			return View(expedicaoKit);
		}

		// POST: ExpedicaoKits/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ExpedicaoKitId,ValorTotal,ClienteId,MarcantiId")] ExpedicaoKit expedicaoKit)
		{
			if (ModelState.IsValid)
			{
				db.Entry(expedicaoKit).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoKit.ClienteId);
			ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoKit.MarcantiId);
			return View(expedicaoKit);
		}

        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoKits/Delete/5
        public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ExpedicaoKit expedicaoKit = db.ExpedicaoKits.Find(id);
            db.Entry(expedicaoKit).Reference(u => u.Cliente).Load();
            if (expedicaoKit == null)
			{
				return HttpNotFound();
			}
			return View(expedicaoKit);
		}

		// POST: ExpedicaoKits/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			ExpedicaoKit expedicaoKit = db.ExpedicaoKits.Find(id);
			db.ExpedicaoKits.Remove(expedicaoKit);
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
