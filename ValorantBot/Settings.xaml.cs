using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Settings : Window
    {
        public static int width = 1920;//переменная с шириной экрана
        public static int height = 1080;//переменная с высотой экрана
        public static bool OnlyAccept = false;//переменная для определения только принять игру или нет

        public Settings()
        {
            InitializeComponent();
            resolution1.Text =Convert.ToString(width);//присвоить техтблоку значение ширины экрана
            resolution2.Text = Convert.ToString(height);//присвоить техтблоку значение высоты экрана
            Setting1Check.IsChecked = OnlyAccept;//присвоить чекбоксу значение переменной 
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши сохранить 
        {
            width = Convert.ToInt32(resolution1.Text);//сохранить в переменную ширину
            height= Convert.ToInt32(resolution2.Text);//сохранить в переменную высоту
            OnlyAccept = Setting1Check.IsChecked==true ? true : false;//сохранить в переменную чекбокс
            StreamWriter sw = new StreamWriter("settings.cfg", false);//запись новых параметров в файл с настройками
            sw.WriteLine(width);
            sw.WriteLine(height);
            sw.WriteLine(OnlyAccept);
            sw.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши назад 
        {
            MainWindow Main = new MainWindow();//создаем окно стартовое
            Main.Show();//показать его
            this.Close();//закрыть старое
        }
    }
}
