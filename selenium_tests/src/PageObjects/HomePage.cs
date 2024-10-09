using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using Bogus;
using selenium_tests.models;

namespace selenium_tests.PageObjects
{
    class HomePage
    {
        string url= "https://demo.playwright.dev/todomvc/#/";
    

      private IWebDriver driver;
      private WebDriverWait wait;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName , Using = "new-todo")]
        [CacheLookup]
        private IWebElement lblInsertToDo;

        [FindsBy(How = How.Id , Using = "toggle-all")]
        [CacheLookup]
        private IWebElement btnSelectAll;

        [FindsBy(How = How.XPath , Using = "/html/body/section/div/section/ul/li[1]/div/input")]
        [CacheLookup]
        private IWebElement btnCompleteFirstItem;
        
        [FindsBy(How = How.ClassName , Using = "clear-completed")]
        [CacheLookup]
        private IWebElement btnclearCompleted;

        [FindsBy(How = How.XPath , Using = "/html/body/section/div/section/ul/li[1]/div/button")]
        [CacheLookup]
        private IWebElement btnDelete;

        [FindsBy(How = How.ClassName , Using = "todo-count")]
        [CacheLookup]
        private IWebElement lblCount;

        [FindsBy(How = How.XPath , Using = "/html/body/section/div/footer/ul/li[1]/a")]
        [CacheLookup]
        private IWebElement btnAll;

        
        public void goToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public HomePage clickAllItems(){
            btnAll.Click();
            return new HomePage(driver);
        }

        public HomePage deleteFirstItem(){
            btnDelete.Click();
            return new HomePage(driver);
        }

        public string getItemCount(){
            return lblCount.GetAttribute("innerText");
        }

        public HomePage selectAll(){
            btnSelectAll.Click();
            return new HomePage(driver);
        }

        public HomePage completeAll(){
            btnclearCompleted.Click();
            return new HomePage(driver);
        }

        public HomePage completeFirstItem(){
            btnCompleteFirstItem.Click();
            return new HomePage(driver);
        }

        public HomePage insertItem(string listItem){
            lblInsertToDo.SendKeys(listItem);   
            lblInsertToDo.SendKeys(Keys.Enter);
            return new HomePage(driver);
        }

        public void createFakeItem(){
            var clientFaker = new Faker<ClientModel>()
            .RuleFor(x => x.Id, Guid.NewGuid)
            .RuleFor(x => x.listItem, x => x.Lorem.Sentence(1));
            var clientGenerator = clientFaker.Generate();
            insertItem(clientGenerator.listItem);
        }

        public void createDuplicateFakeItem(){
            var clientFaker = new Faker<ClientModel>()
            .RuleFor(x => x.Id, Guid.NewGuid)
            .RuleFor(x => x.listItem, x => x.Lorem.Sentence(1));
            var clientGenerator = clientFaker.Generate();
            insertItem(clientGenerator.listItem);
            insertItem(clientGenerator.listItem);
        }
    
    }
}