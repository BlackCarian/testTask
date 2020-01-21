using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    class CareerVeeamTests
    {
        IWebDriver driver = new ChromeDriver(@"E:\ChromeDriver");
        IPageObject pageObject;
        CareersVeeamSteps steps;

        [SetUp]
        public void SetupTest()
        {
            pageObject = new VeeamPageObject(driver);
            steps = new CareersVeeamSteps(pageObject, driver);
            driver.Navigate().GoToUrl(@"https://careers.veeam.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void CheckVacanciesByCountryAndLanguage()
        {
            steps.ChooseCountry("Romania");
            steps.ChooseLanguage("English");
            steps.CheckVacanciesCount(32);
        }

        [TearDown]
        public void TearDownTest()
        {
            driver.Quit();
        }
    }
}
