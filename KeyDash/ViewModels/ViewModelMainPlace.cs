using KeyDash.Models;
using KeyDash.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KeyDash.ViewModels
{
    public class ViewModelMainPlace : ViewModelBase
    {
        private Game game;
        private String text;
       
        public String Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public ViewModelMainPlace()
        {
            Text = "wrhwpihw0ri whgwri whgwir hirwhhgwqio";
            
        }
     


    }
}
