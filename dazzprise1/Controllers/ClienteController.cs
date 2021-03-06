using dazzprise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace dazzprise1.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new dazzpriseEntities1())
            {
                return View(db.usuario.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(usuario usuario)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new dazzpriseEntities1())
                {

                    usuario.contraseña = UsuarioController.HashSHA1(usuario.contraseña);
                    db.usuario.Add(usuario);
                    _ = db.SaveChanges();
                    return RedirectToAction("index");

                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }


        public static string HashSHA1(string value)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = sha1.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {

                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public ActionResult Details(int id)
        {
            using (var db = new dazzpriseEntities1())
            {
                var findUser = db.usuario.Find(id);
                return View(findUser);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    usuario findUser = db.usuario.Where(a => a.id == id).FirstOrDefault();
                    return View(findUser);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(usuario editUser)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    usuario user = db.usuario.Find(editUser.id);

                    user.email = editUser.email;
                    user.contraseña = editUser.contraseña;





                    db.SaveChanges();
                    return RedirectToAction("index");
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", "error " + ex);
                return View();
            }
        }
        public ActionResult Login(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

    }
}