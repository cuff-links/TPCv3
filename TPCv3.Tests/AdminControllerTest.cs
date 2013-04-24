using System.Web.Mvc;
using System.Web;
using NUnit.Framework;
using Rhino.Mocks;
using TPCv3.Controllers;
using TPCv3.Models;
using TPCv3.Providers;

namespace TPCv3.Tests{
    [TestFixture]
    public class AdminControllerTest{
        #region Constants and Fields

        private AdminController _adminController;

        private IAuthProvider _authProvider;

        #endregion

        #region Public Methods and Operators

        [SetUp]
        public void SetUp(){
            _authProvider = MockRepository.GenerateMock<IAuthProvider>();
            _adminController = new AdminController(_authProvider);
        }

        [Test]
        public void TestIsLoggedInFalse(){
            //Arrange
            _authProvider.Stub(s => s.IsLoggedIn).Return(false);

            //Act
            ActionResult actual = _adminController.Login("/");

            //Assert
            Assert.IsInstanceOf<ViewResult>(actual);
            Assert.AreEqual("/", ((ViewResult) actual).ViewBag.ReturnUrl);
        }


        [Test]
        public void TestLoginPostModelInvalid(){
            // arrange
            var model = new LoginModel();
            _adminController.ModelState.AddModelError("Username", "Username is required");

            // act
            ActionResult actual = _adminController.Login(model, "/");

            // assert
            Assert.IsInstanceOf<ViewResult>(actual);
        }

        [Test]
        public void TestLoginPostUserInvalid(){
            // arrange
            var model = new LoginModel {Username = "invaliduser", Password = "password"};
            _authProvider.Stub(s => s.Login(model.Username, model.Password)).Return(false);

            // act
            ActionResult actual = _adminController.Login(model, "/");

            // assert
            Assert.IsInstanceOf<ViewResult>(actual);
            ModelErrorCollection modelStateErrors = _adminController.ModelState[""].Errors;
            Assert.IsTrue(modelStateErrors.Count > 0);
            Assert.AreEqual("The user name or password provided is incorrect.", modelStateErrors[0].ErrorMessage);
        }

        #endregion
    }
}