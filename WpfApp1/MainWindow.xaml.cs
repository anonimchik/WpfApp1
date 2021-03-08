using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenQA.Selenium;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium.Support.UI;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Parser ps = new Parser(); //создание объекта класса
            ps.SetMainUrl("https://readmanga.live/"); //ссылка
            ps.SetQuery("Наруто"); //запрос
            Manga mg = new Manga(); //создание объекта класса
            var options = new EdgeOptions(); //задание опции для edge
            options.UseChromium = true; //активация хромиума
            using (IWebDriver driver = new EdgeDriver(options)) //запуск дравйвера edge
            {
                List<Manga> MangaList = new List<Manga>(); //создание списка
                mg.ParserInit(driver, ps); //первичная инициализация
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10)); //задание задержки
                for (int i = 0; i < mg.GetUrlVolume(ps); i++) //парсинг данных
                {
                    Manga mng = new Manga(); //обнуление объекта
                    MangaList.Add(mng); //запись данных класса в список
                    ps.SetMainUrl("https://readmanga.live/"); //ввод ссылки
                    mng.ParseMainInfo(ps.GetMangaUrl()[i], driver, ps); //парсинг данных
                    foreach (var chapter in mng.Chapters)
                    {
                        mng.parseImages(chapter, driver);
                    }
                }
       
                
            }
            
        }
    }
}
