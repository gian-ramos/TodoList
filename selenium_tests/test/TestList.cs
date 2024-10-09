using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using selenium_tests.PageObjects;
using System.Threading;
using System;
using System.Net;
using NUnit.Framework.Legacy;

namespace selenium_tests.test {
    public class TestList
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        //[Name("Clear all completed TodoItems")]
        [Category("ToDo")]
        public void TestinsertItemToList()
        {
            HomePage home_page = new HomePage(driver);
            home_page.goToPage();
            //Create 5 Items
            for (int i = 0; i < 5; i++){
            home_page.createFakeItem();
            }

            home_page.selectAll();

            // implicit wait added because... I want you to see the results.
            int milliseconds = 3000;
            Thread.Sleep(milliseconds);
            home_page.completeAll();
            Thread.Sleep(milliseconds);
        }

        [Test]
        //[Name("Insert Duplicate Items")]
        [Category("ToDo")]
        public void TestDuplicateItems(){
            HomePage home_page = new HomePage(driver);
            home_page.goToPage();
            home_page.createDuplicateFakeItem();
            
            IWebElement firstItem = driver.FindElement(By.XPath("/html/body/section/div/section/ul/li[1]/div/label"));
            string firstlabel = firstItem.GetAttribute("innerText");

            IWebElement secondItem = driver.FindElement(By.XPath("/html/body/section/div/section/ul/li[2]/div/label"));
            string secondlabel = secondItem.GetAttribute("innerText");

            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
            ClassicAssert.AreEqual(firstlabel, secondlabel);
        }

        [Test]
        //[Name("TestCountNumber")]
        [Category("ToDo")]
        public void TestItemLeftAfterCompleteOne(){
            HomePage home_page = new HomePage(driver);
            home_page.goToPage();
            //Create 5 Items
            for (int i = 0; i < 5; i++){
            home_page.createFakeItem();
            }

            string count = home_page.getItemCount();
     
            //Validation that I have 5 Pending Items
            ClassicAssert.AreEqual(count, "5 items left");

            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
            //Mark as done First Item
            home_page.completeFirstItem();

            //Go to All Items
            home_page.clickAllItems();

            string count2 = home_page.getItemCount();
            ClassicAssert.AreEqual(count2, "4 items left");
        }

        [Test]
        //[Name("Delete Item and validate")]
        [Category("ToDo")]
        public void TestDeleteItems(){
            HomePage home_page = new HomePage(driver);
            home_page.goToPage();
            home_page.createDuplicateFakeItem();
            
            //Select First Item
            IWebElement firstItem = driver.FindElement(By.XPath("/html/body/section/div/section/ul/li[1]/div/label"));
            firstItem.Click();
            home_page.deleteFirstItem();
            string count = home_page.getItemCount();
            ClassicAssert.AreNotEqual(count, "2 items left");
        }

        [OneTimeTearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}   