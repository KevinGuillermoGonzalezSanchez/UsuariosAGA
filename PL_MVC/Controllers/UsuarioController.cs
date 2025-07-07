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
        public ActionResult GetAll()
         {
             ML.Usuario usuario = new ML.Usuario();
             ML.Result result = BL.Usuario.GetAll();

             if (result.Correct)
             {
                 usuario.Usuarios = new List<object>();
                 usuario.Usuarios = result.Objects;

             }

             return View(usuario);
         }

       /* public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();

            var respuesta = GetAll();

            if (respuesta.Correct)
            {
                usuario.Usuarios = respuesta.Objects.ToList();
            }

            return View(usuario);
        }*/



        // GET: Usuario
        [HttpGet]
        public ActionResult Formulario(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            if (IdUsuario == 0)
            {

            }
            else
            {
                var respuesta = BL.Usuario.GetByIdLinq((int)IdUsuario);

                if (respuesta.Correct == true)
                {
                    usuario = (ML.Usuario)respuesta.Object;
                }
            }
            return View(usuario);
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
                result = BL.Usuario.UpdateLinq(usuario);
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