using dazzprise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

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

        public ActionResult NombreCategoria()
        {
            using (var db = new dazzpriseEntities1())
            {
                return PartialView(db.categoria.ToList());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(producto producto, HttpPostedFileBase imagen)
        { 

            if (!ModelState.IsValid)
            
                return View();

            //string para guardar la ruta
            string filePath = string.Empty;

            //condicion para saber si llego o no el archivo
            if (imagen != null)
            {
                //ruta de la carpeta que caragara el archivo
                string path = Server.MapPath("~/Uploads/");

                //verificar si la ruta de la carpeta existe
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //obtener el nombre del archivo
                filePath = path + Path.GetFileName(imagen.FileName);
                //obtener la extension del archivo
                string extension = Path.GetExtension(imagen.FileName);

                //guardando el archivo
                imagen.SaveAs(filePath);


            }

            //   return View();


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

                    producto.imagen = "/Uploads/" + Path.GetFileName(imagen.FileName);
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

/*var pic = string.Empty;
var folder = "~/Content/ImageProducto";

if (producto.imagen != null)
{
    pic = FilesHelper.UploadPhoto(producto.imagen, folder);
    pic = string.Format("{0}/{1}", folder, pic);
}

var image = ToFlower(producto);
producto.imagen = pic;


return RedirectToAction("index"); */