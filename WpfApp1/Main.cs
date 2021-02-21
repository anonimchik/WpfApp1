using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.Support.UI;

namespace WpfApp1
{
    class Main
    {
        public void main()
        {
            /*
            using (IWebDriver driver = new InternetExplorerDriver())
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                driver.Navigate().GoToUrl("https://readmanga.live/search");
                driver.FindElement(By.Name("q")).SendKeys("наруто" + Keys.Enter);
                wait.Until(WebDriver => WebDriver.FindElement(By.Id("mangaResults")).Displayed);
                IWebElement firstResult = driver.FindElement(By.ClassName("tile col-sm-6  el_2"));
                Console.WriteLine(firstResult.GetAttribute("textContent")+"asd");
            */

            var options = new Microsoft.Edge.SeleniumTools.EdgeOptions();
            options.UseChromium = true;
            using (IWebDriver driver = new Microsoft.Edge.SeleniumTools.EdgeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                driver.Navigate().GoToUrl("https://readmanga.live/search");
                driver.FindElement(By.Name("q")).SendKeys("наруто" + Keys.Enter);
                wait.Until(WebDriver => WebDriver.FindElement(By.Id("mangaResults")).Displayed);
                IReadOnlyCollection<IWebElement> firstResult = driver.FindElements(By.ClassName("tile"));
            }


        }

        }
}
