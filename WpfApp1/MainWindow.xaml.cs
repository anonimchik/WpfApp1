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
            Parser ps1 = new Parser();
            ps1.Url = @"https://readmanga.live/";
            ps1.ParserInit();
            for (int i = 0; i < ps1.GetUrlVolume(); i++)
            {
                Parser ps = new Parser();
                ps.Url = @"https://readmanga.live/";
                ps.Parse();

            }
            
        }
    }
}
