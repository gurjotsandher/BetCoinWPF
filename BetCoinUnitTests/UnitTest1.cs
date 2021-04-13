using System;
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
        public void TestGameCrashing()
        {
            User user = new User();
            Game game = new Game(user);
            Assert.AreEqual(game.multiplierTextProperty, "Crashed");
        }

        [TestMethod]
        public void TestLogin()
        {
            User user = new User(50, "myBank", "Default", "3233", "123", "testMEthod");
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void TestBalanceDecrease()
        {
            User user = new User();
            Game game = new Game(user);
            game.betTextBoxProperty = "50";
            Assert.AreEqual(user.Balance, int.Parse(game.betTextBoxProperty) - 50);
        }

        [TestMethod]
        public void TestBalanceIncrease()
        {
            User user = new User();
            Game game = new Game(user);
            game.multiplierTextProperty = "2.3";
            game.betTextBoxProperty = "50";
            if (game.dt.Interval.TotalSeconds == 2.2)
            {
                game.dt.Stop();
            }
            double winnings = Double.Parse(game.multiplierTextProperty) *
                int.Parse(game.betTextBoxProperty);
            Assert.AreEqual(user.Balance, 150 + winnings);
        }

        [TestMethod]
        public void TestMultiplierIncrease()
        {
            User user = new User();
            Game game = new Game(user);
            game.dt.Start();
            Assert.IsTrue(game.dt.Interval.TotalSeconds > 0);
        }

        [TestMethod]
        public void TestCashoutSameTimeAsCrash()
        {
            User user = new User();
            Game game = new Game(user);
            game.multiplierTextProperty = "2.3";
            game.betTextBoxProperty = "50";
            if (game.dt.Interval.TotalSeconds == 2.3)
            {
                game.dt.Stop();
            }
            Assert.AreEqual(user.Balance, 100);
        }

        [TestMethod]
        public void TestUserBeginsWith100()
        {
            User user = new User();
            Assert.AreEqual(user.Balance, 100);
        }

        [TestMethod]
        public void TestUserPlacesBetHigherThanBalance()
        {
            User user = new User();
            Game game = new Game(user);
            game.betTextBoxProperty = "150";
            Assert.AreEqual(user.Balance, 100);
        }

        [TestMethod]
        public void TestEmptyLogin()
        {
            Login login = new Login();
            Assert.IsTrue(login.textBoxEmailProperty.Equals("") &&
                login.textBoxPasswordProperty.Equals(""));
        }
    }
}
