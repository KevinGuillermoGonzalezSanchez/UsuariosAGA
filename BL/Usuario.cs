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
                                 where a.IdUsuario == IdUsuario
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
        public static ML.Result GetByIdLinq(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (UsuariosAGAEntities conn = new UsuariosAGAEntities())
                {
                    var listUsuarios = (from usuarioDB in conn.Usuarios
                                        where usuarioDB.IdUsuario == IdUsuario

                                        select new
                                        {
                                            IdUsuario = usuarioDB.IdUsuario,
                                            Nombre = usuarioDB.Nombre,
                                            ApellidoPaterno = usuarioDB.ApellidoPaterno,
                                            ApellidoMaterno = usuarioDB.ApellidoMaterno,
                                            FechaNacimiento = usuarioDB.FechaNacimiento

                                        }).SingleOrDefault();

                    if (listUsuarios != null)
                    {
                        result.Object = new List<object>();

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = listUsuarios.IdUsuario;
                        usuario.Nombre = listUsuarios.Nombre;
                        usuario.ApellidoPaterno = listUsuarios.ApellidoPaterno;
                        usuario.ApellidoMaterno = listUsuarios.ApellidoMaterno;
                        usuario.FechaNacimiento = (listUsuarios.FechaNacimiento).Value.ToString("dd/MM/yyyy");

                        result.Object = usuario;
                        result.Correct=true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }
            return result;
        }

        public static ML.Result UpdateLinq(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
            }

            return result;
        }


        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UsuariosAGAEntities context = new DL_EF.UsuariosAGAEntities())
                {
                    var listUsuarios = (from UsuarioDB in context.Usuarios
                                        select new
                                        {
                                            IdUsuario = UsuarioDB.IdUsuario,
                                            Nombre = UsuarioDB.Nombre,
                                            ApellidoPaterno = UsuarioDB.ApellidoPaterno,
                                            ApellidoMaterno = UsuarioDB.ApellidoMaterno,
                                            FechaNacimiento = UsuarioDB.FechaNacimiento,

                                        }).ToList();

                    if (listUsuarios != null && listUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in listUsuarios)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.FechaNacimiento = obj.FechaNacimiento?.ToString("dd/MM/yyyy");
                            result.Objects.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
