using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sampe.Models;

namespace Sampe.Controllers
{    
    public class LoginController : Controller
    {
        private SampeContext db = new SampeContext();

        // GET: Login
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }

        // GET: Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Login/Create
        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnUrl = returnURL;
            return View(new Login());
        }

        // POST: Login/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "LoginId,User,Senha")] Login login, String returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (SampeContext db = new SampeContext())
                {
                    //Usuario usuario = new Usuario();
                    var usuarios = db.Usuarios;
                    foreach (var item in usuarios)
                    {
                        if (login.User == item.Login && login.Senha == item.Senha)
                        {
                            FormsAuthentication.SetAuthCookie(item.Login, false);
                            if (Url.IsLocalUrl(returnUrl)
                              && returnUrl.Length > 1
                             && returnUrl.StartsWith("/")
                             && !returnUrl.StartsWith("//")
                              && returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            /*código abaixo cria uma session para armazenar o nome do usuário*/
                            Session["NomeUsuario"] = item.NomeUsuario;
                            /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                            Session["SobrenomeUsuario"] = item.SobrenomeUsuario;
                            /*código abaixo cria uma session para armazenar o id do usuário*/
                            Session["UsuarioId"] = item.UsuarioId;
                            /*código abaixo cria uma session para armazenar a hierarquia*/
                            Session["Hierarquia"] = item.Hierarquia;
							/*retorna para a tela inicial do Home*/
							Session["Senha"] = item.Senha;
							return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                            ModelState.AddModelError("User", "Login e/ou senha inválidos");
                        }

                    }
                }
            }
            return View(login);
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginId,User,Senha")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
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
