using KeyDash.Abstractions;
using KeyDash.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using ITimer = KeyDash.Abstractions.ITimer;
using TheTime = System.Windows.Threading;
using Timer = KeyDash.Models.Timer;

namespace KeyDash.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для Timer.xaml
    /// </summary>
    public partial class TimerUi : UserControl, INotifyPropertyChanged
    {
        private int seconds;
        private Window window;
        private ITimer timer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Seconds
        {
            get{ return seconds;}
            set {
                seconds = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Seconds"));
            }
        }
        public TimerUi()
        {
            InitializeComponent();
            DataContext = this;
            timer = new Timer(5, 0, Operation.Minus, new TheTime.DispatcherTimer(),ChangePropertyTime, ActionStart, ActionStop);
            this.Loaded += LoadedData;

        }
        private void ChangePropertyTime(int Second)=>Seconds = Second;
        private void ActionStart()
        {
            TimeOverlay.Visibility = Visibility.Visible;
            window.IsEnabled = false;
        }
        private void ActionStop()
        {
            TimeOverlay.Visibility = Visibility.Collapsed;
            window.IsEnabled = true;
            this.Focusable = false;
            window.Focusable = true;
            window.Focus();
        }
        private void LoadedData(object sender, RoutedEventArgs e)=> window = Window.GetWindow(this);
         
        public void startTime()=>timer.startTime(); 
    }
}
