using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DL_EF;
using ML;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddLinQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (UsuariosAGAEntities context = new UsuariosAGAEntities())
                {
                    DL_EF.Usuario usuarioDL = new DL_EF.Usuario();

                    usuarioDL.Nombre = usuario.Nombre;
                    usuarioDL.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDL.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDL.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                    context.Usuarios.Add(usuarioDL);

                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar el Usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Exception = ex;
            }
            return result;
        }

        public static ML.Result DeleteLinQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (UsuariosAGAEntities context = new UsuariosAGAEntities())
                {
                    var query = (from a in context.Usuarios
                                 where a.IdUsuario = IdUsuario
                                 select a).First();
                    if (query != null)
                    {
                        context.Usuarios.Remove(query);
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el Usuario a Eliminar";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Exception = ex;
            }

            return result;
        }

    }
}
