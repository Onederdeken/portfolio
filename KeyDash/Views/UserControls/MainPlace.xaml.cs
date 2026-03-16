using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

    public partial class MainPlace : UserControl, INotifyPropertyChanged
    {
        private bool start = false;
        private String text;
        private Window MainWindow;

        public event PropertyChangedEventHandler? PropertyChanged;

        public String Text
        {
            get { return text; }
            set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Text"));
            }
        }

        public MainPlace()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += MainPlace_Loaded;
            Generate();
        }


        private void MainPlace_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow = Window.GetWindow(this);
            MainWindow.PreviewTextInput += MainPlace_PreviewTextInput;


        }
        private void Generate()
        {
            Text = "Солнце только поднималось над горизонтом, когда город начал просыпаться. Легкий ветер гнал по " +
            "тротуарам сухие листья, напоминая о приближении осени. Я люблю это время суток за тишину и " +
            "возможность собраться с мыслями перед началом рабочего дня.\r\n\r\nСлепая печать — это не просто " +
            "умение быстро стучать по клавишам. Это свобода. Когда пальцы сами находят нужные буквы, голова " +
            "перестает думать о процессе и полностью сосредотачивается на тексте. Говорят, что средня" +
            "я скорость грамотного пользователя — около 300 знаков в минуту. Но дело " +
            "не только в цифрах: меньше ошибок, меньше усталости и больше удовольстви" +
            "я от работы.\r\n\r\nЕсли вы читаете этот текст, значит, вы уже на пут" +
            "и к мастерству. Продолжайте тренироваться, и результат не заставит себя ждать. Удачи!";

        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Timers.startTime();
            start = true;
        }
        private void MainPlace_PreviewTextInput(Object sender, TextCompositionEventArgs e)
        {
            if (start == false) return;
            MessageBox.Show(e.Text);



        }
    }
}
