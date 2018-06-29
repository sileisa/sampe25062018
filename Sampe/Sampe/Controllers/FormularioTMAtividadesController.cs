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
    [Authorize(Roles = "Acesso Total, Acesso Administrador,Acesso Supervisor,Acesso Produção")]
    public class FormularioTMAtividadesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: FormularioTMAtividades
        public ActionResult Index()
        {
            var formularioTMAtividade = db.FormularioTMAtividade.Include(f => f.AtividadeTM).Include(f => f.FormularioTrocaMolde);
            return View(formularioTMAtividade.ToList());
        }

        // GET: FormularioTMAtividades/Details/5
        public ActionResult Details(int? id)
        {
            FormularioTMAtividade formularioTMAtividade = db.FormularioTMAtividade.Find(id);
            AtividadeTM atv = db.AtividadeTMs.Find(formularioTMAtividade.AtividadeTMId);
            FormularioTrocaMolde formTm = db.FormularioTrocaMoldes.Find(formularioTMAtividade.FormularioTrocaMoldeId);

            var atvs = db.FormularioTrocaMoldes.Include(f => f.AtividadesTM);

            //db.Entry(formularioTMAtividade.FormularioTrocaMolde).Reference(f => f.Molde).Load();
            db.Entry(formularioTMAtividade.FormularioTrocaMolde).Reference(f => f.Maquina).Load();
            db.Entry(formularioTMAtividade.FormularioTrocaMolde).Reference(f => f.Usuario).Load();
            db.Entry(formularioTMAtividade.FormularioTrocaMolde).Collection(f => f.AtividadesTM).Load();
            var y = atvs.ToList();
            //formularioTMAtividade.FormularioTrocaMolde.AtividadesTM = y;
            //db.Entry(formTm).Collection(f => f.AtividadesTM);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (formularioTMAtividade == null)
            {
                return HttpNotFound();
            }
            return View(formularioTMAtividade);
        }

        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        // GET: FormularioTMAtividades/Create
        public ActionResult Create()
        {
            ViewBag.MaquinaId = db.Maquinas.ToList();
            ViewBag.MoldeId = db.Moldes.Select(p => new SelectListItem
            {
                Text = p.NomeMolde + " (" + p.Cavidade + ")",
                Value = p.MoldeId.ToString()
            });
            //ViewBag.MoldeId = db.Moldes.ToList() ; 
            ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor");
            ViewBag.Supervisor = db.Usuarios.Where(u => u.Hierarquia == "Acesso Supervisor");
			//ViewBag.AtividadeTMId = new SelectList(db.AtividadeTMs, "AtividadeTMId", "NomeAtvTm");
			ViewBag.AtividadeTMId = db.AtividadeTMs.ToList();
			ViewBag.FormularioTrocaMoldeId = new SelectList(db.FormularioTrocaMoldes, "FormularioTrocaMoldeId", "DtRetirada");
            return View();
        }

        // POST: FormularioTMAtividades/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormularioTMAtividadeId,FormularioTrocaMoldeId,AtividadeTMId,StatusTM,FormularioTrocaMolde")] FormularioTMAtividade formularioTMAtividade, ICollection<int> MoldeId, [Bind(Include = "MaquinaId")] Maquina m1, [Bind(Include = "UsuarioId")] Usuario ex, string Supervisor)
        {

            //if (ModelState.IsValid)
            //{
            //var ex=Request.Form["Executor"]
            if (m1.MaquinaId == 0)
            {
                ViewBag.Maquina = "Preencha este campo!";
            }
            else if (MoldeId.Count < 2)
            {
                ViewBag.Molde = "Preencha este campo!";
            }
            else if (ex.UsuarioId == 0)
            {
                ViewBag.Exe = "Preencha este campo!";
            }
            else if (Supervisor == "")
            {
                ViewBag.Sup = "Preencha este campo!";
            }
            else
            {
                var lstTags = Request.Form["chkTags"];
                if (!string.IsNullOrEmpty(lstTags))
                {
                    FormularioTrocaMolde form = new FormularioTrocaMolde();
                    form = formularioTMAtividade.FormularioTrocaMolde;
                    formularioTMAtividade.StatusTM = false;

                    //formularioTMAtividade.FormularioTrocaMolde.MoldeId = m2.MoldeId;
                    List<FormularioMolde> molde = new List<FormularioMolde>();
                    foreach (var x in MoldeId)
                    {
                        FormularioMolde m = new FormularioMolde();
                        var md = db.Moldes.Find(x);
                        m.Molde = md;
                        molde.Add(m);
                    }

                    formularioTMAtividade.FormularioTrocaMolde.FormularioMoldes = molde;
                    formularioTMAtividade.FormularioTrocaMolde.MaquinaId = m1.MaquinaId;
                    formularioTMAtividade.FormularioTrocaMolde.UsuarioId = ex.UsuarioId;
                    formularioTMAtividade.FormularioTrocaMolde.Supervisor = Supervisor;
                    int[] splTags = lstTags.Split(',').Select(Int32.Parse).ToArray();

                    foreach (var idform in splTags)
                    {
                        formularioTMAtividade.AtividadeTMId = idform;
                        db.FormularioTMAtividade.Add(formularioTMAtividade);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "FormularioTrocaMoldes");
                }
            }
            //}
            ViewBag.MaquinaId = db.Maquinas.ToList();
            //ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioTMAtividade.FormularioTrocaMolde.MaquinaId);
            //ViewBag.MoldeId = new SelectList(db.Moldes, "MoldeId", "NomeMolde", formularioTMAtividade.FormularioTrocaMolde.MoldeId);
            //ViewBag.UsuarioId = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario", formularioTMAtividade.FormularioTrocaMolde.UsuarioId);


            ViewBag.MoldeId = db.Moldes.Select(p => new SelectListItem
            {
                Text = p.NomeMolde + " (" + p.Cavidade + ")",
                Value = p.MoldeId.ToString()
            });
            ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor").ToList();
            ViewBag.Supervisor = db.Usuarios.Where(u => u.Hierarquia == "Acesso Supervisor");
            ViewBag.AtividadeTMId = new SelectList(db.AtividadeTMs, "AtividadeTMId", "NomeAtvTm");
            ViewBag.FormularioTrocaMoldeId = new SelectList(db.FormularioTrocaMoldes, "FormularioTrocaMoldeId", "DtRetirada");
            return View(formularioTMAtividade);
        }

        // GET: FormularioTMAtividades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioTMAtividade formularioTMAtividade = db.FormularioTMAtividade.Find(id);
            if (formularioTMAtividade == null)
            {
                return HttpNotFound();
            }
            ViewBag.AtividadeTMId = new SelectList(db.AtividadeTMs, "AtividadeTMId", "NomeAtvTm", formularioTMAtividade.AtividadeTMId);
            ViewBag.FormularioTrocaMoldeId = new SelectList(db.FormularioTrocaMoldes, "FormularioTrocaMoldeId", "DtRetirada", formularioTMAtividade.FormularioTrocaMoldeId);
            return View(formularioTMAtividade);
        }

        // POST: FormularioTMAtividades/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioTMAtividadeId,FormularioTrocaMoldeId,AtividadeTMId,StatusTM")] FormularioTMAtividade formularioTMAtividade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(formularioTMAtividade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AtividadeTMId = new SelectList(db.AtividadeTMs, "AtividadeTMId", "NomeAtvTm", formularioTMAtividade.AtividadeTMId);
            ViewBag.FormularioTrocaMoldeId = new SelectList(db.FormularioTrocaMoldes, "FormularioTrocaMoldeId", "DtRetirada", formularioTMAtividade.FormularioTrocaMoldeId);
            return View(formularioTMAtividade);
        }

        // GET: FormularioTMAtividades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioTMAtividade formularioTMAtividade = db.FormularioTMAtividade.Find(id);
            if (formularioTMAtividade == null)
            {
                return HttpNotFound();
            }
            return View(formularioTMAtividade);
        }

        // POST: FormularioTMAtividades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioTMAtividade formularioTMAtividade = db.FormularioTMAtividade.Find(id);
            db.FormularioTMAtividade.Remove(formularioTMAtividade);
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
