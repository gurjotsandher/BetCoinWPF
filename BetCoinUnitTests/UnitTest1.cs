using Microsoft.VisualStudio.TestTools.UnitTesting;
using BetCoinWpf;

namespace BetCoinUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRegistrationIsEmpty()
        {
            Registration reg = new Registration();
            Assert.IsTrue(reg.textBoxEmailProperty.Equals("") &&
                reg.passwordBoxProperty.Equals("") && reg.textBoxIdProperty.Equals(""));
        }

        [TestMethod]
        public void TestLogin()
        {
            User user = new User(50, "myBank", "Default", "3233", "123", "testMEthod");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestUserBeginsWith0()
        {
            User user = new User();
            Assert.AreEqual(user.Balance, 0);
        }

        [TestMethod]
        public void TestEmptyLogin()
        {
            Login login = new Login();
            Assert.IsTrue(login.textBoxEmailProperty.Equals("") &&
                login.textBoxPasswordProperty.Equals(""));
        }

        [TestMethod]
        public void TestBalanceGreaterThan0()
        {
            User user = new User(50, "myBank", "Default", "3233", "123", "testMethod");
            Assert.AreEqual(user.Balance, 50);
        }
    }
}
