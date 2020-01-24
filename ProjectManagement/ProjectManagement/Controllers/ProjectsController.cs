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
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private ProjectManagementContext db = new ProjectManagementContext();
        UsersController UCDetails = new UsersController();

        // GET: api/Projects
        [HttpGet]
        [Route("GetProjects")]
        public IQueryable<Project> GetProjects()
        {
            var list= db.Projects;
            return list;
        }

        // GET: api/Projects/5
        [HttpGet]
        [ResponseType(typeof(Project))]
        public IHttpActionResult GetProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(Project))]
        public IHttpActionResult PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.Project_ID)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Success");
        }

        // POST: api/Projects
        [HttpPost]
        [ResponseType(typeof(Project))]
        public IHttpActionResult PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (project.Project_ID != 0)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                db.Projects.Add(project);
                db.SaveChanges();
                if (project.User_ID > 0)
                {
                    int userid = (int)project.User_ID;
                    User user = db.Users.Find(userid);
                    user.Project_ID = project.Project_ID;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = project.Project_ID }, project);
        }

        // DELETE: api/Projects/5
        
        [HttpGet]
        [ResponseType(typeof(Project))]
        [Route("DeleteProject/{id}")]
        public IHttpActionResult DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            if(project.User_ID > 0)
            {
                User user = db.Users.Find(project.User_ID);
                if (user != null)
                {
                    user.Project_ID = null;
                    db.SaveChanges();
                }
            }
            db.Projects.Remove(project);
            db.SaveChanges();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.Project_ID == id) > 0;
        }
    }
}