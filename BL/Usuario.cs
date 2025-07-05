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

        }

     

    }
}
