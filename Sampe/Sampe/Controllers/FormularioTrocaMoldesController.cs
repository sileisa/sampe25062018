using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Rotativa;
using Rotativa.Options;
using Sampe;
using Sampe.Models;

namespace Sampe.Controllers
{
    [Authorize(Roles = "Acesso Total, Acesso Administrador,Acesso Supervisor,Acesso Produção")]
    public class FormularioTrocaMoldesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: FormularioTrocaMoldes
        public ActionResult Index(int? page)
        {
            var formularioTrocaMoldes = db.FormularioTrocaMoldes.Include(f => f.Maquina).Include(f => f.Usuario).OrderByDescending(f => f.DtRetirada);
			int pageSize = 15;
			int pageNumber = (page ?? 1);
			if (Session["Hierarquia"].ToString() == "Acesso Produção")
            {
                formularioTrocaMoldes = db.FormularioTrocaMoldes.Include(f => f.Maquina).Include(f => f.Usuario).OrderBy(f => f.DtRetirada);
                //return View(formularioTrocaMoldes.ToList().Where(f => f.Usuario.NomeUsuario == Session["NomeUsuario"].ToString()));
				return View(formularioTrocaMoldes.ToList().Where(f => f.Usuario.NomeUsuario == Session["NomeUsuario"].ToString()).ToPagedList(pageNumber, pageSize));
			}
            else
            {
                //return View(formularioTrocaMoldes.ToList());
				return View(formularioTrocaMoldes.ToPagedList(pageNumber, pageSize));
			}
        }

      
        // GET: FormularioTrocaMoldes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(id);

            var busca = from FormularioTrocaMoldes in db.FormularioTrocaMoldes
                        where FormularioTrocaMoldes.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                        join FormularioTMAtividades in db.FormularioTMAtividade
                        on FormularioTrocaMoldes.FormularioTrocaMoldeId equals FormularioTMAtividades.FormularioTrocaMoldeId
                        join AtividadeTM in db.AtividadeTMs
                        on FormularioTMAtividades.AtividadeTMId equals AtividadeTM.AtividadeTMId
                        select FormularioTMAtividades;

            var busca2 = from Formulario in db.FormularioTrocaMoldes
                         where Formulario.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join Relacional in db.FormularioTMAtividade
                         on Formulario.FormularioTrocaMoldeId equals Relacional.FormularioTrocaMoldeId
                         join Atividade in db.AtividadeTMs
                         on Relacional.AtividadeTMId equals Atividade.AtividadeTMId
                         select Relacional.AtividadeTM;

            var busca3 = from Formulario in db.FormularioTrocaMoldes
                         where Formulario.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join Relacional in db.FormularioMolde
                         on Formulario.FormularioTrocaMoldeId equals Relacional.FormularioTrocaMoldeId
                         join Molde in db.Moldes
                         on Relacional.MoldeId equals Molde.MoldeId
                         select Relacional.Molde;

            //var user = User.Identity.Name;
            //db.Entry(formularioTrocaMolde).Reference(f => f.Molde).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();
            formularioTrocaMolde.FormularioTMAtividades = busca.ToList();
            formularioTrocaMolde.AtividadesTM = busca2.ToList();
            formularioTrocaMolde.Moldes = busca3.ToList();

