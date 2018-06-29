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
    public class ClientesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Clientes
        public ActionResult Index()
        {
            return View(db.Clientes.ToList().OrderBy(u => u.NomeCliente));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "ClienteId,NomeCliente,Cnpj,Cep,Uf,Cidade,Rua,Bairro,Numero")] Cliente cliente)
        {
            var clientes = db.Clientes.ToList();
            var x = db.Clientes.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {                
                if (cont > 0) { 
                if (cliente.Cnpj == clientes[i].Cnpj)
                {

                    ViewBag.Error = "Cliente já existente";
                    break;
                    }
                    else if (cont == x)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Clientes.Add(cliente);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else {
                    if (ModelState.IsValid)
                    {
                        db.Clientes.Add(cliente);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                cont++;
            }
            return View(db.Clientes.ToList());
        }
        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,NomeCliente")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,NomeCliente,Cnpj,Cep,Uf,Cidade,Rua,Bairro,Numero")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
			var busca = db.Especificacaos.Where(o => o.ClienteId == id).ToList();
			var busca2 = db.ExpedicaoCopoes.Where(o => o.ClienteId == id).ToList();
			var busca3 = db.ExpedicaoKits.Where(o => o.ClienteId == id).ToList();		
			if ((busca.Count() > 0) || (busca2.Count() > 0) || (busca3.Count() > 0))
			{
				ViewBag.Error = "Não é possível deletar este Cliente, pois está sendo utilizado em outras partes do sistema.";
			}
			else { 
			db.Clientes.Remove(cliente);
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
