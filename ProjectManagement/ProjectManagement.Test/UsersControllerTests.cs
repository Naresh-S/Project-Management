using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagement.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProjectManagement.Controllers.Tests
{
    [TestClass]
    public class UsersControllerTests
    {
        private static UsersController _UsersController;
        public UsersControllerTests()
        {
            _UsersController = new UsersController();
        }
        [TestMethod]
        public void AddNewUser_ValidUser_ReturnsAllUsers()
        {
            bool isValidTask = false;

            while (!isValidTask)
            {
                var TaskId = new Random().Next();

                var TaskInDb = _UsersController.GetUser(TaskId);
                if (TaskInDb.GetType() == typeof(NotFoundResult))
                {
                    isValidTask = true;

                    var Task = new DAL.User()
                    {
                        User_ID = TaskId,
                        Employee_ID = new Random().Next(),
                        FirstName = "Test_First Name",
                        LastName="Test_LastNmae"
                };
                    IHttpActionResult actionResult = _UsersController.PostUser(Task);
                    var contentResult = ((System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<ProjectManagement.DAL.User>)actionResult);

                    Assert.IsNotNull(contentResult);
                    Assert.IsNotNull(contentResult.Content);
                }
            }
        }
        [TestMethod]
        public void GetUsersTest()
        {
            IQueryable<DAL.User> actionResult = _UsersController.GetUsers();

            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public void GetUserTest()
        {
            IQueryable<DAL.User> result = _UsersController.GetUsers();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            IHttpActionResult actionResult = _UsersController.GetUser(Task.User_ID);
            var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.User>)actionResult).Content;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(Task.User_ID, contentResult.User_ID);
        }
        [TestMethod]
        public void PutUserTest()
        {
            IQueryable<DAL.User> result = _UsersController.GetUsers();
            Assert.IsNotNull(result);
            var Task = result.FirstOrDefault();
            Task.FirstName = String.Format("First Name {0}", new Random().Next());
            IHttpActionResult updateActionResult = _UsersController.PutUser(Task.User_ID, Task);
            Assert.IsNotNull(updateActionResult);

            Assert.IsNotNull(((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.User>)updateActionResult).Content);
        }



        [TestMethod]
        public void DeleteUserTest()
        {
            IQueryable<DAL.User> result = _UsersController.GetUsers();
            if (result != null)
            {
                Assert.IsNotNull(result);
                var Task = result.FirstOrDefault();
                IHttpActionResult deleteActionResult = _UsersController.DeleteUser(Task.User_ID);
                Assert.IsNotNull(deleteActionResult);
                var contentResult = ((System.Web.Http.Results.OkNegotiatedContentResult<ProjectManagement.DAL.User>)deleteActionResult).Content;
                Assert.IsNotNull(contentResult);
            }
        }
    }
}