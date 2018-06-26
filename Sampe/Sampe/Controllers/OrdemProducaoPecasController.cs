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
	public class OrdemProducaoPecasController : Controller
	{
		private SampeContext db = new SampeContext();
		public int OPnoMes = 0;
		// GET: OrdemProducaoPecas

		public ActionResult Index(int? page)
		{
			var ordemProducaoPecas = db.OrdemProducaoPecas.Include(o => o.Expectativa).Include(o => o.Maquina).Include(o => o.CorPeca).OrderByDescending(o => o.Data);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			return View(ordemProducaoPecas.ToPagedList(pageNumber, pageSize));
			//return View(ordemProducaoPecas.ToList());
		}

		// GET: OrdemProducaoPecas/Details/5
		public ActionResult Details(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(id);
			ordemProducaoPeca.Expectativa = db.Expectativas.Find(ordemProducaoPeca.ExpectativaId);
			db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
			db.Entry(ordemProducaoPeca).Reference(f => f.Maquina).Load();
			if (ordemProducaoPeca == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoPeca);
		}

		// GET: OrdemProducaoPecas/Create
		public ActionResult Create()
		{
			ViewBag.MaquinaId = new SelectList(db.Maquinas.ToList(), "MaquinaId", "NomeMaquina");
			ViewBag.Assinatura = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
			ViewBag.ExpectativaId = new SelectList(db.Expectativas, "ExpectativaId", "Produto");
			ViewBag.CorPecaId = new SelectList(db.CorPecas.ToList(), "CorPecaId", "NomeCorPeca");
			return View();
		}

		// POST: OrdemProducaoPecas/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CodigoIdentificador,ExpectativaId,Data,MateriaPrima," +
			"MPLote,MPConsumo,ProdIncio,ProdFim,MaquinaId,Produto,CorPecaId,MasterLote,Fornecedor,TempAgua," +
			"NivelOleo,Galho,OffSpec,RefugoKg,UnidadesProduzidas,ContadorInicial,ContadorFinal")] OrdemProducaoPeca ordemProducaoPeca, bool Status)
		{
           
            if (ordemProducaoPeca.MateriaPrima == null)
            {
                ViewBag.MateriaPrima = "Preencha este campo!";
            }
            
            else { 
			
			var a = Request.Form["Parada.HoraParada"];
			var b = Request.Form["Parada.HoraRetorno"];
			var c = Request.Form["Motivo"];
			var d = Request.Form["Parada.Observacoes"];
			var e = Request.Form["Hora"];
			var h = Request.Form["Assinatura"];
			ordemProducaoPeca.Status = Status;
			var maquina = db.Maquinas.Find(ordemProducaoPeca.MaquinaId);
			ordemProducaoPeca.Maquina = maquina;
			var qtdOp = 0;
			var mesAnterior = 0;
			if (db.OrdemProducaoPecas.Count() > 0)
			{
				qtdOp = db.OrdemProducaoPecas.ToList().Last().OPnoMes;
				mesAnterior = db.OrdemProducaoPecas.ToList().Last().Data.Month;
			}
			else
			{
				qtdOp = 0;
				mesAnterior = 0;
			}
			if (mesAnterior != ordemProducaoPeca.Data.Month)
			{
				ordemProducaoPeca.OPnoMes = 1;
			}
			else
				ordemProducaoPeca.OPnoMes = qtdOp + 1;
			ordemProducaoPeca.Expectativa = db.Expectativas.Find(ordemProducaoPeca.ExpectativaId);
			//db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
			

			if (a != null)
			{
				var hrP = a.Split(',');
				var hrR = b.Split(',');
				var mot = c.Split(',');
				var obs = d.Split(',');
				List<Parada> parada = new List<Parada>();
				for (int x = 0; x < hrP.Count(); x++)
				{
					Parada p = new Parada();
					p.HoraParada = hrP[x];
					p.HoraRetorno = hrR[x];
					p.Motivo = mot[x];
					p.Observacoes = obs[x];
					p.OrdemProducaoPecaId = ordemProducaoPeca.CodigoIdentificador;
					parada.Add(p);
				}
				ordemProducaoPeca.Paradas = parada;
			}

			if (e != null)
			{
				var hora = e.Split(',');
				List<string> f = new List<string>(Request.Form.GetValues("ControleDeQualidade.Ciclo"));
				List<string> g = new List<string>(Request.Form.GetValues("ControleDeQualidade.PesoDaPeca"));
				var ciclo = f.Select(Double.Parse).ToList();
				var pesoPeca = g.Select(Double.Parse).ToList();
				List<string> peso1 = new List<string>(Request.Form.GetValues("ControleDeQualidade.Peso"));
				List<string> cor1 = new List<string>(Request.Form.GetValues("ControleDeQualidade.Cor"));
				List<string> dim = new List<string>(Request.Form.GetValues("ControleDeQualidade.Dimensao"));

				peso1 = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(peso1);
				bool[] peso = peso1.Select(Boolean.Parse).ToArray();

				cor1 = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(cor1);
				bool[] cor = cor1.Select(Boolean.Parse).ToArray();

				dim = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(dim);
				bool[] dimensao = dim.Select(Boolean.Parse).ToArray();

				var assinatura = h.Split(',').Select(Int32.Parse).ToArray();
				List<ControleDeQualidade> ctrlQual = new List<ControleDeQualidade>();
				for (int x = 0; x < hora.Count(); x++)
				{
					ControleDeQualidade c1 = new ControleDeQualidade();
					c1.Hora = hora[x];
					c1.Ciclo = ciclo[x];
					c1.PesoDaPeca = pesoPeca[x];
					c1.Peso = peso[x];
					c1.Cor = cor[x];
					c1.Dimensao = dimensao[x];
					c1.Assinatura = assinatura[x];
					c1.OrdemProducaoPecaId = ordemProducaoPeca.CodigoIdentificador;
					ctrlQual.Add(c1);
					//ordemProducaoPeca.ControleDeQualidade.ValidaInspecao();
				}
				ordemProducaoPeca.ControlesDeQualidade = ctrlQual;
			}

			if (ModelState.IsValid)
			{
				ordemProducaoPeca.ConfiguracaoInicial();
				db.OrdemProducaoPecas.Add(ordemProducaoPeca);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
            }
            //}
            ViewBag.MaquinaId = new SelectList(db.Maquinas.ToList(), "MaquinaId", "NomeMaquina");
            ViewBag.Assinatura = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
            ViewBag.CorPecaId = new SelectList(db.CorPecas, "CorPecaId", "NomeCorPeca", ordemProducaoPeca.CorPecaId);
            ViewBag.ExpectativaId = new SelectList(db.Expectativas, "ExpectativaId", "Produto", ordemProducaoPeca.ExpectativaId);
            return View(ordemProducaoPeca);
		}

		// GET: OrdemProducaoPecas/Edit/5
		public ActionResult Edit(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ViewBag.CorPecaId = db.CorPecas;
			ViewBag.MaquinaId = db.Maquinas.ToList();
			ViewBag.Assinatura = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
			ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor").ToArray();
			ViewBag.ExpectativaId = new SelectList(db.Expectativas, "ExpectativaId", "Produto");
			OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(id);
			var busca = db.Paradas.Where(o => o.OrdemProducaoPecaId == id).ToList();
			ordemProducaoPeca.Paradas = busca;
			var busca2 = db.ControleDeQualidades.Where(o => o.OrdemProducaoPecaId == id).Include(o => o.Usuario).ToList();
			ordemProducaoPeca.ControlesDeQualidade = busca2;
			if (ordemProducaoPeca == null)
			{
				return HttpNotFound();
			}
			ViewBag.ExpectativaId = new SelectList(db.Expectativas, "ExpectativaId", "Produto", ordemProducaoPeca.ExpectativaId);
			return View(ordemProducaoPeca);
		}

		// POST: OrdemProducaoPecas/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CodigoIdentificador,ExpectativaId,Data,MateriaPrima,MPLote,MPConsumo,ProdIncio,ProdFim,MaquinaId,Produto,CorPecaId,MasterLote,Fornecedor,TempAgua,NivelOleo,Galho,OffSpec,RefugoKg,UnidadesProduzidas,ContadorInicial,ContadorFinal")] OrdemProducaoPeca ordemProducaoPeca, bool Status)
		{
			var a = Request.Form["Parada.HoraParada"];
			var b = Request.Form["Parada.HoraRetorno"];
			var c = Request.Form["Motivo"];
			var d = Request.Form["Parada.Observacoes"];

			var e = Request.Form["Hora"];

			var h = Request.Form[""];
			if (ModelState.IsValid)
			{
				if (a != null)
				{
					var hrP = a.Split(',');
					var hrR = b.Split(',');
					var mot = c.Split(',');
					var obs = d.Split(',');

					for (int x = 0; x < hrP.Count(); x++)
					{
						Parada p = new Parada();
						p.HoraParada = hrP[x];
						p.HoraRetorno = hrR[x];
						p.Motivo = mot[x];
						p.Observacoes = obs[x];
						p.OrdemProducaoPecaId = ordemProducaoPeca.CodigoIdentificador;
						db.Paradas.Add(p);
						db.SaveChanges();
					}
				}
				/*
				if (e != null)
				{
					var hora = e.Split(',');

					List<string> f = new List<string>(Request.Form.GetValues("ControleDeQualidade.Ciclo"));
					List<string> g = new List<string>(Request.Form.GetValues("ControleDeQualidade.PesoDaPeca"));
					var ciclo = f.Select(Double.Parse).ToList();
					var pesoPeca = g.Select(Double.Parse).ToList();

					List<string> peso1 = new List<string>(Request.Form.GetValues("ControleDeQualidade.Peso"));
					List<string> cor1 = new List<string>(Request.Form.GetValues("ControleDeQualidade.Cor"));
					List<string> dim = new List<string>(Request.Form.GetValues("ControleDeQualidade.Dimensao"));
					peso1 = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(peso1);
					bool[] peso = peso1.Select(Boolean.Parse).ToArray();

					cor1 = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(cor1);
					bool[] cor = cor1.Select(Boolean.Parse).ToArray();

					dim = ordemProducaoPeca.RemoveExtraFalseFromCheckbox(dim);
					bool[] dimensao = dim.Select(Boolean.Parse).ToArray();
					var assinatura = h.Split(',').Select(Int32.Parse).ToArray();
					List<ControleDeQualidade> ctrlQual = new List<ControleDeQualidade>();
					for (int x = 0; x < hora.Count(); x++)
					{
						ControleDeQualidade c1 = new ControleDeQualidade();
						c1.Hora = hora[x];
						c1.Ciclo = ciclo[x];
						c1.PesoDaPeca = pesoPeca[x];
						c1.Peso = peso[x];
						c1.Cor = cor[x];
						c1.Dimensao = dimensao[x];
						c1.Assinatura = assinatura[x];
						c1.OrdemProducaoPecaId = ordemProducaoPeca.CodigoIdentificador;
						ctrlQual.Add(c1);
						//db.ControleDeQualidades.Add(c1);
						//db.SaveChanges();
						//ordemProducaoPeca.ControleDeQualidade.ValidaInspecao();
					}
					ordemProducaoPeca.ControlesDeQualidade = ctrlQual;
				}*/
				ordemProducaoPeca.Status = Status;
				db.Entry(ordemProducaoPeca).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.ExpectativaId = new SelectList(db.Expectativas, "ExpectativaId", "Produto", ordemProducaoPeca.ExpectativaId);
			return View(ordemProducaoPeca);
		}

		// GET: OrdemProducaoPecas/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(id);
			db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
			db.Entry(ordemProducaoPeca).Reference(f => f.Maquina).Load();
			if (ordemProducaoPeca == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoPeca);
		}

		// POST: OrdemProducaoPecas/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			OrdemProducaoPeca ordemProducaoPeca = db.OrdemProducaoPecas.Find(id);
			db.Entry(ordemProducaoPeca).Reference(f => f.Expectativa).Load();
			db.Entry(ordemProducaoPeca).Reference(f => f.Maquina).Load();
			var busca = db.Paradas.Where(o => o.OrdemProducaoPecaId == id).ToList();
			ordemProducaoPeca.Paradas = busca;
			var busca2 = db.ControleDeQualidades.Where(o => o.OrdemProducaoPecaId == id).Include(o => o.Usuario).ToList();
			ordemProducaoPeca.ControlesDeQualidade = busca2;
			var busca3 = db.KitPecas.Where(o => o.CodigoIdentificador == id).ToList();
			ordemProducaoPeca.KitPecas = busca3;
			db.OrdemProducaoPecas.Remove(ordemProducaoPeca);
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
