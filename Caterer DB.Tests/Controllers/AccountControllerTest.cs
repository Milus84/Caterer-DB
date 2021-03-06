﻿using Ploeh.AutoFixture;
using Caterer_DB.Controllers;
using System.Linq;
using System.Web.Mvc;
using Caterer_DB.Models;
using Caterer_DB.Interfaces;
using Caterer_DB.Services;
using Business.Interfaces;
using Moq;
using DataAccess.Model;
using NUnit.Framework;

namespace Caterer_DB.Tests.Controllers
{

    [TestFixture]
    public class AccountControllerTest
    { 
        
        private Fixture Fixture { get; set; }

        private IBenutzerViewModelService MockBenutzerViewModelService { get; set; }
        private ILoginService MockLoginService { get; set; }
        private IBenutzerService MockBenutzerService { get; set; }
        private AccountController AccountController { get; set; }

        [OneTimeSetUp]
        public void TestInit()
        {
            Fixture = new Fixture();
            Fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => Fixture.Behaviors.Remove(b));
            Fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
            mockBenutzerService.Setup(x => x.SearchUserByEmail(It.IsAny<string>())).Returns(Fixture.Build<Benutzer>().Create());
            mockBenutzerService.Setup(x => x.VerifyRegistration(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockBenutzerService.Setup(x => x.VerifyPasswordChange(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockBenutzerService.Setup(x => x.CheckEmailForRegistration(It.IsAny<string>())).Returns(true);
            mockBenutzerService.Setup(x => x.RegisterBenutzer(Fixture.Build<Benutzer>().Create()));
            mockBenutzerService.Setup(x => x.SearchUserByEmail(It.IsAny<string>())).Returns(Fixture.Build<Benutzer>().With(x => x.IstEmailVerifiziert, true).Create());
            MockBenutzerService = mockBenutzerService.Object;

            var mockBenutzerViewModelService = new Mock<IBenutzerViewModelService>();
            mockBenutzerViewModelService.Setup(x => x.CreateNewRegisterBenutzerViewModel()).Returns(Fixture.Build<RegisterBenutzerViewModel>().Create());
            mockBenutzerViewModelService.Setup(x => x.Get_ForgottenPasswordCreateNewPasswordViewModel_ByBenutzerId(It.IsAny<int>())).Returns(Fixture.Build<ForgottenPasswordCreateNewPasswordViewModel>().Create());
            mockBenutzerViewModelService.Setup(x => x.Map_ForgottenPasswordCreateNewPasswordViewModel_Benutzer(It.IsAny<ForgottenPasswordCreateNewPasswordViewModel>())).Returns(Fixture.Build<Benutzer>().Create());
            mockBenutzerViewModelService.Setup(x => x.Map_RegisterBenutzerViewModel_Benutzer(It.IsAny<RegisterBenutzerViewModel>())).Returns(Fixture.Build<Benutzer>().Create());
            mockBenutzerViewModelService.Setup(x => x.AddListsToRegisterViewModel(It.IsAny<RegisterBenutzerViewModel>())).Returns(Fixture.Build<RegisterBenutzerViewModel>().Create());
            MockBenutzerViewModelService = mockBenutzerViewModelService.Object;

            var mockLoginService = new Mock<ILoginService>();
            mockLoginService.Setup(x => x.Abmelden());
            mockLoginService.Setup(x => x.AnmeldePrüfung(It.IsAny<string>(), It.IsAny<string>())).Returns(LoginSuccessLevel.Erfolgreich);
            MockLoginService = mockLoginService.Object;
            AccountController = new AccountController(MockLoginService, MockBenutzerService, MockBenutzerViewModelService);
        }

        [Test]
        public void Login_HTTPGet_Test()
        {
            //Arrange
            
            //Act
            ActionResult result = AccountController.Login(null);
          
            //Assert
            Assert.IsNotNull(result);
           
        }

        [Test]
        public void Register_HTTPGet_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.Register("");
            var ViewModel = ((ViewResult)result).Model;
            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(ViewModel);
        }

        [Test]
        public void RegisterSuccessfull_HTTPGet_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.RegisterSuccsessfull("");

            //Assert
            Assert.IsNotNull(result);
        }

        
        [Test]
        public void RegisterComplete_HTTPGet_TrueVerifyCode_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.RegisterComplete("TestID","TESTVerify");
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterComplete_HTTPGet_FalseVerifyCodeTest()
        {
            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
            mockBenutzerService.Setup(x => x.VerifyRegistration(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var newMockBenutzerService = mockBenutzerService.Object;
            var accountController = new AccountController(MockLoginService, newMockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            
            //Act
            ActionResult result = accountController.RegisterComplete("3", "TESTVerify");
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordChange_HTTPGet_TrueVerifyCode_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.PasswordChange("3", "TESTVerify");
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordChange_HTTPGet_FalseVerifyCode_Test()
        {
            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
            mockBenutzerService.Setup(x => x.VerifyPasswordChange(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            var newMockBenutzerService = mockBenutzerService.Object;
            var accountController = new AccountController(MockLoginService, newMockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            FakeHttpContext.SetFakeContext(accountController, true);
            //Act
            ActionResult result = accountController.PasswordChange("TestID", "TESTVerify");
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordRequestComplete_HTTPGet_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.PasswordRequestComplete();
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordChangeComplete_HTTPGet_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.PasswordChangeComplete();
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void RegisterMailVerification_HTTPGet_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.RegisterMailVerificationNotComplete();
            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void PasswordChange_HTTPPost_VerifyOK_ModelStateOk_Test()
        {
            //Arrange

            //Act
            ActionResult result = AccountController.PasswordChange(Fixture.Build<ForgottenPasswordCreateNewPasswordViewModel>().Create(),"TestId","TestVerify");
            string routeAction = ((RedirectToRouteResult)result).RouteValues["Action"].ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PasswordChangeComplete", routeAction);
        }

        [Test]
        public void PasswordChange_HTTPPost_VerifyFalse_ModelStateOk_Test()
        {
            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
  
            mockBenutzerService.Setup(x => x.VerifyPasswordChange(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockBenutzerService.Setup(x => x.EditBenutzerPassword(It.IsAny<Benutzer>()));
            var newMockBenutzerService = mockBenutzerService.Object;
            var accountController = new AccountController(MockLoginService, newMockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            FakeHttpContext.SetFakeContext(accountController, true);

            //Act
            ActionResult result = accountController.PasswordChange(Fixture.Build<ForgottenPasswordCreateNewPasswordViewModel>().Create(), "TestId", "TestVerify");
            string routeAction = ((RedirectToRouteResult)result).RouteValues["Action"].ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("~/Views/Shared/Error.cshtml", routeAction);

        }

        [Test]
        public void PasswordChange_HTTPPost_VerifyOk_ModelStateFalse_Test()
        {
            //Arrange
            var accountController = new AccountController(MockLoginService, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ModelState.AddModelError("fakeError", "fakeError");

            //Act
            ActionResult result = accountController.PasswordChange(new ForgottenPasswordCreateNewPasswordViewModel(), "TestId", "TestVerify");

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Register_HTTPPost_EMailCheckOK_ModelStateOk_Test()
        {
            //Arrange
            
            //Act
            ActionResult result = AccountController.Register(Fixture.Build<RegisterBenutzerViewModel>().Create());
            string routeAction = ((RedirectToRouteResult)result).RouteValues["Action"].ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("RegisterSuccsessfull", routeAction);

        }


        [Test]
        public void Register_HTTPPost_EMailCheckOK_ModelStateFalse_Test()
        {
            //Arrange
            var accountController = new AccountController(MockLoginService, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ModelState.AddModelError("fakeError", "fakeError");

            //Act
            ActionResult result = accountController.Register(Fixture.Build<RegisterBenutzerViewModel>().Create());


            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(((ViewResult)result).Model);
        }

        [Test]
        public void Register_HTTPPost_EMailCheckFalse_ModelStateOk_Test()
        {
            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
            mockBenutzerService.Setup(x => x.CheckEmailForRegistration(It.IsAny<string>())).Returns(false);
            var newMockBenutzerService = mockBenutzerService.Object;
            var accountController = new AccountController(MockLoginService, newMockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();

            //Act
            ActionResult result = accountController.Register(Fixture.Build<RegisterBenutzerViewModel>().Create());

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);
        }



        [Test]
        public void LoginVorgang_LoginSuccessLevel_Erfolgreich_ModelStateOK_Test()
        {
            //Arrange
            
            //Act
            ActionResult result = AccountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");
            string routeController = ((RedirectToRouteResult)result).RouteValues["Controller"].ToString();
            string routeAction = ((RedirectToRouteResult)result).RouteValues["Action"].ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home", routeController);
            Assert.AreEqual("Index", routeAction);

        }


        [Test]
        public void LoginVorgang_LoginSuccessLevel_Erfolgreich_ModelStateFalse_Test()
        {
            //Arrange
            var accountController = new AccountController(MockLoginService, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ModelState.AddModelError("fakeError", "fakeError");

            //Act
            ActionResult result = AccountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);

        }


        [Test]
        public void LoginVorgang_LoginSuccessLevel_BenutzerNichtGefunden_ModelStateOK_Test()
        {
            //Arrange
            var newMockLoginService = new Mock<ILoginService>();
            newMockLoginService.Setup(x => x.AnmeldePrüfung(It.IsAny<string>(), It.IsAny<string>())).Returns(LoginSuccessLevel.BenutzerNichtGefunden);
            var accountController = new AccountController(newMockLoginService.Object, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();

            //Act
            ActionResult result = accountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");
          
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);
        }

        [Test]
        public void LoginVorgang_LoginSuccessLevel_PasswortFalsch_ModelStateOK_Test()
        {
            //Arrange
            var newMockLoginService = new Mock<ILoginService>();
            newMockLoginService.Setup(x => x.AnmeldePrüfung(It.IsAny<string>(), It.IsAny<string>())).Returns(LoginSuccessLevel.PasswortFalsch);
            var accountController = new AccountController(newMockLoginService.Object, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            
            //Act
            ActionResult result = accountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);
        }

        [Test]
        public void LoginVorgang_LoginSuccessLevel_DatenbankFehler_ModelStateOK_Test()
        {
            //Arrange
            var newMockLoginService = new Mock<ILoginService>();
            newMockLoginService.Setup(x => x.AnmeldePrüfung(It.IsAny<string>(), It.IsAny<string>())).Returns(LoginSuccessLevel.DatenbankFehler);
            var accountController = new AccountController(newMockLoginService.Object, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            

            //Act
            ActionResult result = accountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);
        }

        [Test]
        public void LoginVorgang_LoginSuccessLevel_Unbekannt_ModelStateOK_Test()
        {
            //Arrange
            var newMockLoginService = new Mock<ILoginService>();
            newMockLoginService.Setup(x => x.AnmeldePrüfung(It.IsAny<string>(), It.IsAny<string>())).Returns(LoginSuccessLevel.Unbekannt);
            var accountController = new AccountController(newMockLoginService.Object, MockBenutzerService, MockBenutzerViewModelService);
            accountController.ControllerContext = new ControllerContext();
            

            //Act
            ActionResult result = accountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, accountController.ModelState.IsValid);
        }

        [Test]
        public void LoginVorgang_LoginSuccessLevel_Erfolgreich_ModelStateOK_EmailNotVirifyed_Test()
        {
            //Arrange
            var mockBenutzerService = new Mock<IBenutzerService>();
            mockBenutzerService.Setup(x => x.SearchUserByEmail(It.IsAny<string>())).Returns(Fixture.Build<Benutzer>().With(x => x.IstEmailVerifiziert, false).Create());
            var newMockBenutzerService = mockBenutzerService.Object;
            var accountController = new AccountController(MockLoginService, newMockBenutzerService, MockBenutzerViewModelService);

            //Act
            ActionResult result = accountController.Login(Fixture.Build<LoginModel>().Create(), "Home/Index");
            string routeAction = ((RedirectToRouteResult)result).RouteValues["Action"].ToString();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("RegisterMailVerificationNotComplete", routeAction);

        }
    }
}
