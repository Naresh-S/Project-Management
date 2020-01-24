using NUnit.Framework;
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
    [TestFixture]
    public class TasksControllerTests
    {
        private static TasksController _TaskController;
        private static UsersController _UsersController;
        public TasksControllerTests()
        {
            _TaskController = new TasksController();
            _UsersController = new UsersController();
        }
        [Test]
        public void AddNewTask_ValidTask_ReturnsAllTasks()
        {
            bool isValidTask = false;

            while (!isValidTask)
            {
                var TaskId = new Random().Next();
                var TaskI = _TaskController.GetTask(2);
                var TaskInDb = _TaskController.GetTask(TaskId);
                if (TaskInDb.GetType() == typeof(NotFoundResult))
                {
                    var user = new DAL.User()
                    {
                        User_ID = 2,
                        Employee_ID = 10003,
                        FirstName = "Test_First Name_" + RandomString(3, true),
                        LastName = "Test_LastNmae_" + RandomString(3, true)

                    };
                    //_UsersController.PostUser(user);
                    isValidTask = true;
                    
                    var Task = new DAL.Task()
                    {
                        Task_ID = 1,
                        Start_Date = DateTime.Now.AddDays(1),
                        End_Date = DateTime.Now.AddDays(2),
                        Priority = 1,
                        TaskName = "Test_Nunit_" + RandomString(5,true),
                        User_ID = 2
                    };
                    IHttpActionResult actionResult = _TaskController.PostTask(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Task>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
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

        [Test]
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
                        Task_ID = 0,
                        Priority = 0,
                        TaskName = "Test_Nunit_" + RandomString(5, true)
                    };
                    IHttpActionResult actionResult = _TaskController.PostTask(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.Task>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
                }
            }
        }
        [Test]
        public void GetTasks_All_ReturnAllTasks()
        {
            IQueryable<DAL.Task> actionResult = _TaskController.GetTasks();

            Assert.IsNotNull(actionResult);
        }
        [Test]
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
        [Test]
        public void GetParentTasksTest()
        {
            IQueryable<DAL.ParentTask> actionResult = _TaskController.GetParentTasks();

            Assert.IsNotNull(actionResult);
        }

       
        [Test]
        public void PutTaskTest()
        {
            IQueryable<DAL.Task> result = _TaskController.GetTasks();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            Task.TaskName = String.Format("Task {0}", new Random().Next());
            IHttpActionResult updateActionResult=_TaskController.PutTask(Task.Task_ID, Task);
            Assert.IsNotNull(updateActionResult);
            Assert.AreEqual(((System.Web.Http.Results.OkNegotiatedContentResult<string>)updateActionResult).Content, "Success");
        }

        [Test]
        public void DeleteTaskTest()
        {
                IHttpActionResult deleteActionResult = _TaskController.DeleteTask(1);
                Assert.IsNotNull(deleteActionResult);
                var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.Task>)deleteActionResult).Content;
                Assert.IsNotNull(contentResult);
        }
    }
}