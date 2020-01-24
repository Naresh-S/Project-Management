using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Controllers;
using ProjectManagement.DAL;
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
    public class TasksControllerTests
    {
        private static TasksController _TaskController;
        private static UsersController _UsersController;
        public TasksControllerTests()
        {
            _TaskController = new TasksController();
            _UsersController = new UsersController();
        }
        [TestMethod]
        public void AddNewTask_ValidTask_ReturnsAllTasks()
        {
                bool isValidTask = false;

                while (!isValidTask)
                {
                    var TaskId = new Random().Next();

                    var TaskInDb = _TaskController.GetTask(TaskId);
                    if (TaskInDb.GetType() == typeof(NotFoundResult))
                    {
                        var user = new DAL.User()
                        {
                            User_ID = TaskId,
                            Employee_ID = new Random().Next(),
                            FirstName = "Test_First Name",
                            LastName = "Test_LastNmae"
                        };
                        _UsersController.PostUser(user);
                        isValidTask = true;

                        var Task = new DAL.Task()
                        {
                            Task_ID = TaskId,
                            Start_Date = DateTime.Now.AddDays(1),
                            End_Date = DateTime.Now.AddDays(2),
                            Priority = 1,
                            TaskName = "Test_Nunit",
                            User_ID = user.User_ID
                        };
                        IHttpActionResult actionResult = _TaskController.PostTask(Task);
                        var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Task>)actionResult);

                        Assert.IsNotNull(contentResult);
                        Assert.IsNotNull(contentResult.Content);
                    }
                }
        }


        [TestMethod]
        public void AddNewTask_ValidParentTask_ReturnsAllTasks()
        {
            bool isValidTask = false;

            while (!isValidTask)
            {
                var TaskId = new Random().Next();

                var TaskInDb = _TaskController.GetTask(TaskId);
                if (TaskInDb.GetType() == typeof(NotFoundResult))
                {
                    isValidTask = true;

                    var Task = new DAL.Task()
                    {
                        Task_ID = TaskId,
                        Priority = 0,
                        TaskName = "Test_Nunit"
                    };
                    IHttpActionResult actionResult = _TaskController.PostTask(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Task>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
                }
            }
        }
        [TestMethod]
        public void GetTasks_All_ReturnAllTasks()
        {
            IQueryable<DAL.Task> actionResult = _TaskController.GetTasks();

            Assert.IsNotNull(actionResult);
        }
        [TestMethod]
        public void GetTask_PassTaskId_ReturnsValidTask()
        {
            IQueryable<DAL.Task> result = _TaskController.GetTasks();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            IHttpActionResult actionResult= _TaskController.GetTask(Task.Task_ID);
            var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Task>)actionResult).Content;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(Task.Task_ID, contentResult.Task_ID);
        }
        [TestMethod]
        public void GetParentTasksTest()
        {
            IQueryable<DAL.ParentTask> actionResult = _TaskController.GetParentTasks();

            Assert.IsNotNull(actionResult);
        }


        [TestMethod]
        public void PutTaskTest()
        {
            IQueryable<DAL.Task> result = _TaskController.GetTasks();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            Task.TaskName = String.Format("Task {0}", new Random().Next());
            IHttpActionResult updateActionResult=_TaskController.PutTask(Task.Task_ID, Task);
            Assert.IsNotNull(updateActionResult);
            Assert.AreEqual(((System.Web.Http.Results.StatusCodeResult)updateActionResult).StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void DeleteTaskTest()
        {
            IQueryable<DAL.Task> result = _TaskController.GetTasks();
            if (result != null)
            {
                Assert.IsNotNull(result);
                var Task = result.FirstOrDefault();
                IHttpActionResult deleteActionResult = _TaskController.DeleteTask(Task.Task_ID);
                Assert.IsNotNull(deleteActionResult);
                var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Task>)deleteActionResult).Content;
                Assert.IsNotNull(contentResult);
            }
        }
    }
}