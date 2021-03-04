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
        public static List<String> MangaUrl = new List<String>();
        public string Title { set; get; }
        public string BackgroundImg { set; get; }
        public string Description { set; get; }
        public int NumberVolumes { set; get; }
        public string TranslateStatus { set; get; }
        public string Author { set; get; }
        public int ReleaseYear { set; get; }
        public static List<String> Genres = new List<string>();
        public ICollection<IWebElement> PageUrl { set; get; }
        public void Parse()
        {

            var options = new EdgeOptions();
            options.UseChromium = true;
            using (IWebDriver driver = new EdgeDriver(options))
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                driver.Navigate().GoToUrl(Url + "search");
                driver.FindElement(By.Name("q")).SendKeys("наруто" + Keys.Enter);
                wait.Until(WebDriver => WebDriver.FindElement(By.Id("mangaResults")).Displayed);
                ICollection<IWebElement> Mangaurl = driver.FindElements(By.XPath("//a[@class='non-hover']"));
                foreach (var mgurl in Mangaurl)
                {
                    MangaUrl.Add(mgurl.GetAttribute("href"));
                }
            }

            using (IWebDriver drv = new EdgeDriver(options))
            {
                WebDriverWait Wait = new WebDriverWait(drv, TimeSpan.FromSeconds(30));
                foreach (var item in MangaUrl)
                {
                    drv.Navigate().GoToUrl(item); //переход на страницу с конкретной мангой
                    BackgroundImg = drv.FindElements(By.XPath(@"//img[@class='fotorama__img']"))[0].GetAttribute("src"); //получение задней картины
                    NumberVolumes = int.Parse(Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[1]")).Text, @"Томов: ", "")); //получение кол-ва глав
                    TranslateStatus = Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[2]")).Text, @"Перевод: ", ""); //получение статуса перевода
                    ReleaseYear = int.Parse(drv.FindElement(By.XPath(@"//span[@class='elem_year ']/a")).Text); //получение года выпуска
                    ICollection<IWebElement> genres = drv.FindElements(By.XPath(@"//span[@class='elem_genre ']/a")); //подготовка данных к заненсению в список Genres
                    foreach  (var gnrs in genres) //запись данных в список Genres
                    {
                        Genres.Add(gnrs.Text);
                    }
                    Author = drv.FindElement(By.XPath(@"//span[@class='elem_author ']/a")).Text; //получение автора
                    Description = drv.FindElement(By.XPath(@"//div[@class='manga-description']")).Text; //получение описания
                }
            }

        }

    }
}
