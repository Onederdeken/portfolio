

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace KeyDash.Controls
{
    public static  class TextBlockHelper
    {

        public static readonly DependencyProperty InputTextProperty = DependencyProperty.RegisterAttached("InputText",typeof(string), typeof(TextBlockHelper),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,PropertyChanged) );
        public static readonly DependencyProperty FullTextProperty = DependencyProperty.RegisterAttached("FullText", typeof(string), typeof(TextBlockHelper),
            new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PropertyChanged));
        public static void SetInputText(UIElement element, string value) => element.SetValue(InputTextProperty, value);
        public static string GetInputText(UIElement element) => (string)element.GetValue(InputTextProperty);

        public static void SetFullText(UIElement element, string value) => element.SetValue(FullTextProperty, value);
        public static string GetFullText(UIElement element) => (string)element.GetValue(FullTextProperty);
        
        public static void PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            if(o is TextBlock tb)
            {
                string fullText = GetFullText(tb);
                string inputtext = GetInputText(tb);
                tb.Inlines.Clear();
                if (!string.IsNullOrEmpty(fullText))
                {
                    for (int i = 0; i < fullText.Length; i++)
                    {
                        string displayChar = fullText[i].ToString();
                        var run = new Run(displayChar);
                        if (i < inputtext.Length)
                        {
                            if (string.Equals(inputtext[i], fullText[i]))
                            {   if (displayChar == " ") run.Background = Brushes.Green;
                                else run.Background = Brushes.Gray;
                                run.Foreground = Brushes.Green;
                                tb.Inlines.Add(run);
                            }
                            else
                            {
                                if (displayChar == " ") run.Background = Brushes.Red;
                                else run.Background = Brushes.Gray;
                               
                                run.Foreground = Brushes.Red;
                                tb.Inlines.Add(run);
                            }
                        }
                        else if (i == inputtext.Length)
                        {
                            run.Background = Brushes.Gray;
                            run.Foreground = Brushes.White;
                            tb.Inlines.Add(run); 
                        }
                        else
                        {
                            run.Background = Brushes.Transparent;
                            run.Foreground = Brushes.Black;
                            tb.Inlines.Add(run);
                           
                        }
                    }

                }
                
            }
        }
    }
}
