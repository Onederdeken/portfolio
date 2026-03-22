using KeyDash.Abstractions;
using KeyDash.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.ViewModels
{
    class ViewModelLeftPanelFile:BaseLeftPanel
    {
        private String _filepath;
        public String FilePath
        {
            get {  return _filepath; } set { _filepath = value; OnPropertyChanged();  }
        }

        public ViewModelLeftPanelFile()
        {

        }
    }
}
