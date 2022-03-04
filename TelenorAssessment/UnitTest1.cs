using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Threading;

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
            driver.FindElement(By.XPath("//button[contains(text(),'Godkänn cookies')]")).Click();
            driver.FindElement(By.LinkText("Handla")).Click();
            driver.FindElement(By.XPath("//a[contains(text(),'Mobiltelefoner')]")).Click();
            driver.FindElement(By.XPath("//h2[text()='iPhone 13 Pro Max']")).Click();
            driver.FindElement(By.XPath("//h3[text()='75 GB']")).Click();
            driver.FindElement(By.XPath("//h3[contains(text(),'1 person')]")).Click();
            driver.FindElement(By.XPath("//span[text()='Nytt nummer']")).Click();

            IWebElement ele = driver.FindElement(By.XPath("//button[contains(text(),'Välj och gå vidare')]"));
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].checked = true;", ele);

            Actions build = new Actions(driver); // heare you state ActionBuider
            build.MoveToElement(ele).Build().Perform();

            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", ele);
            Thread.Sleep(5000);

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