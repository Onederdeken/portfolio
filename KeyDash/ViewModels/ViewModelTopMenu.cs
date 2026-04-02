using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace KeyDash.ViewModels
{
    public class ViewModelTopMenu:ViewModelBase
    {
        private bool _fullScrean = true;
        public RelayCommand ExitCommand { get; set; }
        public RelayCommand ResizeCommand { get; set;  }
        public RelayCommand HiddenCommand { get; set; }
        public RelayCommand ModeSelect { get; private set; }
        private EventBus EventBus { get;  }
        private Game gameoptions;
        public Game GameOptions
        {
            get { return gameoptions; }
            set { gameoptions = value; OnPropertyChanged(); }
        }

        public ViewModelTopMenu(Game GameOptions, EventBus eventBus)
        {
            this.GameOptions = GameOptions;
            this.EventBus = eventBus;
            ExitCommand = new RelayCommand(_ => Exit());
            ResizeCommand = new RelayCommand(_ => Resize());
            HiddenCommand = new RelayCommand(_ => Hidden());
            ModeSelect = new RelayCommand(param => SelectedMode(param));
        }
        public void SelectedMode(Object param)
        {
            if (param is Modes modes)
            {
                switch (modes)
                {
                    case Modes.File: EventBus.Publish(new ChangedModesSignal() { LeftPanel = new ViewModelLeftPanelFile(EventBus), mode = Modes.File}); break;
                    case Modes.Ai: EventBus.Publish(new ChangedModesSignal() { LeftPanel = new ViewModelLeftPanelAi(), mode = Modes.Ai}); break;
                    case Modes.Random: EventBus.Publish(new ChangedModesSignal() { LeftPanel = new ViewModelLeftPanelRandom(), mode = Modes.Random}); break;
                }
            }

        }
       
        private void Exit()
        {
            Application.Current.Shutdown();
        }
        private void Resize()
        {
            if (_fullScrean)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
                Application.Current.MainWindow.Width = 1120;
                Application.Current.MainWindow.Height = 630;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            _fullScrean = !_fullScrean;
        }
        public void Hidden()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
