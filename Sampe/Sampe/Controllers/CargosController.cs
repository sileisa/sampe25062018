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
    [Authorize]
    public class CargosController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Cargos
        public ActionResult Index()
        {
            return View(db.Cargoes.ToList().OrderBy(u => u.NomeCargo));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "CargoId,NomeCargo,DescricaoCargo")] Cargo cargo)
        {
            var cargos = db.Cargoes.ToList();
            var x = db.Cargoes.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {
                cont++;

                if (cargo.NomeCargo == cargos[i].NomeCargo)
                {

                    ViewBag.Error = "Cargo já existente";
                    break;
                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.Cargoes.Add(cargo);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(db.Cargoes.ToList());
        }

        // GET: Cargos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // GET: Cargos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cargos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CargoId,NomeCargo,DescricaoCargo")] Cargo cargo)
        {
            var cargos = db.Cargoes.ToList();
            var x = db.Cargoes.Count();
            var cont = 0;
            for (var i = 0; i <= cont; i++)
            {
                cont++;

                if (cargo.NomeCargo == cargos[i].NomeCargo)
                {

                    ViewBag.Error = "Máquina já existente";
                    break;
                }
                else if (cont == x)
                {
                    if (ModelState.IsValid)
                    {
                        db.Cargoes.Add(cargo);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(db.Cargoes.ToList());
        }

        // GET: Cargos/Edit/5
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CargoId,NomeCargo,DescricaoCargo")] Cargo cargo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cargo);
        }

        // GET: Cargos/Delete/5
        [Authorize(Roles = "Acesso Total, Acesso Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cargo cargo = db.Cargoes.Find(id);
            if (cargo == null)
            {
                return HttpNotFound();
            }
            return View(cargo);
        }

        // POST: Cargos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cargo cargo = db.Cargoes.Find(id);
			var busca = db.Usuarios.Where(o => o.CargoId == id);
			if (busca.Count() > 0)
			{
				ViewBag.Error = "Não é possível deletar este Cargo, pois está sendo utilizado em outras partes do sistema.";
			}
			else
			{ 
            db.Cargoes.Remove(cargo);
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
