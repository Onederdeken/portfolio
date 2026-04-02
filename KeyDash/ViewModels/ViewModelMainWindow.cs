using KeyDash.Abstractions;
using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System.Windows;
using System.Windows.Input;
namespace KeyDash.ViewModels
{
    public class ViewModelMainWindow:ViewModelBase
    {  
        private EventBus eventBus {  get;}

        private ViewModelTopMenu _menu;
        public ViewModelTopMenu Menu
        {
            get { return _menu; } set { _menu = value; OnPropertyChanged();}
        }
        private BaseLeftPanel _leftPanel;
        public BaseLeftPanel LeftPanel
        {
            get { return _leftPanel; } set { _leftPanel = value; OnPropertyChanged(); }
        }
        private ViewModelTimer _timer;
        public ViewModelTimer Timer
        {
            get { return _timer; } set { _timer = value; OnPropertyChanged(); }
        }
        private ViewModelMainPlace _mainPlace;
        public ViewModelMainPlace MainPlace
        {
            get { return _mainPlace; } set { _mainPlace = value; OnPropertyChanged(); }
        }
        private ViewModelStatisticPanel _statisticPanel;
        public ViewModelStatisticPanel StatisticPanel { get { return _statisticPanel; } set { _statisticPanel = value; OnPropertyChanged(); }  }
        private Game gameoptions;
        public Game GameOptions
        {
            get { return gameoptions;} set { gameoptions = value; OnPropertyChanged(); }
        }
        public RelayCommand StartGame { get; private set;  }
        public RelayCommand PreviewTextInput {  get; private set; }
        public RelayCommand PreviewKeyDown { get; private set; }


        public ViewModelMainWindow( Game game, ViewModelMainPlace vmmainplace, ViewModelTopMenu vmtopmenu, ViewModelTimer vmtimer, ViewModelStatisticPanel viewModelStatisticPanel ,EventBus eventBus)
        {
            this.eventBus = eventBus;
            this.GameOptions = game;
            Menu = vmtopmenu;
            _timer = vmtimer;
            _mainPlace = vmmainplace;
            this.StatisticPanel = viewModelStatisticPanel;
            StartGame = new RelayCommand(execute=>start(), canEx=>CanStart());
            PreviewTextInput = new RelayCommand(f => Window_PreviewTextDown((TextCompositionEventArgs)f), canEx=>Can_textDown());
            PreviewKeyDown = new RelayCommand(f => Window_PreviewKeyDown((KeyEventArgs)f));
            this.eventBus.Subcribe<StartGameEventSignal>(_ => ChangesOverlayForStartTimer());
            this.eventBus.Subcribe<EndTimerSignal>(_ => ChangesOverlayForEndTimer());
            this.eventBus.Subcribe<ChangedModesSignal>(sig => ChangedMode(sig));
            this.eventBus.Subcribe<FileTextModel>(ftm => { gameoptions.IsText = true; });
            this.eventBus.Subcribe<EndGame>(_ => { gameoptions.StartGame = false; gameoptions.ContinueGame = false; gameoptions.IsText = false; });
        }
        private void ChangesOverlayForStartTimer()
        {
            GameOptions.StartGame = true;
            GameOptions.OverlayTimer = true;
            GameOptions.OverlayMainWindow = false;
        }
        private void ChangesOverlayForEndTimer()
        {
            GameOptions.OverlayTimer = false;
            GameOptions.OverlayMainWindow = true;
            GameOptions.ContinueGame = true;
            
        }
        private void ChangedMode(ChangedModesSignal changedModesSignal)
        {
            GameOptions.Modes = changedModesSignal.mode ;
            LeftPanel = changedModesSignal.LeftPanel; 
        }
      
      
        private void start()
        {
           
            var startgameevent = new StartGameEventSignal();
            startgameevent.modeltimer = new ModelTimer() { startTime = 3, endTime = 0, operation = Operation.Minus };
            eventBus.Publish<StartGameEventSignal>(startgameevent);
        }
        private bool CanStart()
        {
            return !gameoptions.StartGame && !gameoptions.ContinueGame && gameoptions.IsText;
        }
        private void Window_PreviewTextDown(TextCompositionEventArgs arg)
        {
            if (gameoptions.ContinueGame)
            {
                if(arg.Text != " ")
                {
                    if(gameoptions.indexText == 0) eventBus.Publish(new StartTimerEventSignal());
                    var InputChar = new InputChar()
                    {
                        Item = arg.Text,
                        index = gameoptions.indexText,
                        workKey = WorkKey.Char,
                    };
                    GameOptions.indexText++;
                    eventBus.Publish(InputChar);
                }
               
            }
        }
        private bool Can_textDown()
        {
            return gameoptions.StartGame;
        }
        private void Window_PreviewKeyDown(KeyEventArgs arg)
        {
            if (arg.Key == Key.Back && gameoptions.ContinueGame)
            {
                if (gameoptions.indexText > 0)
                {
                    gameoptions.indexText--;

                    eventBus.Publish(new InputChar
                    {
                        index = gameoptions.indexText,
                        Item = string.Empty,
                        workKey = WorkKey.BackSpace
                    });
                }
                arg.Handled = true;
            }
            else if(arg.Key == Key.Space && gameoptions.ContinueGame)
            {
                eventBus.Publish(new InputChar
                {
                    index = gameoptions.indexText,
                    Item = " ",
                    workKey = WorkKey.Space,
                });
                GameOptions.indexText++;
                arg.Handled = true;
            }
            else if (arg.Key == Key.Tab && gameoptions.StartGame) return;

        }

    }
}
