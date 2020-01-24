using NUnit.Framework;
using ProjectManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProjectManagement.Controllers.Tests
{

    [TestFixture]
    public class ProjectsControllerTests
    {
        private static ProjectsController _ProjectsController;
        public ProjectsControllerTests()
        {
            _ProjectsController = new ProjectsController();
        }
        [Test]
        public void AddNewProject_ValidProject_ReturnsAllProjects()
        {
            bool isValidTask = false;

            while (!isValidTask)
            {
                var TaskId = 0;

                var TaskInDb = _ProjectsController.GetProject(TaskId);
                if (TaskInDb.GetType() == typeof(NotFoundResult))
                {
                    isValidTask = true;

                    var Task = new DAL.Project()
                    {
                        Project_ID = 0,
                        StartDate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(2),
                        Priority = 1,
                        ProjectName = "Test_Nunit_" + RandomString(5, true),
                    };
                    IHttpActionResult actionResult = _ProjectsController.PostProject(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Project>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
                }
            }
        }
        [Test]
        public void GetProjectsTest()
        {
            IQueryable<DAL.Project> actionResult = _ProjectsController.GetProjects();

            Assert.IsNotNull(actionResult);
        }

        [Test]
        public void GetProjectTest()
        {
            IQueryable<DAL.Project> result = _ProjectsController.GetProjects();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            if (Task != null)
            {
                IHttpActionResult actionResult = _ProjectsController.GetProject(Task.Project_ID);
                var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Project>)actionResult).Content;
                Assert.IsNotNull(contentResult);
                Assert.AreEqual(Task.Project_ID, contentResult.Project_ID);
            }
        }

        [Test]
        public void PutProjectTest()
        {
            IQueryable<DAL.Project> result = _ProjectsController.GetProjects();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            if (Task != null)
            {
                Task.ProjectName = String.Format("Project {0}", new Random().Next());
                IHttpActionResult updateActionResult = _ProjectsController.PutProject(Task.Project_ID, Task);
                Assert.IsNotNull(updateActionResult);

                Assert.AreEqual(((System.Web.Http.Results.OkNegotiatedContentResult<string>)updateActionResult).Content, "Success");
            }
        }



        [Test]
        public void DeleteProjectTest()
        {
            IQueryable<DAL.Project> result = _ProjectsController.GetProjects();
            if (result != null)
            {
                Assert.IsNotNull(result);
                var Task = result.FirstOrDefault();
                if (Task != null)
                {
                    IHttpActionResult deleteActionResult = _ProjectsController.DeleteProject(Task.Project_ID);
                    Assert.IsNotNull(deleteActionResult);
                    var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Project>)deleteActionResult).Content;
                    Assert.IsNotNull(contentResult);
                }
            }
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}