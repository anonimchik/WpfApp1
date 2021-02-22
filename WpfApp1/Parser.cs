using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    class Parser
    {
        public string Url { set; get; }
        public static ICollection<IWebElement> MangaUrl { set; get; }
        public string Title { set; get; }
        public string BackgroundImg { set; get; }
        public string Description { set; get; }
        public int NumberVolumes { set; get; }
        public string TranslateStatus { set; get; }
        public string Author { set; get; }
        public int ReleaseYear { set; get; }
        public ICollection<IWebElement> Genres { set; get; }
        public ICollection<IWebElement> PageUrl { set; get; }
        public void Parse()
        {

            var options = new EdgeOptions();
            options.UseChromium = true;
            using (IWebDriver driver = new EdgeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                driver.Navigate().GoToUrl(Url + "search");
                driver.FindElement(By.Name("q")).SendKeys("наруто" + Keys.Enter);
                wait.Until(WebDriver => WebDriver.FindElement(By.Id("mangaResults")).Displayed);
                MangaUrl = driver.FindElements(By.XPath("//a[@class='non-hover']"));

                IWebDriver drv = new EdgeDriver(options);
                foreach (var item in MangaUrl)
                {
                    drv.Navigate().GoToUrl(item.GetAttribute("href"));
                    BackgroundImg=drv.FindElements(By.XPath(@"//img[@class='fotorama__img']"))[0].GetAttribute("src");
                    NumberVolumes=int.Parse(Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[1]")).Text, @"Томов: ", ""));
                    TranslateStatus=Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[2]")).Text, @"Перевод: ", "");
                    ReleaseYear=int.Parse(drv.FindElement(By.XPath(@"//span[@class='elem_year ']/a")).Text);
                    Genres=drv.FindElements(By.XPath(@"//span[@class='elem_genre ']/a"));  //доделать
                    Author=drv.FindElement(By.XPath(@"//span[@class='elem_author ']/a")).Text;
                    Description = drv.FindElement(By.XPath(@"//div[@class='manga-description']")).Text;
                }

            }

        }

    }
}
