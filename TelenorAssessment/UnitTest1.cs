using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace TelenorAssessment
{
    public class Tests
    {
        private IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver("C:/Users/Altaf/Videos/Downloads/chromedriver_win32/");
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.telenor.se/");
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(3000);
       
        }

        [Test]
        public void Test1()
        {
            String expectedTitle = "Checkout order";
            driver.FindElement(By.XPath("//button[contains(text(),'Godk�nn cookies')]")).Click();
            driver.FindElement(By.LinkText("Handla")).Click();
            driver.FindElement(By.XPath("//a[contains(text(),'Mobiltelefoner')]")).Click();
            driver.FindElement(By.XPath("//h2[text()='iPhone 13 Pro Max']")).Click();
            driver.FindElement(By.XPath("//h3[text()='75 GB']")).Click();
            driver.FindElement(By.XPath("//h3[contains(text(),'1 person')]")).Click();
            driver.FindElement(By.XPath("//span[text()='Nytt nummer']")).Click();

            IWebElement ele = driver.FindElement(By.XPath("//button[contains(text(),'V�lj och g� vidare')]"));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));

            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver; 
            //js.ExecuteScript("arguments[0].click();", ele);

            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromMinutes(2);


            driver.FindElement(By.XPath("//button[contains(text(),'V�lj och g� vidare')]")).Click();
            driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(30);

            String actualTitle = driver.Title;
            Assert.AreEqual(actualTitle, expectedTitle);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}