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
    public class FormularioOSAtividadesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: FormularioOSAtividades
        public ActionResult Index()
        {
            var formularioOSAtividade = db.FormularioOSAtividade.Include(f => f.AtividadeOS).Include(f => f.FormularioOrdemServico);
            return View(formularioOSAtividade.ToList());
        }

        // GET: FormularioOSAtividades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOSAtividade formularioOSAtividade = db.FormularioOSAtividade.Find(id);
            if (formularioOSAtividade == null)
            {
                return HttpNotFound();
            }
            return View(formularioOSAtividade);
        }

        // GET: FormularioOSAtividades/Create
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        public ActionResult Create()
        {
            ViewBag.MaquinaId = db.Maquinas.ToList();
            ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor");
            ViewBag.Supervisor = db.Usuarios.Where(u => u.Hierarquia == "Acesso Supervisor");
            ViewBag.AtividadeOSId = new MultiSelectList(db.AtividadeOS, "AtividadeOSId", "NomeAtvOs");
            ViewBag.FormularioOrdemServicoId = new SelectList(db.FormularioOrdemServicoes, "FormularioOrdemServicoId", "TipoManutencao");
            return View();
        }

        // POST: FormularioOSAtividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormularioOSAtividadeId,FormularioOrdemServicoId,AtividadeOSId,AtividadesOS,StatusOS,FormularioOrdemServico,TipoManutencao")] FormularioOSAtividade formularioOSAtividade, [Bind(Include = "MaquinaId")] Maquina m1, [Bind(Include = "UsuarioId")] Usuario u1, [Bind(Include = "AtividadeOSId, NomeAtvOs")] AtividadeOS atividadeOS, int Executor, string Supervisor, string TipoManutencao)
        {

            //if (ModelState.IsValid)
            //{
            var lstTags = Request.Form["chkTags"];
            if (!string.IsNullOrEmpty(lstTags))
            {
                FormularioOrdemServico form = new FormularioOrdemServico();
                form = formularioOSAtividade.FormularioOrdemServico;
                formularioOSAtividade.StatusOS = false;
                formularioOSAtividade.FormularioOrdemServico.MaquinaId = m1.MaquinaId;
                formularioOSAtividade.FormularioOrdemServico.UsuarioId = Executor;
                formularioOSAtividade.FormularioOrdemServico.Supervisor = Supervisor;
                formularioOSAtividade.FormularioOrdemServico.TipoManutencao = TipoManutencao;
                int[] splTags = lstTags.Split(',').Select(Int32.Parse).ToArray();
                foreach (var idform in splTags)
                {
                    formularioOSAtividade.AtividadeOSId = idform;
                    db.FormularioOSAtividade.Add(formularioOSAtividade);
                    db.SaveChanges();                  
                }
                return RedirectToAction("Index", "FormularioOrdemServicoes");
            }                                                                 
            //}
            return View(formularioOSAtividade);
        }

        // GET: FormularioOSAtividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOSAtividade formularioOSAtividade = db.FormularioOSAtividade.Find(id);
            if (formularioOSAtividade == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtividadeOSId = new SelectList(db.AtividadeOS, "AtividadeOSId", "NomeAtvOs", formularioOSAtividade.AtividadeOSId);
            ViewBag.FormularioOrdemServicoId = new SelectList(db.FormularioOrdemServicoes, "FormularioOrdemServicoId", "TipoManutencao", formularioOSAtividade.FormularioOrdemServicoId);
            return View(formularioOSAtividade);
        }

        // POST: FormularioOSAtividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioOSAtividadeId,FormularioOrdemServicoId,AtividadeOSId,StatusOS")] FormularioOSAtividade formularioOSAtividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formularioOSAtividade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtividadeOSId = new SelectList(db.AtividadeOS, "AtividadeOSId", "NomeAtvOs", formularioOSAtividade.AtividadeOSId);
            ViewBag.FormularioOrdemServicoId = new SelectList(db.FormularioOrdemServicoes, "FormularioOrdemServicoId", "TipoManutencao", formularioOSAtividade.FormularioOrdemServicoId);
            return View(formularioOSAtividade);
        }

        // GET: FormularioOSAtividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioOSAtividade formularioOSAtividade = db.FormularioOSAtividade.Find(id);
            if (formularioOSAtividade == null)
            {
                return HttpNotFound();
            }
            return View(formularioOSAtividade);
        }

        // POST: FormularioOSAtividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioOSAtividade formularioOSAtividade = db.FormularioOSAtividade.Find(id);
            db.FormularioOSAtividade.Remove(formularioOSAtividade);
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
