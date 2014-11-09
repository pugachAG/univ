using Common.RealAnalysis;
using Common.Lab1;
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
using System.Threading;

namespace NMMP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    #region Lab1

        private async Task BGMethod()
        {
            btnBG.IsEnabled = false;

            canvas.PlotDrawer.MinimumX = InputData.a;
            canvas.PlotDrawer.MaximumX = InputData.b;
            canvas.PlotDrawer.MinimumY = 0;
            canvas.PlotDrawer.MaximumY = 100;


            ObservableCollection<IFunction> functions = new ObservableCollection<IFunction>();
            BaseRealFunction solutionBG = await Lab1Solver.GetSolutionAsync(Method.BubnovGalerkin);
            functions.Add(solutionBG.ToIFunction());
            textBox1.Text = "BG: " + Calc(InputData.Solution, solutionBG).ToString();
            BaseRealFunction solutionLS = await Lab1Solver.GetSolutionAsync(Method.LeastSquares);
            functions.Add(solutionLS.ToIFunction());
            textBox2.Text = "LS: " + Calc(InputData.Solution, solutionLS).ToString();
            functions.Add(InputData.Solution.ToIFunction());
            canvas.Functions = functions;

            btnBG.IsEnabled = true;
        }

        private double Calc(BaseRealFunction f1, BaseRealFunction f2)
        {
            HilbertSpace space = new HilbertSpace(InputData.a, InputData.b);
            var df = f1.Sum(f2.Minus());
            double val = space.GetScalarProduct(df, df);
            return Math.Sqrt(val);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await BGMethod();
        }

    #endregion

    }
}
