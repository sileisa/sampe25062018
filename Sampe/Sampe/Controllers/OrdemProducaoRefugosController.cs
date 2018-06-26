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
	public class OrdemProducaoRefugosController : Controller
	{
		private SampeContext db = new SampeContext();

		// GET: OrdemProducaoRefugos
		public ActionResult Index(int? page)
		{
			var ordemProducaoRefugoes = db.OrdemProducaoRefugoes.Include(o => o.Usuario).OrderByDescending(o=>o.Data);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			return View(ordemProducaoRefugoes.ToPagedList(pageNumber, pageSize));
		}

		// GET: OrdemProducaoRefugos/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(id);
			if (ordemProducaoRefugo == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoRefugo);
		}

		// GET: OrdemProducaoRefugos/Create
		public ActionResult Create()
		{
			ViewBag.UsuarioId = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario");
			ViewBag.CorPecaId = db.CorPecas.ToList();
			return View();
		}

		// POST: OrdemProducaoRefugos/Create
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "OrdemProducaoRefugoId,Produto,Data,UsuarioId,ProdIncio,ProdFim,Obs")] OrdemProducaoRefugo ordemProducaoRefugo, bool Status)
		{
			var a = Request.Form["Material"];
			var b = Request.Form["Cor"];

			var e = Request.Form["ParadaRefugo.HoraParada"];
			var f = Request.Form["ParadaRefugo.HoraRetorno"];
			var g = Request.Form["Motivo"];
			var h = Request.Form["ParadaRefugo.Observacoes"];
            if (b == "")
            {
                ViewBag.C = "Preencha este campo!";
            }
            else
            {
                //if (ModelState.IsValid)
                //{
                var qtdOp = 0;
                var mesAnterior = 0;
                if (db.OrdemProducaoRefugoes.Count() > 0)
                {
                    qtdOp = db.OrdemProducaoRefugoes.ToList().Last().OPnoMes;
                    mesAnterior = db.OrdemProducaoRefugoes.ToList().Last().Data.Month;
                }
                else
                {
                    qtdOp = 0;
                    mesAnterior = 0;
                }
                if (mesAnterior != ordemProducaoRefugo.Data.Month)
                {
                    ordemProducaoRefugo.OPnoMes = 1;
                }
                else
                    ordemProducaoRefugo.OPnoMes = qtdOp + 1;

                ordemProducaoRefugo.GerarCodigo();
                if (a != null)
                {
                    var material = a.Split(',');
                    var cor = b.Split(',').Select(Int32.Parse).ToArray();

                    List<string> c = new List<string>(Request.Form.GetValues("EspecificacaoRefugo.Peso"));
                    var peso = c.Select(Double.Parse).ToList();

                    List<string> d = new List<string>(Request.Form.GetValues("EspecificacaoRefugo.Limpeza"));
                    d = ordemProducaoRefugo.RemoveExtraFalseFromCheckbox(d);
                    bool[] limpeza = d.Select(Boolean.Parse).ToArray();
                    List<EspecificacaoRefugo> esp = new List<EspecificacaoRefugo>();
                    for (int x = 0; x < material.Count(); x++)
                    {
                        EspecificacaoRefugo e1 = new EspecificacaoRefugo();
                        e1.Material = material[x];
                        e1.CorPecaId = cor[x];
                        e1.Peso = peso[x];
                        e1.Limpeza = limpeza[x];
                        e1.OrdemProducaoRefugoId = ordemProducaoRefugo.OrdemProducaoRefugoId;
                        esp.Add(e1);
                    }
                    ordemProducaoRefugo.EspecificacoesRefugo = esp;
                }

                if (e != null)
                {
                    var hrP = e.Split(',');
                    var hrR = f.Split(',');
                    var mot = g.Split(',');
                    var obs = h.Split(',');
                    List<ParadaRefugo> parada = new List<ParadaRefugo>();
                    for (int y = 0; y < hrP.Count(); y++)
                    {
                        ParadaRefugo p = new ParadaRefugo();
                        p.HoraParada = hrP[y];
                        p.HoraRetorno = hrR[y];
                        p.Motivo = mot[y];
                        p.Observacoes = obs[y];
                        p.OrdemProducaoRefugoId = ordemProducaoRefugo.OrdemProducaoRefugoId;
                        parada.Add(p);
                    }
                    ordemProducaoRefugo.ParadasRefugo = parada;
                }

                db.OrdemProducaoRefugoes.Add(ordemProducaoRefugo);
                db.SaveChanges();
                return RedirectToAction("Index");
                //}
            }
            ViewBag.CorPecaId = db.CorPecas.ToList();
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", ordemProducaoRefugo.UsuarioId);
			return View(ordemProducaoRefugo);
		}

		// GET: OrdemProducaoRefugos/Edit/5
		public ActionResult Edit(string id)
		{
			ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor").ToList();
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(id);
			var busca = db.ParadaRefugoes.Where(o => o.OrdemProducaoRefugoId == id).ToList();
			ordemProducaoRefugo.ParadasRefugo = busca;
			var busca2 = db.EspecificacaoRefugoes.Where(o => o.OrdemProducaoRefugoId == id).Include(o => o.CorPeca).ToList();
			ordemProducaoRefugo.EspecificacoesRefugo = busca2;
			ViewBag.CorPecaId = db.CorPecas.ToList();
			if (ordemProducaoRefugo == null)
			{
				return HttpNotFound();
			}
			ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", ordemProducaoRefugo.UsuarioId);
			return View(ordemProducaoRefugo);
		}

		// POST: OrdemProducaoRefugos/Edit/5
		// Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
		// obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "OrdemProducaoRefugoId,Produto,Data,UsuarioId,ProdIncio,ProdFim,Obs")] OrdemProducaoRefugo ordemProducaoRefugo)
		{
			var a = Request.Form["Material"];
			var b = Request.Form["Cor"];

			var e = Request.Form["ParadaRefugo.HoraParada"];
			var f = Request.Form["ParadaRefugo.HoraRetorno"];
			var g = Request.Form["Motivo"];
			var h = Request.Form["ParadaRefugo.Observacoes"];
			if (ModelState.IsValid)
			{
				
				if (a != null)
				{
					var material = a.Split(',');
					var cor = b.Split(',').Select(Int32.Parse).ToArray();

					List<string> c = new List<string>(Request.Form.GetValues("EspecificacaoRefugo.Peso"));
					var peso = c.Select(Double.Parse).ToList();

					List<string> d = new List<string>(Request.Form.GetValues("EspecificacaoRefugo.Limpeza"));
					d = ordemProducaoRefugo.RemoveExtraFalseFromCheckbox(d);
					bool[] limpeza = d.Select(Boolean.Parse).ToArray();
					for (int x = 0; x < material.Count(); x++)
					{
						EspecificacaoRefugo e1 = new EspecificacaoRefugo();
						e1.Material = material[x];
						e1.CorPecaId = cor[x];
						e1.Peso = peso[x];
						e1.Limpeza = limpeza[x];
						e1.OrdemProducaoRefugoId = ordemProducaoRefugo.OrdemProducaoRefugoId;
						db.EspecificacaoRefugoes.Add(e1);
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
						ParadaRefugo p = new ParadaRefugo();
						p.HoraParada = hrP[y];
						p.HoraRetorno = hrR[y];
						p.Motivo = mot[y];
						p.Observacoes = obs[y];
						p.OrdemProducaoRefugoId = ordemProducaoRefugo.OrdemProducaoRefugoId;
						db.ParadaRefugoes.Add(p);
						db.SaveChanges();
					}
				}


				db.Entry(ordemProducaoRefugo).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", ordemProducaoRefugo.UsuarioId);
			return View(ordemProducaoRefugo);
		}

		// GET: OrdemProducaoRefugos/Delete/5
		public ActionResult Delete(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(id);
			db.Entry(ordemProducaoRefugo).Reference(o => o.Usuario).Load();
			if (ordemProducaoRefugo == null)
			{
				return HttpNotFound();
			}
			return View(ordemProducaoRefugo);
		}

		// POST: OrdemProducaoRefugos/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(string id)
		{
			OrdemProducaoRefugo ordemProducaoRefugo = db.OrdemProducaoRefugoes.Find(id);
			var busca = db.ParadaRefugoes.Where(o => o.OrdemProducaoRefugoId == id).ToList();
			ordemProducaoRefugo.ParadasRefugo = busca;
			var busca2 = db.EspecificacaoRefugoes.Where(o => o.OrdemProducaoRefugoId == id).Include(o => o.CorPeca).ToList();
			ordemProducaoRefugo.EspecificacoesRefugo = busca2;
			db.OrdemProducaoRefugoes.Remove(ordemProducaoRefugo);
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
