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
    class VeeamPageObject : IPageObject
    {
        IWebDriver _driver;

        public VeeamPageObject(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement GetCountryDropDownElement()
        {
            return _driver.FindElement(By.Id("country-element"));
        }

        public IWebElement GetCountryElement(string country)
        {
            var countryElement = $"//div[@class='scroller-content']//span[@data-value='{country}']";
            return _driver.FindElement(By.XPath(countryElement));
        }

        public IWebElement GetLanguageDropDownElement()
        {
            return _driver.FindElement(By.XPath("//div[@class='selecter-fieldset selecter']//span[@class='selecter-selected' and contains(text(), 'All languages')]"));
        }

        public IWebElement GetLanguageElement(string language)
        {
            var languageElement = $"//div[@class='row']//fieldset[@class='col-xs-12']//label[@for='{language}']";
            return _driver.FindElement(By.XPath(languageElement));
        }

        public IWebElement GetLanguageSubmitElement()
        {
            return _driver.FindElement(By.XPath("//a[@class='selecter-fieldset-submit' and contains(text(), 'Apply')]"));
        }
        
        public IWebElement GetSelectedCountryElement()
        {
            return _driver.FindElement(By.XPath("/html/body/div[1]/article/section[2]/div/div/div[2]/dd/div/span"));
            
        }

        public IWebElement GetSelectedLanguageElement()
        {
            return _driver.FindElement(By.XPath("//div[@data-selected-title='Selected languages']/span[@class='selecter-selected']"));
        }

        public IWebElement GetVacanciesElement()
        {
            return _driver.FindElement(By.XPath("/html/body/div[1]/article/section[3]/div/div[1]/div[1]/h3"));
        }
    }
}
