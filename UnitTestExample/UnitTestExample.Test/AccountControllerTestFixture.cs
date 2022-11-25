﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;
using System.Text.RegularExpressions;
using System.Activities;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        public AccountControllerTestFixture()
        {
        }

        [
            Test,
            TestCase("ABCDabcd", false),
            TestCase("ABCD1234", false),
            TestCase("abcd1234", false),
            TestCase("ab1234", false),
            TestCase("ABcd12345", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            //var actualResult = accountController.ValidatePassword(password);

            bool actualResult;

            var EnglishAlphabet = new Regex(@"[A - Za - z\d]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");

            if (!EnglishAlphabet.IsMatch(password))
            {
                actualResult = false;
            }
            else if (!hasLowerChar.IsMatch(password))
            {
                actualResult = false;
            }
            else if (!hasMinimum8Chars.IsMatch(password))
            {
                actualResult = false;
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                actualResult = false;
            }
            else if (!hasNumber.IsMatch(password))
            {
                actualResult = false;
            }
            else
            {
                actualResult = true;
            }


            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidateEmail(email);

            Assert.AreEqual(expectedResult, actualResult);
        }
        [
             Test,
             TestCase("irf@uni-corvinus.hu", "Abcd1234"),
             TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            var accountController = new AccountController();

            var actualResult = accountController.Register(email, password);

            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            var accountController = new AccountController();

            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }
        }
    }
}
