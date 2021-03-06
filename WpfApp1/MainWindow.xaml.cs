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
            Parser ps = new Parser();
            ps.SetMainUrl("https://readmanga.live/");
            ps.SetQuery("Наруто");
            Manga mg = new Manga();
            var options = new EdgeOptions();
            options.UseChromium = true;
            using (IWebDriver driver = new EdgeDriver(options))
            {
                mg.ParserInit(driver, ps);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                for (int i = 0; i < mg.GetUrlVolume(ps); i++)
                {
                    ps.SetMainUrl("https://readmanga.live/");
                    mg.Parse(ps.GetMangaUrl()[i], driver, ps);

                }
            }
            
        }
    }
}
