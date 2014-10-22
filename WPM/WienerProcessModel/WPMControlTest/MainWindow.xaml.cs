using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPMControls.Drawing;

namespace WPMControlTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IFunction f1 = new Func<decimal, decimal>(x => x * x).ToIFunction();
            IFunction f2 = new Func<double, double>(x => x * Math.Sin(x)).ToIFunction();
            Random rand = new Random();
            IFunction f3 = new Func<double, double>(x => rand.NextDouble() + x).ToIFunction();
            ObservableCollection<IFunction> funcs = new ObservableCollection<IFunction>();
            funcs.Add(f1);
            this.canvas.Functions = funcs;
            funcs.Add(f2);
            //funcs.Add(f3);
        }
    }
}
