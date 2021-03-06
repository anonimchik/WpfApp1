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
    class Manga
    {
        public string Title { set; get; }
        public string BackgroundImg { set; get; }
        public string Description { set; get; }
        public int NumberVolumes { set; get; }
        public string TranslateStatus { set; get; }
        public string Author { set; get; }
        public List<String> Chapters = new List<String>();
        public int ReleaseYear { set; get; }

        public static List<String> Genres = new List<string>();

        //public List<Manga> manga = new List<Manga>();
        
        public void ParserInit(IWebDriver driver, Parser ps)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            driver.Navigate().GoToUrl(ps.GetMainUrl() + "search");
            driver.FindElement(By.Name("q")).SendKeys(ps.GetQuery() + Keys.Enter);
            wait.Until(WebDriver => WebDriver.FindElement(By.Id("mangaResults")).Displayed);
            ICollection<IWebElement> Mangaurl = driver.FindElements(By.XPath("//a[@class='non-hover']"));
            foreach (var mgurl in Mangaurl)
            {
                ps.GetMangaUrl().Add(mgurl.GetAttribute("href"));
            }
        }
        public int GetUrlVolume(Parser ps)
        {
            return (int)ps.GetMangaUrl().Count;
        }
        public void Parse(string url, IWebDriver drv, Parser ps)
        {
            List<Manga> manga = new List<Manga>(ps.GetMangaUrl().Count);
            for (int i = 0; i < ps.GetMangaUrl().Count; i++)
            {
                ps.setManga();
                WebDriverWait Wait = new WebDriverWait(drv, TimeSpan.FromSeconds(30));
                drv.Navigate().GoToUrl(url); //переход на страницу с конкретной мангой
                if (Regex.IsMatch(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']")).Text, "Информация о манге"))
                {
                    drv.FindElements(By.XPath(@"//img[@class='fotorama__img']"))[0].GetAttribute("src"); //получение задней картины
                    try
                    {
                        manga[i].NumberVolumes = int.Parse(Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[1]")).Text, @"Томов: ", "")); //получение кол-ва глав
                    }
                    catch (Exception e) { }

                    try
                    {
                        manga[i].TranslateStatus = Regex.Replace(drv.FindElement(By.XPath(@"//div[@class='subject-meta col-sm-7']/p[2]")).Text, @"Перевод: ", ""); //получение статуса перевода
                    }
                    catch (Exception e) { }

                    try
                    {
                        manga[i].ReleaseYear = int.Parse(drv.FindElement(By.XPath(@"//span[@class='elem_year ']/a")).Text); //получение года выпуска
                    }
                    catch (Exception e) { }

                    ICollection<IWebElement> genres = drv.FindElements(By.XPath(@"//span[@class='elem_genre ']/a")); //подготовка данных к заненсению в список Genres
                    foreach (var gnrs in genres) //запись данных в список Genres
                    {
                        Genres.Add(gnrs.Text);
                    }
                    try
                    {
                        Author = drv.FindElement(By.XPath(@"//span[@class='elem_author ']/a")).Text; //получение автора
                    }
                    catch (Exception e) { }

                    try
                    {
                        Description = drv.FindElement(By.XPath(@"//div[@class='manga-description']")).Text; //получение описания
                    }
                    catch (Exception e) { }

                    ICollection<IWebElement> chapter = drv.FindElements(By.XPath(@"//a[@class='cp-l']")); //получение ссылок на главы
                    foreach (var ch in chapter)
                    {
                        Chapters.Add(ch.GetAttribute("href") + "|" + ch.Text);
                    }

                }
            }        
            
        }

    }
}
