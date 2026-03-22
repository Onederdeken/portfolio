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
        private Game gameoptions;
        public Game GameOptions
        {
            get { return gameoptions;} set { gameoptions = value; OnPropertyChanged(); }
        }
        public RelayCommand StartGame { get; private set;  }
        
        
        public ViewModelMainWindow( Game game, ViewModelMainPlace vmmainplace, ViewModelTopMenu vmtopmenu, ViewModelTimer vmtimer, EventBus eventBus)
        {
            this.eventBus = eventBus;
            this.GameOptions = game;
            Menu = vmtopmenu;
            _timer = vmtimer;
            _mainPlace = vmmainplace;
            StartGame = new RelayCommand(execute=>start(), canEx=>CanStart());
            
            this.eventBus.Subcribe<StartGameEventSignal>(_ => ChangesOverlayForStartTimer());
            this.eventBus.Subcribe<EndTimerSignal>(_ => ChangesOverlayForEndTimer());
            this.eventBus.Subcribe<ChangedModesSignal>(sig => ChangedMode(sig));
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
            gameoptions.StartGame = true;
            var startgameevent = new StartGameEventSignal();
            startgameevent.modeltimer = new ModelTimer() { startTime = 5, endTime = 0, operation = Operation.Minus };
            eventBus.Publish<StartGameEventSignal>(startgameevent);

           
            
        }
        private bool CanStart()
        {
            return !gameoptions.StartGame && !gameoptions.ContinueGame && gameoptions.IsText;
        }

    }
}
