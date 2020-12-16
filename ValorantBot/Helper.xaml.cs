using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
    public partial class Window1 : Window
    {

        public static bool Final = false;//переменная для  слежки старт стоп кнопки

        public Window1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши start начало программы
        {
            Final = false;
            Thread Th = new Thread(Keybor);//Создаем поток для ожидания нажатия клавиши с клавы
            Th.SetApartmentState(ApartmentState.STA);
            Th.Start();//запуск потока
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши stop начало программы
        {
            Final = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)//оброботчик нажатия клавиши назад 
        {
            MainWindow Main = new MainWindow();//создаем окно стартовое
            Main.Show();//показать его
            this.Close();//закрыть старое
        }

        void Keybor()//Новый поток для ожидания нажатия клавиши с клавы
        {
            Color ButtonAccept = Color.FromArgb(255, 76, 175, 80);//создание цвета кнопки принять из csgo
            Color Buffer = Color.FromArgb(255, 255, 255, 255);//создание цвета буффера для сравнения цвета кнопки принять
            bool PressButton = true;
            while (PressButton && Final==false)//само ожидание нажатие клавиши 
            {
                Thread.Sleep(40);
                if ((Keyboard.GetKeyStates(Key.F8) & KeyStates.Down) > 0)
                {
                    SetCursorPos(Convert.ToInt32(Settings.height - Settings.height * 0.99), Convert.ToInt32(Settings.width - Settings.width * 0.91));//поиск кнопки старт в csgo
                    ClickMouse();
                    Thread.Sleep(500);
                    //SetCursorPos(Convert.ToInt32(Settings.height - Settings.height * 0.85), Convert.ToInt32(Settings.width - Settings.width * 0.87));//поиск кнопки соревновательного режима в csgo
                    Thread.Sleep(500);
                    ClickMouse();
                   // SetCursorPos(1500, 1050);//поиск кнопки запуск в csgo
                    Thread.Sleep(500);
                    ClickMouse();
                   // SetCursorPos(960, 610);//поиск кнопки прянять матч в csgo
                    while (Buffer != ButtonAccept)
                    {//ожидание появления кнопки принять
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
