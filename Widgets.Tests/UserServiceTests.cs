using Highway.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Widgets.Data;

// Method Naming Convention = Method_Condition_Result
namespace Widgets.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestInitialize]
        public void Start()
        {
            TestSetup.DestroyDatabase();
            TestSetup.CreateDatabase();
        }

        [TestCleanup]
        public void End()
        {
            TestSetup.DestroyDatabase();
        }

        [TestMethod]
        public void Create_ValidUser_Success()
        {
            // Arrange
            var configuration = new WidgetsMappingConfiguration();
            var context = new DataContext("Widgets", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var email = "mockuser@widgets.com";

            // Act
            User user = userService.CreateUser(email);
            context.SaveChanges();

            // Assert
            Assert.IsTrue(user.UserId != 0);
            Assert.IsTrue(user.Email == email);
        }

        [TestMethod]
        public void Create_InvalidUser_Fail()
        {
            // Arrange
            var configuration = new WidgetsMappingConfiguration();
            var context = new DataContext("Widgets", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            var email = string.Empty;

            // Act
            User user = userService.CreateUser(email);
            context.SaveChanges();

            // Assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Get_User_Success()
        {
            // Arrange
            var email = "mockuser@widgets.com";
            InsertMockUser(email);
            var configuration = new WidgetsMappingConfiguration();
            var context = new DataContext("Widgets", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);

            // Act
            User user = userService.GetUserByEmail(email);

            // Assert
            Assert.AreEqual(user.Email, email);
        }

        [TestMethod]
        public void Get_InvalidUser_Fail()
        {
            // Arrange
            var emailAdded = "mockuser@widgets.com";
            var emailTested = "usernoexist@widgets.com";
            InsertMockUser(emailAdded);
            var configuration = new WidgetsMappingConfiguration();
            var context = new DataContext("Widgets", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);

            // Act
            User user = userService.GetUserByEmail(emailTested);

            // Assert
            Assert.IsNull(user);
        }

        private void InsertMockUser(string email)
        {
            var configuration = new WidgetsMappingConfiguration();
            var context = new DataContext("Widgets", configuration);
            var repository = new Repository(context);
            var userService = new UserService(repository);
            User user = userService.CreateUser(email);
            context.SaveChanges();
        }
    }
}
