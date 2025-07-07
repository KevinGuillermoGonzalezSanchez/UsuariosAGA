using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Formulario()
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            if (IdUsuario == 0)
            {

            }
            else
            {
                var respuesta = GetByIdLinq((int)IdUsuario);

                    if (respuesta.Correct = true)
                    {
                    usuario = (ML.Usuario)respuesta.Object;
                    }
            }
        }

        [HttpPost]
        public ActionResult Formulario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.AddLinQ(usuario);
                return RedirectToAction("GetAll");
            }
            else
            {
                result = BL.UpdateLinq(IdUsuario);
                return RedirectToAction("GetAll");
            }

            return View();
        }
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteLinQ(IdUsuario);
            if (result.Correct)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                ViewBag.ErrorMessage = "Error al eliminar";
                return View("Error");
            }
        }
    }
}