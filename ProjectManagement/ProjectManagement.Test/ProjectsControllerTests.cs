using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    [TestClass]
    public class ProjectsControllerTests
    {
        private static ProjectsController _ProjectsController;
        public ProjectsControllerTests()
        {
            _ProjectsController = new ProjectsController();
        }
        [TestMethod]
        public void AddNewProject_ValidProject_ReturnsAllProjects()
        {
            bool isValidTask = false;

            while (!isValidTask)
            {
                var TaskId = new Random().Next();

                var TaskInDb = _ProjectsController.GetProject(TaskId);
                if (TaskInDb.GetType() == typeof(NotFoundResult))
                {
                    isValidTask = true;

                    var Task = new DAL.Project()
                    {
                        Project_ID = TaskId,
                        StartDate = DateTime.Now.AddDays(1),
                        EndDate = DateTime.Now.AddDays(2),
                        Priority = 1,
                        ProjectName = "Test_Nunit",
                    };
                    IHttpActionResult actionResult = _ProjectsController.PostProject(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Project>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
                }
            }
        }
        [TestMethod]
        public void GetProjectsTest()
        {
            IQueryable<DAL.Project> actionResult = _ProjectsController.GetProjects();

            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
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

        [TestMethod]
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

                Assert.IsNotNull(((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Project>)updateActionResult).Content);
            }
        }



        [TestMethod]
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
    }
}