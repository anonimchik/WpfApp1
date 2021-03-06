using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.Support.UI;

namespace WpfApp1
{
    class Parser
    {
        private string MainUrl;
        private List<String> MangaUrl = new List<String>();
        private ICollection<IWebElement> PageUrl;
        private string Query;
        public void SetMainUrl(string mainUrl)
        {
            MainUrl = mainUrl;
        }
        public string GetMainUrl()
        {
            return MainUrl;
        }
        public void SetMangaUrl(List<String> mangaUrl) 
        {
            MangaUrl = mangaUrl;
        }
        public List<String> GetMangaUrl()
        {
            return MangaUrl;
        }
        public void SetQuery(string query)
        {
            Query = query;
        }
        public string GetQuery()
        {
            return Query;
        }     
        public void setManga()
        {
           
        }
    }
}
