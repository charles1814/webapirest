using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Conection;

namespace webapirest.Controllers
{
    public class UsuariosController : ApiController
    {
        private DBPRUEBAEntities dbcontext = new DBPRUEBAEntities();

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            using (DBPRUEBAEntities db = new DBPRUEBAEntities())
            {
                return db.Usuarios.ToList();
            }
        }
        [HttpGet]
        public Usuario Get(int id)
        {
            using(DBPRUEBAEntities db= new DBPRUEBAEntities())
            {
                return db.Usuarios.FirstOrDefault(u => u.int_id == id);
            }
        }
        [HttpPost]
        public IHttpActionResult NuevoUsuario([FromBody]Usuario model)
        {
            if (ModelState.IsValid)
            {
                dbcontext.Usuarios.Add(model);
                dbcontext.SaveChanges();

                return Ok(model);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IHttpActionResult Edit(int id,[FromBody]Usuario model)
        {
            if (ModelState.IsValid)
            {
                var UsuarioExiste = dbcontext.Usuarios.Count(e => e.int_id == id) > 0;
                if (UsuarioExiste)
                {
                    dbcontext.Entry(model).State = EntityState.Modified;
                    dbcontext.SaveChanges();
                    return Ok();


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
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var usuario = dbcontext.Usuarios.Find(id);

            if (usuario != null)
            {
                dbcontext.Usuarios.Remove(usuario);
                dbcontext.SaveChanges();

                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
