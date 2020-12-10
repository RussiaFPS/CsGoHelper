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
using System.Windows.Shapes;

namespace ValorantBot
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши назад 
        {
            MainWindow Main = new MainWindow();//создаем окно стартовое
            Main.Show();//показать его
            this.Close();//закрыть старое
        }
    }
}
