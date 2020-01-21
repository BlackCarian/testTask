using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Selenium
{
    class CareersVeeamSteps
    {
        IPageObject _pageObject;

        public CareersVeeamSteps(IPageObject pageObject)
        {
            _pageObject = pageObject;
        }

        public void ChooseCountry(string country)
        {
            var countryDropDown = _pageObject.GetCountryDropDownElement();
            Assert.IsNotNull(countryDropDown);

            countryDropDown.Click();

            var chooseCountry = _pageObject.GetCountryElement(country);
            Assert.IsNotNull(chooseCountry);

            chooseCountry.Click();

            var selectedCountry = _pageObject.GetSelectedCountryElement().Text;
            bool isRightCountry = (selectedCountry == country);
            Assert.True(isRightCountry);
        }

        public void ChooseLanguage(string language)
        {
            string id = correlateLanguage(language);

            var languageDropDown = _pageObject.GetLanguageDropDownElement();
            Assert.IsNotNull(languageDropDown);

            languageDropDown.Click();

            var chooseLanguage = _pageObject.GetLanguageElement(id);
            Assert.IsNotNull(chooseLanguage);

            chooseLanguage.Click();

            var submitLanguage = _pageObject.GetLanguageSubmitElement();
            Assert.IsNotNull(submitLanguage);

            submitLanguage.Click();

            var selectedLanguage = _pageObject.GetSelectedLanguageElement().Text;
            bool isRightLanguage = (selectedLanguage == language);
            Assert.True(isRightLanguage);
        }

        public void CheckVacanciesCount(int expected)
        {
            Thread.Sleep(2000);
            var vacanciesResponse = _pageObject.GetVacanciesElement().Text;

            int vacanciesCount = getVacanciesCount(vacanciesResponse);

            Assert.AreEqual(expected, vacanciesCount);
        }

        private int getVacanciesCount(string response)
        {
            int result;

            string vc = !String.IsNullOrWhiteSpace(response) && response.Length >= 2
                        ? response.Substring(0, 2)
                        : response;

            Int32.TryParse(vc, out result);

            return result;
        }

        private string correlateLanguage(string language)
        {
            string id;
            Dictionary<string, string> languages = new Dictionary<string, string>();
            languages.Add("English", "ch-7");

            id = languages["English"];
            return id;
        }
    }
}
