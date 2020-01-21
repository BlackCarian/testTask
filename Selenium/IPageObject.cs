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
    interface IPageObject
    {
        IWebElement GetCountryDropDownElement();

        IWebElement GetCountryElement(string country);

        IWebElement GetLanguageDropDownElement();

        IWebElement GetLanguageElement(string language);

        IWebElement GetLanguageSubmitElement();

        IWebElement GetSelectedCountryElement();

        IWebElement GetSelectedLanguageElement();

        IWebElement GetVacanciesElement();
    }
}
