using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace KeyDash.Controls
{
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));
            
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,OnChangedProperty,CoerceValue,false));
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(nameof(Min), typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(250, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(NumericUpDown));
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public int Min
        {
            get { return (int)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); } 
        }
        
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty);  } set { SetValue(CornerRadiusProperty,value);}
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Value = Min;
            if(GetTemplateChild("PART_text") is TextBox text)
            {
                text.PreviewTextInput += (sender, args) => {
                    if (!char.IsDigit(args.Text,0)) args.Handled = true;
                };
            }
            if (GetTemplateChild("PART_UpButton") is RepeatButton up)
            {
                up.Tag = new CornerRadius(0, CornerRadius.TopRight, CornerRadius.BottomRight, 0);
                up.Click += (e, r) => Value++;
            }
            if(GetTemplateChild("PART_DownButton") is RepeatButton down)
            {
                down.Tag = new CornerRadius(CornerRadius.TopLeft, 0, 0, CornerRadius.BottomLeft);
                down.Click += (e, t) =>
                {
                    Value--;
                };

            }
        }
        private static Object CoerceValue(DependencyObject dobj, Object obj)
        {
            int newValue = (int)obj;
            var control = (NumericUpDown)dobj;
            
            if(newValue < control.Min)
            {
                return (object)control.Min;
            }
            return (object)newValue;
        }
        private static void OnChangedProperty(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
           
        }
        
       
    }
}
