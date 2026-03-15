using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyDash.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Topmenu.xaml
    /// </summary>
    public partial class Topmenu : UserControl
    {
        private bool fullScrean = true;
        public Topmenu()
        {
            InitializeComponent();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnResize_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Window.GetWindow(this);
            if (fullScrean)
            {
                mainWindow.WindowState=WindowState.Normal;
                mainWindow.Width = 1120;
                mainWindow.Height = 630;                
            }
            else
            {
                mainWindow.WindowState=WindowState.Maximized;
            }
            fullScrean = !fullScrean;
        }
    }
}
