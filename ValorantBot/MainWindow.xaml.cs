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
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.IO;

namespace ValorantBot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (File.Exists("settings.cfg"))
            {//запись настроек в cfg файл и выгрузка настроек в переменные
                TextReader tr = new StreamReader("settings.cfg");
                Settings.width = Convert.ToInt32(tr.ReadLine());
                Settings.height = Convert.ToInt32(tr.ReadLine());
                Settings.OnlyAccept = Convert.ToBoolean(tr.ReadLine());
                tr.Close();
            }
            else
            {
                TextWriter tw = new StreamWriter("settings.cfg");
                tw.WriteLine(Settings.width);
                tw.WriteLine(Settings.height);
                tw.WriteLine(Settings.OnlyAccept);
                tw.Close();
            }
        }

        private void HelperButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия кнопки автоацепт
        {
            Window1 settings = new Window1();//переход на новое окно
            settings.Show();
            this.Close();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши settings 
        {
            Settings settings = new Settings();//создаем окно Настройки
            settings.Show();//показать его
            this.Close();//закрыть старое
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия кнопки выйти из приложения
        {
            Application.Current.Shutdown();//сам выход из приложухи
        }
    }
}
