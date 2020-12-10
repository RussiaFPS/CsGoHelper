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

namespace ValorantBot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши start начало программы
        {
           // this.Hide();//скрыть это форму
            Window1 helper = new Window1();//создаем окно с Помошником
            helper.Show();//показать окно с помошником
            this.Close();//закрыть старое окно
            Thread Th = new Thread(Keybor);//Создаем поток для ожидания нажатия клавиши с клавы
            Th.SetApartmentState(ApartmentState.STA);
            Th.Start();//запуск потока
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия кнопки выйти из приложения
        {
            Application.Current.Shutdown();//сам выход из приложухи
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши settings 
        {
            Settings settings = new Settings();//создаем окно Настройки
            settings.Show();//показать его
            this.Close();//закрыть старое
            /* this.Hide();
             Settings settings = new Settings();
             settings.Show();*/
        }
        void Keybor()//Новый поток для ожидания нажатия клавиши с клавы
        {
            Color ButtonAccept= Color.FromArgb(255,76,175,80);//создание цвета кнопки принять из csgo
            Color Buffer=Color.FromArgb(255, 255, 255, 255);//создание цвета буффера для сравнения цвета кнопки принять
            bool PressButton = true;
            while (PressButton)//само ожидание нажатие клавиши 
            {
                Thread.Sleep(40);
                if ((Keyboard.GetKeyStates(Key.F8) & KeyStates.Down) > 0)
                {
                    SetCursorPos(25, 100);//поиск кнопки старт в csgo
                    ClickMouse();
                    Thread.Sleep(500);
                    SetCursorPos(150, 200);//поиск кнопки соревновательного режима в csgo
                    Thread.Sleep(500);
                    ClickMouse();
                    SetCursorPos(1500, 1050);//поиск кнопки запуск в csgo
                    Thread.Sleep(500);
                    ClickMouse();
                    SetCursorPos(960, 610);//поиск кнопки прянять матч в csgo
                    while(Buffer != ButtonAccept){//ожидание появления кнопки принять
                        Thread.Sleep(40);
                        Buffer = GetColorAt(960, 610);
                    }
                    ClickMouse();
                    PressButton = false;
                }
            }
        }
        void ClickMouse()//эмулятор нажатия левой кнопки мыши
        {
            mouse_event(0x00000002, 0, 0, 0, 0);
            mouse_event(0x00000004, 0, 0, 0, 0);
        }

        public static Color GetColorAt(int x, int y)//получение цвета пикселя
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (byte)((a >> 0) & 0xff), (byte)((a >> 8) & 0xff), (byte)((a >> 16) & 0xff));
        }
        [DllImport("user32")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);
    }
}
