using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        #region Dependency Properties

        public Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly DependencyProperty BackgroundProperty =
             DependencyProperty.Register("Background", typeof(Brush), typeof(VisualHost), new FrameworkPropertyMetadata(null, BackgroundPropertyChanged));

        #endregion

        #region Members

        private VisualCollection children;

        #endregion

        #region Constructors

        public VisualHost()
        {
            this.children = new VisualCollection(this);
            this.Background = Brushes.Transparent;
        }

        #endregion

        #region Properties

        public int ChildrenCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0, "count greater than or equals to zero");

                return this.children.Count - 1;
            }
        }

        #endregion

        #region Methods

        public void AddChild(DrawingVisual child)
        {
            Contract.Requires(child != null, "child != null");

            this.children.Add(child);
        }

        #endregion

        #region FrameworkElement members override

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

        #endregion

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
            {
                sender.children[0] = null;
                sender.children[0] = visual;
            }
        }

        #endregion
    
    }
}
