using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

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

                    if(listUsuarios != null && listUsuarios.Count > 0 )
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in listUsuarios)
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
