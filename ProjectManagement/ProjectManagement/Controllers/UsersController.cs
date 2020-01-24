using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectManagement.DAL;

namespace ProjectManagement.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private ProjectManagementContext db = new ProjectManagementContext();

        // GET: api/Users
        [HttpGet]
        [Route("GetUsers")]
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //user.User_ID = id;
            if (id != user.User_ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user.User_ID == 0)
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            else
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.User_ID }, user);
        }

        // DELETE: api/Users/5
        [HttpGet]
        [ResponseType(typeof(User))]
        [Route("DeleteUser/{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.User_ID == id) > 0;
        }
    }
}