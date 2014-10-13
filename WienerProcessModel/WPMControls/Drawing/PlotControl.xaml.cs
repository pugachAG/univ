using System;
using System.Collections;
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
    /// <summary>
    /// Interaction logic for PlotControl.xaml
    /// </summary>
    public partial class PlotControl : UserControl
    {
        #region Dependency Properties (Copy & Paste from PlotDrawer)

        public IEnumerable Functions
        {
            get { return (IEnumerable)GetValue(FunctionsProperty); }
            set { SetValue(FunctionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Functions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FunctionsProperty =
            DependencyProperty.Register("Functions", typeof(IEnumerable), typeof(PlotControl), new PropertyMetadata(null));

        public Brush GridBackround
        {
            get { return (Brush)GetValue(GridBackroundProperty); }
            set
            {
                SetValue(GridBackroundProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for GridBackround.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridBackroundProperty =
            DependencyProperty.Register("GridBackround", typeof(Brush), typeof(PlotControl), new PropertyMetadata(null));

        public Brush PlotBackground
        {
            get { return (Brush)GetValue(PlotBackgroundProperty); }
            set
            {
                SetValue(PlotBackgroundProperty, value);
            }
        }

        public static readonly DependencyProperty PlotBackgroundProperty =
             DependencyProperty.Register("PlotBackground", typeof(Brush), typeof(PlotControl), new FrameworkPropertyMetadata(null));

        #endregion

        public PlotControl()
        {
            InitializeComponent();

            this.plotDrawer.SetBinding(PlotDrawer.FunctionsProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath("Functions"),
                    Mode = BindingMode.OneWay
                });

            this.plotDrawer.SetBinding(PlotDrawer.GridBackroundProperty,
                new Binding()
                {
                    Source = this,
                    Path = new PropertyPath("GridBackround"),
                    Mode = BindingMode.OneWay
                });

            this.plotDrawer.SetBinding(PlotDrawer.BackgroundProperty,
               new Binding()
               {
                   Source = this,
                   Path = new PropertyPath("PlotBackground"),
                   Mode = BindingMode.OneWay
               });
        }

        public PlotDrawer PlotDrawer
        {
            get
            {
                return this.plotDrawer;
            }
        }
    }
}
