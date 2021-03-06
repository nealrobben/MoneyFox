﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyFox.Shared.Interfaces;
using MoneyFox.Shared.Manager;
using MoneyFox.Shared.Model;
using Moq;
using MoneyFox.Shared.Repositories;

namespace MoneyFox.Shared.Tests.Manager
{
    [TestClass]
    public class EndMonthManagerTests
    {
   
       
        [TestMethod]
        public void EndofMonthManager_AccountIsNegative()
        {
            Account account1 = new Account();
            account1.Id = 1;
            account1.CurrentBalance = 100;
            account1.EndMonthWarning = "Should Not Show";

            var paymentDataAccess = new Mock<IDataAccess<Payment>>();
            paymentDataAccess.Setup(x => x.LoadList(null)).Returns(new List<Payment>
            {
                new Payment {Id = 10, ChargedAccountId=1, Amount=100,Date= DateTime.Now},
                new Payment {Id = 15, ChargedAccountId=1, Amount=100, Date= DateTime.Now}
            });

            var paymentrepository = new PaymentRepository(paymentDataAccess.Object);
            paymentrepository.Load();

            var accountDataAccess = new Mock<IDataAccess<Account>>();
            accountDataAccess.Setup(x => x.LoadList(null)).Returns(new List<Account>
            {
                new Account {Id=2, CurrentBalance=100}, 
                account1
            });
            var accountrepository = new AccountRepository(accountDataAccess.Object);

            EndOfMonthManager testManager = new EndOfMonthManager(paymentrepository, accountrepository);

            testManager.AssignToAccounts();
            Assert.AreEqual(account1.EndMonthWarning, "Negative at end of month");
        }
        [TestMethod]
        public void EndofMonthManager_AccountIsPositive()
        {
            Account account1 = new Account();
            account1.Id = 1;
            account1.CurrentBalance = -100;
            account1.EndMonthWarning = "Should Not Show";

            var paymentDataAccess = new Mock<IDataAccess<Payment>>();
            paymentDataAccess.Setup(x => x.LoadList(null)).Returns(new List<Payment>
            {
                new Payment {Id = 10, TargetAccountId=1, Amount=100,Date= DateTime.Now},
                new Payment {Id = 15, TargetAccountId=1, Amount=100, Date= DateTime.Now}
            });

            var paymentrepository = new PaymentRepository(paymentDataAccess.Object);
            paymentrepository.Load();

            var accountDataAccess = new Mock<IDataAccess<Account>>();
            accountDataAccess.Setup(x => x.LoadList(null)).Returns(new List<Account>
            {
                new Account {Id=2, CurrentBalance=100},
                account1
            });
            var accountrepository = new AccountRepository(accountDataAccess.Object);

            EndOfMonthManager testManager = new EndOfMonthManager(paymentrepository, accountrepository);

            testManager.AssignToAccounts();
            Assert.AreEqual(account1.EndMonthWarning, " ");
        }






    }
}
