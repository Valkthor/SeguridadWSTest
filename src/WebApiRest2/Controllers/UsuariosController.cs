using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiRest2.Models;

namespace WebApiRest2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {


        [HttpGet]
        public IEnumerable<TablaUsuarios> Get()
        {
            using (var context = new sistemaLoginContext() )
            {
                // == OBTENER TODOS LOS USUARIOS ==
                //return context.TablaUsuarios.ToList();

                // == OBTENER UN USUARIO ==
                //return context.TablaUsuarios.Where(TablaUsuarios => TablaUsuarios.Idempresa == 937).ToList();

                // == PARA INSERTAR ==
                //TablaUsuarios tbusuarios = new TablaUsuarios();
                //tbusuarios.Idempresa = 937;
                //tbusuarios.IdUsuario = 557;
                //context.TablaUsuarios.Add(tbusuarios);
                //context.SaveChanges();

                // == PARA ACTUALIZAR ==
                //TablaUsuarios tbusuarios = context.TablaUsuarios.Where(tablaUsuarios => tablaUsuarios.IdPruenbas == 6).FirstOrDefault();
                //tbusuarios.Idempresa = 687;
                //context.SaveChanges();

                // == PARA ELIMINAR ==
                TablaUsuarios tbusuarios = context.TablaUsuarios.Where(tablaUsuarios => tablaUsuarios.IdPruenbas == 7).FirstOrDefault();
                context.TablaUsuarios.Remove(tbusuarios);
                context.SaveChanges();

                return context.TablaUsuarios.ToList();

            }
        }
    }
}
