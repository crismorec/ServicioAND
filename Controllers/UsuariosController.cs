using ConectarDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ServicioAND.Controllers
{
    public class UsuariosController : ApiController
    {
        private UsuariosEntities dbContext = new UsuariosEntities();
    

        //Visualiza todos los registros (api/usuarios )
        [HttpGet]

        public IEnumerable<usuario> Get() 
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.usuarios.ToList();
            }
        }



        //Visualiza solo un registro (api/usuarios/1)
        [HttpGet]
        public usuario Get(int id)
        {
            using (UsuariosEntities usuariosentities = new UsuariosEntities())
            {
                return usuariosentities.usuarios.FirstOrDefault(e => e.id == id);
                //return usuariosentities.usuarios.ToList();
            }
        }



        //Graba nuevos registros en la base datos usuarios
        [HttpPost]
        public IHttpActionResult AgregarUsuario([FromBody]usuario usu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.usuarios.Add(usu);
                    dbContext.SaveChanges();
                    return Ok(usu);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();
            }
        }



        // Actualiza un registro
        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, [FromBody]usuario usu)
        {
            if (ModelState.IsValid)
            {
                var UsuarioExiste = dbContext.usuarios.Count(c => c.id == id) > 0;

                if (UsuarioExiste)
                {
                    try
                    {
                        dbContext.Entry(usu).State = EntityState.Modified;
                        dbContext.SaveChanges();
                        return Ok();
                    }
                    catch (Exception)
                    {

                        return BadRequest();
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }



        // Borra un registro (api/usuarios/1)
        [HttpDelete]
        public IHttpActionResult EliminarUsuario(int id)
        {
            var usu = dbContext.usuarios.Find(id);

            if (usu != null)
            {
                dbContext.usuarios.Remove(usu);
                dbContext.SaveChanges();

                return Ok(usu);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