            if (formularioTrocaMolde == null)
            {
                return HttpNotFound();
            }
            return View(formularioTrocaMolde);
        }

        // GET: FormularioTrocaMoldes/Create
        public ActionResult Create()
        {
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina");
            ViewBag.MoldeId = new SelectList(db.Moldes, "MoldeId", "NomeMolde");
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario");
            return View();
        }

        // POST: FormularioTrocaMoldes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FormularioTrocaMoldeId,DtRetirada,DtColocar,ColocarInicio,ColocarFim,RetirarInicio,RetirarFim,Supervisor,MoldeId,MaquinaId,UsuarioId")] FormularioTrocaMolde formularioTrocaMolde)
        {
            if (ModelState.IsValid)
            {
                db.FormularioTrocaMoldes.Add(formularioTrocaMolde);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioTrocaMolde.MaquinaId);
            //ViewBag.MoldeId = new SelectList(db.Moldes, "MoldeId", "NomeMolde", formularioTrocaMolde.MoldeId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", formularioTrocaMolde.UsuarioId);
            return View(formularioTrocaMolde);
        }

        // GET: FormularioTrocaMoldes/Edit/5
        [Authorize(Roles = "Acesso Total, Acesso Supervisor, Acesso Produção")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(id);
            var busca = from FormularioTrocaMoldes in db.FormularioTrocaMoldes
                        where FormularioTrocaMoldes.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                        join FormularioTMAtividades in db.FormularioTMAtividade
                        on FormularioTrocaMoldes.FormularioTrocaMoldeId equals FormularioTMAtividades.FormularioTrocaMoldeId
                        join AtividadeTM in db.AtividadeTMs
                        on FormularioTMAtividades.AtividadeTMId equals AtividadeTM.AtividadeTMId
                        select FormularioTMAtividades;

            var busca2 = from FormularioTrocaMoldes in db.FormularioTrocaMoldes
                         where FormularioTrocaMoldes.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join FormularioTMAtividades in db.FormularioTMAtividade
                         on FormularioTrocaMoldes.FormularioTrocaMoldeId equals FormularioTMAtividades.FormularioTrocaMoldeId
                         join AtividadeTM in db.AtividadeTMs
                         on FormularioTMAtividades.AtividadeTMId equals AtividadeTM.AtividadeTMId
                         select FormularioTMAtividades.AtividadeTM;

            var busca3 = from Formulario in db.FormularioTrocaMoldes
                         where Formulario.FormularioTrocaMoldeId == formularioTrocaMolde.FormularioTrocaMoldeId
                         join Relacional in db.FormularioMolde
                         on Formulario.FormularioTrocaMoldeId equals Relacional.FormularioTrocaMoldeId
                         join Molde in db.Moldes
                         on Relacional.MoldeId equals Molde.MoldeId
                         select Relacional.Molde;

            //db.Entry(formularioTrocaMolde).Reference(f => f.Molde).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();
            formularioTrocaMolde.FormularioTMAtividades = busca.ToArray();
            formularioTrocaMolde.AtividadesTM = busca2.ToArray();
            formularioTrocaMolde.Moldes = busca3.ToList();
            if (formularioTrocaMolde == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioTrocaMolde.MaquinaId);
            //ViewBag.MoldeId = new SelectList(db.Moldes, "MoldeId", "NomeMolde", formularioTrocaMolde.MoldeId);
            ViewBag.UsuarioId = db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor");
            //ViewBag.UsuarioId = new SelectList(db.Usuarios.Where(u => u.Hierarquia == "Acesso Produção" || u.Hierarquia == "Acesso Supervisor"), "UsuarioId", "NomeUsuario", formularioTrocaMolde.UsuarioId);

            return View(formularioTrocaMolde);
        }

        // POST: FormularioTrocaMoldes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FormularioTrocaMoldeId,DtRetirada,DtColocar,ColocarInicio,ColocarFim,RetirarInicio,RetirarFim,Supervisor,FormularioTMAtividades,FormularioTMAtividadeId,StatusTm")] FormularioTrocaMolde formularioTrocaMolde, [Bind(Include = "MoldeId")] Molde m2, [Bind(Include = "MaquinaId")] Maquina m1, int Executor, bool Status, ICollection<int> id)
            {

            List<FormularioTMAtividade> form = new List<FormularioTMAtividade>();
            var atvs1 = Request.Form["id"];
            if (atvs1 != null )
            {
                var atvs = atvs1.Split(',').Select(Int32.Parse).ToArray();
                foreach (var x in atvs)
                {
                    FormularioTMAtividade f1 = new FormularioTMAtividade();
                    f1 = db.FormularioTMAtividade.Find(x);
                    f1.StatusTM = true;
                    form.Add(f1);
                }

            }
            

            formularioTrocaMolde.FormularioTMAtividades = form;
            // formularioTrocaMolde.MoldeId = m2.MoldeId;
            formularioTrocaMolde.MaquinaId = m1.MaquinaId;
            //formularioTrocaMolde.UsuarioId = u1.UsuarioId;
            formularioTrocaMolde.UsuarioId = Executor;
            formularioTrocaMolde.Status = Status;
            var a = formularioTrocaMolde.FormularioTMAtividades;

            // if (ModelState.IsValid)
            // {              
            db.Entry(formularioTrocaMolde).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            //}
            /*
            ViewBag.MaquinaId = new SelectList(db.Maquinas, "MaquinaId", "NomeMaquina", formularioTrocaMolde.MaquinaId);
            ViewBag.MoldeId = new SelectList(db.Moldes, "MoldeId", "NomeMolde", formularioTrocaMolde.MoldeId);
            ViewBag.UsuarioId = new SelectList(db.Usuarios, "UsuarioId", "NomeUsuario", formularioTrocaMolde.UsuarioId);
            return View(formularioTrocaMolde);*/
        }



        // GET: FormularioTrocaMoldes/Delete/5
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(id);
            db.Entry(formularioTrocaMolde).Reference(f => f.Maquina).Load();
            db.Entry(formularioTrocaMolde).Reference(f => f.Usuario).Load();

            if (formularioTrocaMolde == null)
            {
                return HttpNotFound();
            }
            return View(formularioTrocaMolde);
        }

        // POST: FormularioTrocaMoldes/Delete/5        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FormularioTrocaMolde formularioTrocaMolde = db.FormularioTrocaMoldes.Find(id);
            db.FormularioTrocaMoldes.Remove(formularioTrocaMolde);
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
