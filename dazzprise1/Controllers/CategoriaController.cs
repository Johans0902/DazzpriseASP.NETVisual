using dazzprise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dazzprise1.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            using (var db = new dazzpriseEntities1())
            {
                return View(db.categoria.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(categoria categoria)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {

                using (var db = new dazzpriseEntities1())
                {
                    db.categoria.Add(categoria);
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

        public ActionResult Details(int id)
        {
            using (var db = new dazzpriseEntities1())
            {
                var findUser = db.categoria.Find(id);
                return View(findUser);
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    categoria findUser = db.categoria.Where(a => a.id == id).FirstOrDefault();
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
        public ActionResult Edit(categoria editUser)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    categoria user = db.categoria.Find(editUser.id);

                    user.nombre_categoria = editUser.nombre_categoria;
                    user.producto = editUser.producto;
                

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

        public ActionResult Delete(int id)
        {
            try
            {
                using (var db = new dazzpriseEntities1())
                {
                    var findUser = db.categoria.Find(id);
                    db.categoria.Remove(findUser);
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
    }
}