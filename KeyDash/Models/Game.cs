using KeyDash.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace KeyDash.Models
{
    public class Game:ViewModelBase
    {
        private bool _startGame = false;
        private bool _continueGame = false;
        private bool _overlayTimer = false;
        private bool _overlayMainWindow = true;
        private bool _isText = false;
        private bool _startTimer = false;
        private EventBus eventBus;
        private Modes _modes;
        public Game( EventBus eventBus, Modes modes)
        {
            this.eventBus = eventBus;
            this.Modes = modes;
            
        }
        public bool StartTimer { get { return _startTimer; } set { _startTimer = value; OnPropertyChanged(); }  }
        public int indexText = 0;
        public bool IsText {  get { return _isText; } set { _isText = value; OnPropertyChanged(); } }
        public bool ContinueGame { get { return _continueGame; } set { _continueGame = value; OnPropertyChanged(); }}
        public Modes Modes
        {
            get { return _modes; } set { _modes = value; OnPropertyChanged();  }
        }

        public bool OverlayMainWindow
        {
            get { return _overlayMainWindow; } set { _overlayMainWindow = value; OnPropertyChanged(); }
        }
        public bool OverlayTimer
        {
            get { return _overlayTimer; } set { _overlayTimer = value; OnPropertyChanged(); }
        }
        public bool StartGame
        {
            get { CommandManager.InvalidateRequerySuggested(); return _startGame; } set { _startGame = value; OnPropertyChanged(); }
        }
       
    }
    public enum Modes
    {
        None,
        File,
        Ai,
        Random
    }

}
