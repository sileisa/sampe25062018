using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sampe.Models;
using PagedList;

namespace Sampe.Controllers
{
	public class OrdemProducaoKitsController : Controller
	{
		private SampeContext db = new SampeContext();
		public int OPnoMes = 0;

		// GET: OrdemProducaoKits
		public ActionResult Index(int? page)
		{
			var ordemProducaoKits = db.OrdemProducaoKits.Include(o => o.Usuario).OrderByDescending(o=>o.Data);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			return View(ordemProducaoKits.ToPagedList(pageNumber, pageSize));
			//return View(ordemProducaoKits.ToList());
		}

		// GET: OrdemProducaoKits/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(id);
			db.Entry(ordemProducaoKit).Reference(o => o.Usuario).Load();
			if (ordemProducaoKit == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoKit);
		}

		// GET: OrdemProducaoKits/Create
		public ActionResult Create()
		{
			ViewBag.Operdor = new SelectList( db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
			var x = db.OrdemProducaoPecas.Where(f => f.Expectativa.Produto.Contains("ANEL")&& f.Status==true).ToList();
			var y = db.OrdemProducaoPecas.Where(f => f.Expectativa.Produto.Contains("CHAPÉU") && f.Status == true).ToList();
			var z = db.OrdemProducaoPecas.Where(f => f.Expectativa.Produto.Contains("CAPA") && f.Status == true).ToList();
			ViewBag.Produto = db.OrdemProducaoPecas.ToList();
			ViewBag.Anel = new SelectList(x, "CodigoIdentificador", "CodigoIdentificador");
			ViewBag.Chapeu = new SelectList(y, "CodigoIdentificador", "CodigoIdentificador"); ;
			ViewBag.Capa = new SelectList(z, "CodigoIdentificador", "CodigoIdentificador"); ;
			ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
			ViewBag.Clientes = db.Clientes.ToList();
			ViewBag.CorPecaId = db.CorPecas.ToList();
			return View();
		}

		// POST: OrdemProducaoKits/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CodigoIdentificadorKit,Data,ProdIncio,ProdFim,TotalProduzido,NivelamentoBalanca,Especificacoes,Operdor,ClienteId")] OrdemProducaoKit ordemProducaoKit, String Capa, String Chapeu, String Anel, bool Status)
		{
                      
        
			var a = Request.Form["TipoKit"];
			var b = Request.Form["CorKit"];
            
			var c = Request.Form["Especificacao.QuantProduzido"];
			var d = Request.Form["ClienteId"];
			
			var e = Request.Form["ParadaKit.HoraParada"];
			var f = Request.Form["ParadaKit.HoraRetorno"];
			var g = Request.Form["Motivo"];
			var h = Request.Form["ParadaKit.Observacoes"];
            if (b == "")
            {
                ViewBag.C = "Preencha este campo!";
            }
            else
            {
               if (ModelState.IsValid)
			{

				List<KitPeca> kits = new List<KitPeca>();
				KitPeca k = new KitPeca();
				KitPeca k2 = new KitPeca();
				KitPeca k3 = new KitPeca();
				var p1 = db.OrdemProducaoPecas.Find(Capa);
				var p2 = db.OrdemProducaoPecas.Find(Chapeu);
				var p3 = db.OrdemProducaoPecas.Find(Anel);

				k.OrdemProducaoPeca = p1;
				kits.Add(k);
				k2.OrdemProducaoPeca = p2;
				kits.Add(k2);
				k3.OrdemProducaoPeca = p3;
				kits.Add(k3);
				ordemProducaoKit.KitPecas = kits;

				var qtdOp = 0;
				var mesAnterior = 0;
				if (db.OrdemProducaoKits.Count() > 0)
				{
					qtdOp = db.OrdemProducaoKits.ToList().Last().OPnoMes;
					mesAnterior = db.OrdemProducaoKits.ToList().Last().Data.Month;
				}
				else
				{
					qtdOp = 0;
					mesAnterior = 0;
				}
				if (mesAnterior != ordemProducaoKit.Data.Month)
				{
					ordemProducaoKit.OPnoMes = 1;
				}
				else
					ordemProducaoKit.OPnoMes = qtdOp + 1;
				//ordemProducaoKit.Expectativa = db.Expectativas.Find(ordemProducaoKit.ExpectativaId);
				//db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
				ordemProducaoKit.GerarCodigo();

				if (a != null)
				{
					var tipo = a.Split(',');
					var cor = b.Split(',').Select(Int32.Parse).ToArray(); ;
					var quant = c.Split(',').Select(Int32.Parse).ToArray();
					var cliente = d.Split(',').Select(Int32.Parse).ToArray();
					List<string> parafuso1 = new List<string>(Request.Form.GetValues("Especificacao.Parafuso"));
					parafuso1 = ordemProducaoKit.RemoveExtraFalseFromCheckbox(parafuso1);
					bool[] parafuso = parafuso1.Select(Boolean.Parse).ToArray();
					List<Especificacao> esp = new List<Especificacao>();
					for ( int i = 0; i < tipo.Count(); i++)
					{
						Especificacao e1 = new Especificacao();
						e1.TipoKit = tipo[i];
						e1.CorPecaId = cor[i];
						e1.Parafuso = parafuso[i];
						e1.QuantProduzido = quant[i];
						e1.ClienteId = cliente[i];
						e1.CodigoIdentificadorKit = ordemProducaoKit.CodigoIdentificadorKit;
						ordemProducaoKit.calculaProdTotal(quant[i]);
						esp.Add(e1);
					}
					ordemProducaoKit.Especificacoes = esp;
				}

				if (e != null)
				{
					var hrP = e.Split(',');
					var hrR = f.Split(',');
					var mot = g.Split(',');
					var obs = h.Split(',');
					List<ParadaKit> parakit = new List<ParadaKit>();
					for (int j = 0; j < hrP.Count(); j++)
					{
						ParadaKit p = new ParadaKit();
						p.HoraParada = hrP[j];
						p.HoraRetorno = hrR[j];
						p.Motivo = mot[j];
						p.Observacoes = obs[j];
						p.CodigoIdentificadorKit = ordemProducaoKit.CodigoIdentificadorKit;
						parakit.Add(p);
					}
					ordemProducaoKit.ParadasKit = parakit;
				}
				ordemProducaoKit.Status = Status;
				db.OrdemProducaoKits.Add(ordemProducaoKit);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
            }
            ViewBag.Operdor = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
            var x = db.OrdemProducaoPecas.Where(u => u.Expectativa.Produto.Contains("ANEL")).ToList();
            var y = db.OrdemProducaoPecas.Where(u => u.Expectativa.Produto.Contains("CHAPÉU")).ToList();
            var z = db.OrdemProducaoPecas.Where(u => u.Expectativa.Produto.Contains("CAPA")).ToList();
            ViewBag.Produto = db.OrdemProducaoPecas.ToList();
            ViewBag.Anel = new SelectList(x, "CodigoIdentificador", "CodigoIdentificador");
            ViewBag.Chapeu = new SelectList(y, "CodigoIdentificador", "CodigoIdentificador"); ;
            ViewBag.Capa = new SelectList(z, "CodigoIdentificador", "CodigoIdentificador"); ;
            ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
            ViewBag.Clientes = db.Clientes.ToList();
            ViewBag.CorPecaId = db.CorPecas.ToList();
            return View(ordemProducaoKit);
		}

		// GET: OrdemProducaoKits/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(id);
			ViewBag.Operdor = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor");
			var busca = db.Especificacaos.Where(o => o.CodigoIdentificadorKit == id).Include(o=>o.CorPeca).ToList();
			ordemProducaoKit.Especificacoes = busca;
			var busca2 = db.ParadaKits.Where(o => o.CodigoIdentificadorKit == id).ToList();
			ordemProducaoKit.ParadasKit = busca2;
			var busca3 = from Kit in db.OrdemProducaoKits
						 where Kit.CodigoIdentificadorKit == ordemProducaoKit.CodigoIdentificadorKit
						 join kitPecas in db.KitPecas
						 on Kit.CodigoIdentificadorKit equals kitPecas.CodigoIdentificadorKit
						 join peca in db.OrdemProducaoPecas
						 on kitPecas.CodigoIdentificador equals peca.CodigoIdentificador
						 select kitPecas.OrdemProducaoPeca;

			ordemProducaoKit.OrdemProducaoPecas = busca3.ToList();
			ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente");
			ViewBag.Clientes = db.Clientes.ToList();

			if (ordemProducaoKit == null)
			{
				return HttpNotFound();
			}
			ViewBag.CorPecaId = db.CorPecas.ToList();
			//ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", ordemProducaoKit.ClienteId);
			return View(ordemProducaoKit);
		}

		// POST: OrdemProducaoKits/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CodigoIdentificadorKit,Data,ProdIncio,ProdFim,TotalProduzido,NivelamentoBalanca,Obs,OPnoMes,Operdor,ClienteId")] OrdemProducaoKit ordemProducaoKit, bool Status)
		{
			var a = Request.Form["TipoKit"];
			var b = Request.Form["CorKit"];
			var c = Request.Form["Especificacao.QuantProduzido"];
			var d = Request.Form["ClienteId"];
			
			var e = Request.Form["ParadaKit.HoraParada"];
			var f = Request.Form["ParadaKit.HoraRetorno"];
			var g = Request.Form["Motivo"];
			var h = Request.Form["ParadaKit.Observacoes"];

			if (a != null)
			{
				var tipo = a.Split(',');
				var cor = b.Split(',').Select(Int32.Parse).ToArray();
				var quant = c.Split(',').Select(Int32.Parse).ToArray();
				var cliente = d.Split(',').Select(Int32.Parse).ToArray();
				List<string> parafuso1 = new List<string>(Request.Form.GetValues("Especificacao.Parafuso"));

				parafuso1 = ordemProducaoKit.RemoveExtraFalseFromCheckbox(parafuso1);
				bool[] parafuso = parafuso1.Select(Boolean.Parse).ToArray();
				for (int x = 0; x < tipo.Count(); x++)
				{
					Especificacao e1 = new Especificacao();
					e1.TipoKit = tipo[x];
					e1.CorPecaId = cor[x];
					e1.Parafuso = parafuso[x];
					e1.QuantProduzido = quant[x];
					e1.ClienteId = cliente[x];
					e1.CodigoIdentificadorKit = ordemProducaoKit.CodigoIdentificadorKit;
					ordemProducaoKit.calculaProdTotal(quant[x]);
					db.Especificacaos.Add(e1);
					db.SaveChanges();
				}
			}

			if (e != null)
			{
				var hrP = e.Split(',');
				var hrR = f.Split(',');
				var mot = g.Split(',');
				var obs = h.Split(',');
				for (int y = 0; y < hrP.Count(); y++)
				{
					ParadaKit p = new ParadaKit();
					p.HoraParada = hrP[y];
					p.HoraRetorno = hrR[y];
					p.Motivo = mot[y];
					p.Observacoes = obs[y];
					p.CodigoIdentificadorKit = ordemProducaoKit.CodigoIdentificadorKit;
					db.ParadaKits.Add(p);
					db.SaveChanges();
				}
			}
			if (ModelState.IsValid)
			{
				ordemProducaoKit.Status = Status;
				db.Entry(ordemProducaoKit).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			//ViewBag.ClienteId = new SelectList(db.Clientes, "ClienteId", "NomeCliente", ordemProducaoKit.ClienteId);
			return View(ordemProducaoKit);
		}

		// GET: OrdemProducaoKits/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(id);
			db.Entry(ordemProducaoKit).Reference(o => o.Usuario).Load();
			if (ordemProducaoKit == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoKit);
		}

		// POST: OrdemProducaoKits/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			OrdemProducaoKit ordemProducaoKit = db.OrdemProducaoKits.Find(id);
			
			var busca = db.Especificacaos.Where(o => o.CodigoIdentificadorKit == id).Include(o => o.CorPeca).ToList();
			ordemProducaoKit.Especificacoes = busca;
			var busca2 = db.ParadaKits.Where(o => o.CodigoIdentificadorKit == id).ToList();
			ordemProducaoKit.ParadasKit = busca2;
			var busca3 = db.KitPecas.Where(o => o.CodigoIdentificadorKit == id).ToList();
			ordemProducaoKit.KitPecas = busca3.ToList();
			db.OrdemProducaoKits.Remove(ordemProducaoKit);
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
