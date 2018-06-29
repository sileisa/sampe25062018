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
    public class ExpedicaoCoposController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: ExpedicaoCopos
        public ActionResult Index(int? page)
        {
            var expedicaoCopoes = db.ExpedicaoCopoes.Include(e => e.Cliente).Include(e => e.Marcanti).OrderByDescending(e=>e.Vencimento);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			return View(expedicaoCopoes.ToPagedList(pageNumber, pageSize));
        }

        // GET: ExpedicaoCopos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedicaoCopo expedicaoCopo = db.ExpedicaoCopoes.Find(id);
			var busca = from venda in db.Vendas
						where venda.ExpedicaoCopoId == id
						select venda;
			expedicaoCopo.Vendas = busca.Include(o=>o.EspecificacaoCopo).Include(o=>o.EspecificacaoCopo.Cor).ToList();

			if (expedicaoCopo == null)
            {
                return HttpNotFound();
            }
            return View(expedicaoCopo);
        }
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoCopos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
            ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa");
            ViewBag.Copos = db.EspecificacaoCopoes.ToList();
            ViewBag.Copo = db.EspecificacaoCopoes.Select(p => new SelectListItem
            {
                Text = p.OrdemProducaoCopo.CodigoIdentificador+ ", Cor: "+p.Cor.NomeCor + ", Quantidade: " + p.UniProd + ", Lote Master: "+ p.LoteMaster,
                Value = p.EspecificacaoCopoId.ToString()
            });
            return View();
        }

        // POST: ExpedicaoCopos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExpedicaoId,ValorTotal,Vencimento,ClienteId,MarcantiId")] ExpedicaoCopo expedicaoCopo)
        {
            var a = Request.Form["Copo"];
           
			var c = Request.Form["Venda.Quantidade"];
			expedicaoCopo.MarcantiId = 1;
            if (ModelState.IsValid)
            {
                if(a!= null)
                {
                    var copo = a.Split(',').Select(Int32.Parse).ToList();
					List<string> b = new List<string>(Request.Form.GetValues("Venda.ValorUnitario"));
					var val = b.Select(Double.Parse).ToList();
					var quant = c.Split(',').Select(Int32.Parse).ToList();
					List<Venda> vendas = new List<Venda>();
                    for (var x=0; x<copo.Count();x++)
                    {
                        Venda v1 = new Venda();
                        v1.ValorUnitario = val[x];
						v1.ExpedicaoCopoId = expedicaoCopo.ExpedicaoId;
						v1.EspecificacaoCopoId =  copo[x];
						v1.Quantidade = quant[x];
						var teste = db.EspecificacaoCopoes.Find(v1.EspecificacaoCopoId);
						v1.CalcSubtotal(val[x], quant[x]);
						expedicaoCopo.CalcValorTotal(v1.Subtotal);
                        vendas.Add(v1);
                    }
                    expedicaoCopo.Vendas= vendas;
                }
                db.ExpedicaoCopoes.Add(expedicaoCopo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoCopo.ClienteId);
            ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoCopo.MarcantiId);
            return View(expedicaoCopo);
        }

        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoCopos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedicaoCopo expedicaoCopo = db.ExpedicaoCopoes.Find(id);
            if (expedicaoCopo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoCopo.ClienteId);
            ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoCopo.MarcantiId);
            return View(expedicaoCopo);
        }

        // POST: ExpedicaoCopos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExpedicaoId,ValorTotal,Vencimento,ClienteId,MarcantiId")] ExpedicaoCopo expedicaoCopo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(expedicaoCopo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", expedicaoCopo.ClienteId);
            ViewBag.MarcantiId = new SelectList(db.Marcantis, "MarcantiId", "NomeEmpresa", expedicaoCopo.MarcantiId);
            return View(expedicaoCopo);
        }
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: ExpedicaoCopos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExpedicaoCopo expedicaoCopo = db.ExpedicaoCopoes.Find(id);
            db.Entry(expedicaoCopo).Reference(u => u.Cliente).Load();
            if (expedicaoCopo == null)
            {
                return HttpNotFound();
            }
            return View(expedicaoCopo);
        }

        // POST: ExpedicaoCopos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExpedicaoCopo expedicaoCopo = db.ExpedicaoCopoes.Find(id);
            db.ExpedicaoCopoes.Remove(expedicaoCopo);
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
