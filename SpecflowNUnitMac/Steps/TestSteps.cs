using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace SpecflowNUnitMac
{
    [Binding]
    public class TestSteps
    {
        IWebDriver driver;

        [Given(@"Open URL ""(.*)""")]
        public void GivenOpenURL(string url)
        {
            driver = new ChromeDriver();
            driver.Url = url;
            
        }

        [Then(@"Search for ""(.*)""")]
        public void GivenSearchFor(string searchQuery)
        {
            IWebElement gInput = driver.FindElement(By.XPath("//input[@type='text']"));
            gInput.SendKeys(searchQuery);
            IWebElement searchButton = driver.FindElements(By.XPath("//input[@value='Google Search']"))[0];
            searchButton.Click();
        }

        [Then(@"Check if Results page is displayed")]
        public void ThenCheckIfResultsPageIsDisplayed()
        {
            IWebElement resultIndex = driver.FindElement(By.XPath("//div[@id='resultStats' and contains(text(),'results')]"),10);
            Assert.AreEqual(true, resultIndex.Displayed && resultIndex.Enabled, "Results page is not loaded...");
        }

        [Then(@"Take Screenshot")]
        public void TakeScreenshot()
        {
            //driver.TakeScreenshot().SaveAsFile(Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../"))+"/Screenshots/screenshot.png", ScreenshotImageFormat.Png);
        }

        [Then(@"Search for ""(.*)"" again")]
        public void SearchForAgain(string searchQuery)
        {
            IWebElement gInput = driver.FindElement(By.XPath("//input[@type='text' and @title='Search']"));
            gInput.Clear();
            gInput.SendKeys(searchQuery);
            IWebElement searchButton = driver.FindElements(By.XPath("//button[@aria-label='Google Search']"))[0];
            searchButton.Click();
        }

        [Then(@"End the Test")]
        public void EndTheTest()
        {
            driver.Close();
        }

        [Then(@"From ""(.*)"" To ""(.*)""")]
        public void FromTo(string from, string to)
        {
            IWebElement fromEle = driver.FindElement(By.XPath("//input[@type='text' and @id='package-origin-hp-package']"));
            fromEle.SendKeys(from);
            fromEle.SendKeys(Keys.Tab);
            IWebElement toEle = driver.FindElement(By.XPath("//input[@type='text' and @id='package-destination-hp-package']"));
            toEle.SendKeys(to);
            toEle.SendKeys(Keys.Tab);
        }

        [Then(@"Set the dates for Journey")]
        public void SetTheDatesForJourney()
        {
            DateTime now = DateTime.Now;
            now.AddDays(10);
            string fromString = now.ToString("MM/dd/yyyy");
            now.AddDays(30);
            string toString = now.ToString("MM/dd/yyyy");
            IWebElement fromEle = driver.FindElement(By.XPath("//input[@type='text' and @id='package-departing-hp-package']"));
            fromEle.SendKeys(fromString);
            fromEle.SendKeys(Keys.Tab);
            IWebElement toEle = driver.FindElement(By.XPath("//input[@type='text' and @id='package-returning-hp-package']"));
            toEle.SendKeys(toString);
            fromEle.SendKeys(Keys.Tab);
        }

        [Then(@"Set Travelers Adult (.*) And Children (.*)")]
        public void SetTravelers(int adults, int children)
        {
            IWebElement travelersButton = driver.FindElement(By.XPath("//button[@alt='Travelers']"));
            travelersButton.Click();
            IWebElement adultButton = driver.FindElement(By.XPath("//div[contains(@class,'traveler-selector-room-data')]/div[contains(@class,'gcw-component-initialized')]/div/button[contains(@class,'uitk-step-input-plus')]"));
            IWebElement childButton = driver.FindElement(By.XPath("//div[contains(@class,'traveler-selector-room-data')]/div[@class='children-wrapper']/div[contains(@class,'gcw-component-initialized')]/div/button[contains(@class,'uitk-step-input-plus')]"));
            while (adults > 1)
            {
                adultButton.Click();
                adults = adults - 1;
            }
            while (children > 0)
            {
                childButton.Click();
                children = children - 1;
            }
            IWebElement childAge = driver.FindElement(By.XPath("//select[contains(@class,'gcw-child-age-1-1-hc')]"));
            var selectElement = new SelectElement(childAge);
            selectElement.SelectByText("3");
            travelersButton.Click();
        }

        [Then(@"Search for Results")]
        public void SearchForResults()
        {
            IWebElement search = driver.FindElement(By.XPath("//button[@id='search-button-hp-package']"));
            search.Click();
            IWebElement resultsPage = driver.FindElement(By.XPath("//h1[@class='section-header-main' and contains(text(),'Start by choosing your hotel')]"), 30);
        }

    }
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }
    }
}
