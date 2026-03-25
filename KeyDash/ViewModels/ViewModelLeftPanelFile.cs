using KeyDash.Abstractions;
using KeyDash.Models;
using KeyDash.MVVM;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace KeyDash.ViewModels
{
    class ViewModelLeftPanelFile:BaseLeftPanel
    {
        private FileTextModel fileTextModel;
        private string error;
        public string Error { get { return error; } set { error = value; OnPropertyChanged(); }  }

        public RelayCommand GetFileCommand { get; private set; }
        public EventBus EventBus { get; private set; }
        private FileTextModel ftm;
       
        public ViewModelLeftPanelFile(EventBus eventBus)
        {
            ftm = new FileTextModel();
            this.EventBus = eventBus;
            GetFileCommand = new RelayCommand(_=>GetFile(), canEx=>CanGetFile());

        }
        private async void GetFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                ftm.path = ofd.FileName;
                await Task.Run(GetText);
                EventBus.Publish(ftm);

            }
        }
        private bool CanGetFile() => true;
        private async void GetText()
        {
            try
            {
                ftm.text = File.ReadAllText(ftm.path);
                
            }
            catch (Exception ex)
            {
                ftm.error = ex.Message;
            }
            
        }
    }
}
