using dazzprise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;

namespace dazzprise1.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        [Authorize]
        public ActionResult Index()
        {
            using (var db = new dazzpriseEntities1())
            {
                return View(db.producto.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto)
        {
            if (!ModelState.IsValid)
                return View();

            /*      HttpPostedFileBase FileBase = Request.Files[0];

      WebImage image = new WebImage(FileBase.InputStream);

      ProductoController.imagen = image.GetBytes; */

            /*   string fileName = Path.GetFileNameWithoutExtension(producto.imagen);
               string extension = Path.GetExtension(producto.imagen);
               fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
               producto.imagen = "~/Image/" + fileName;
               fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
               producto.imagen.SaveAs(fileName);
               using (producto db = new producto())
               {
                   db.imagen.(producto);
                   db.SaveChanges();
               }
               ModelState.Clear();

               return View(); */

            try
            {

                using (var db = new dazzpriseEntities1())
                {

                    
                    db.producto.Add(producto);
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
                var findUser = db.producto.Find(id);
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

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    var findUser = db.producto.Find(id);
                    db.producto.Remove(findUser);
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(producto editUser)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    producto user = db.producto.Find(editUser.id);

                    user.id = editUser.id;
                    user.nombre = editUser.nombre;
                    user.descripcion = editUser.descripcion;
                    user.precio = editUser.precio;
                    user.marca = editUser.marca;
                    user.modelo = editUser.modelo;
                    user.imagen = editUser.imagen;
                    


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