

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
                        if (i < inputtext.Length)
                        {
                            if (string.Equals(inputtext[i], fullText[i]))
                            {
                                var run = new Run(fullText[i].ToString());
                                run.Background = Brushes.Gray;
                                run.Foreground = Brushes.Green;
                                tb.Inlines.Add(run);
                            }
                            else
                            {
                                var run = new Run(fullText[i].ToString());
                                run.Background = Brushes.Gray;
                                run.Foreground = Brushes.Red;
                                tb.Inlines.Add(run);
                            }

                        }
                        else if (i == inputtext.Length)
                        {
                            var run = new Run(fullText[i].ToString());
                            run.Background = Brushes.Gray;
                            run.Foreground = Brushes.White;
                            tb.Inlines.Add(run);
                            
                        }
                        else
                        {
                            var run2 = new Run(fullText[i].ToString());
                            run2.Background = Brushes.Transparent;
                            run2.Foreground = Brushes.Black;
                            tb.Inlines.Add(run2);
                           
                        }
                    }

                }
                
            }
        }
    }
}
