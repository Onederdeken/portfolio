using KeyDash.Models;
using KeyDash.MVVM;
using KeyDash.Signals;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace KeyDash.ViewModels
{
    public class ViewModelMainPlace : ViewModelBase
    {
        
        private EventBus eventBus{  get;}
        private String text;
        private string inputText = string.Empty;
        public string InputText { get { return inputText; } set { inputText = value; OnPropertyChanged(); }  }
        public String Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        private PartFullTextModel partFullText;
       
        
        public ViewModelMainPlace(EventBus eventBus)
        {
            this.eventBus = eventBus;
            this.eventBus.Subcribe<FileTextModel>( param => setText(param));
            this.eventBus.Subcribe<InputChar>(param => GetInputChar(param));
            
        }
        private void setText(FileTextModel ftm)
        {
            partFullText = new PartFullTextModel()
            {
                FullText = ftm.text.Replace(Environment.NewLine,"").Split(' '),
                countWord = 10
            };
            var tempText = String.Empty;
            partFullText.countPart = partFullText.FullText.Length / partFullText.countWord;
            partFullText.ostatok = partFullText.FullText.Length % partFullText.countWord;
            for (int i = partFullText.countWord * (partFullText.indexpart - 1); i < partFullText.countWord * partFullText.indexpart; i++)
            {
                tempText += partFullText.FullText[i] + " ";
            }
            partFullText.maxindexpart = tempText.Length-1;
            
            Text = tempText.TrimEnd();
            partFullText.indexpart++;
            

        }
        private void GetInputChar(InputChar inputChar)
        {
            if (string.IsNullOrEmpty(inputChar.Item))
            {
                if(inputChar.workKey == WorkKey.BackSpace && !String.IsNullOrEmpty(InputText))
                {
                    InputText = InputText.Remove(InputText.Length-1);
                    partFullText.indexchar--;
                }
            }
            else if(partFullText.indexchar == partFullText.maxindexpart)
            {
                if(inputChar.workKey == WorkKey.Space)
                {
                   UpLoadText();                }
            }
            else
            {
                InputText += inputChar.Item;
                partFullText.indexchar++;
                
            }

           
        }
        private void UpLoadText()
        {
            partFullText.indexchar = 0;
            var tempText = String.Empty;
            Debug.WriteLine($"countPart:{partFullText.countPart} ostatok:{partFullText.ostatok} indexofpart:{partFullText.indexpart}, longtext:{partFullText.FullText.Length} ");

            if (partFullText.indexpart - 1 == partFullText.countPart && partFullText.ostatok != 0)
            {
                for (int i = partFullText.countWord * partFullText.countPart; i < partFullText.FullText.Length; i++)
                {
                    tempText += partFullText.FullText[i] + " ";
                }
                partFullText.indexpart++;
            }
            else if (partFullText.indexpart - 1 > partFullText.countPart)
            {
                Text = string.Empty;
                InputText = string.Empty;
                eventBus.Publish(new EndGame()); return;
            }
            else
            {
                for (int i = partFullText.countWord * (partFullText.indexpart - 1); i < partFullText.countWord * partFullText.indexpart; i++)
                {
                    tempText += partFullText.FullText[i] + " ";
                }
                partFullText.indexpart++;
            }

            partFullText.maxindexpart = tempText.Length-1;
            Text = tempText.TrimEnd();
            InputText = String.Empty;

        }

    }
}
