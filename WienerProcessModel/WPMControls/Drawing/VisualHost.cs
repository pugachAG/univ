using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPMControls.Drawing
{
    public class VisualHost : FrameworkElement
    {
        public static readonly DependencyProperty BackgroundProperty =
             DependencyProperty.Register("Background", typeof(Brush), typeof(VisualHost), new FrameworkPropertyMetadata(Brushes.Transparent, BackgroundPropertyChanged));

        VisualCollection children;

        public VisualHost()
        {
            this.children = new VisualCollection(this);
        }

    #region Properties
        
        public Brush Background
        {
            get
            {
                return (Brush)GetValue(BackgroundProperty);
            }
            set
            {
                SetValue(BackgroundProperty, value);
            }
        }

    #endregion

        protected override int VisualChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.children[index];    
        }

    #region Dependency properties event handlers

        private static void BackgroundPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            VisualHost sender = (VisualHost)d;
            Brush brush = (Brush)e.NewValue;

            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();

            context.DrawRectangle(brush, null, new Rect(0, 0, sender.Width, sender.Height));

            context.Close();

            if (sender.children.Count == 0)
                sender.children.Add(visual);
            else
                sender.children[0] = visual;
        }

    #endregion
    
    }
}
