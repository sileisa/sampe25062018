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
    public class MoldesController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Moldes
        public ActionResult Index()
        {
            return View(db.Moldes.ToList().OrderBy(u => u.NomeMolde));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "MoldeId,NomeMolde,Cavidade")] Molde molde)
        {
            var moldes = db.Moldes.ToList();
            var x = db.Moldes.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {
                cont++;

                if (molde.NomeMolde == moldes[i].NomeMolde && molde.Cavidade == moldes[i].Cavidade)
                {
                    
                    ViewBag.Error = "Molde já existente";
                    break;
                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.Moldes.Add(molde);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            
            return View(db.Moldes.ToList());
        }
        // GET: Moldes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Molde molde = db.Moldes.Find(id);
            if (molde == null)
            {
                return HttpNotFound();
            }
            return View(molde);
        }

        // GET: Moldes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Moldes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MoldeId,NomeMolde,Cavidade")] Molde molde)
        {
            var moldes = db.Moldes.ToList();
            var x = db.Moldes.Count();
            var cont = 0;
            for (var i = 0; i<cont; i++ )
            {
                cont++;
               
                if (molde == moldes[i])
                {
                    //ModelState.AddModelError("NomeMolde", "Molde existente");

                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.Moldes.Add(molde);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            

            return View(molde);
        }

        // GET: Moldes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Molde molde = db.Moldes.Find(id);
            if (molde == null)
            {
                return HttpNotFound();
            }
            return View(molde);
        }

        // POST: Moldes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MoldeId,NomeMolde,Cavidade")] Molde molde)
        {
            if (ModelState.IsValid)
            {
                db.Entry(molde).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(molde);
        }

        // GET: Moldes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Molde molde = db.Moldes.Find(id);
            if (molde == null)
            {
                return HttpNotFound();
            }
            return View(molde);
        }

        // POST: Moldes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Molde molde = db.Moldes.Find(id);
			var busca = db.FormularioMolde.Where(o => o.MoldeId == id);

			if (busca.Count() > 0)
			{
				ViewBag.Error = "Não é possível deletar este Molde, pois está sendo utilizado em outras partes do sistema.";
			}
			else { 
			db.Moldes.Remove(molde);
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
