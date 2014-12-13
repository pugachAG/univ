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
using Common.Lab2;
using Common.Lab3;

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
            textBox1.Text = "BG: " + Calc(InputData.Solution, solutionBG, InputData.a, InputData.b).ToString();
            BaseRealFunction solutionLS = await Lab1Solver.GetSolutionAsync(Method.LeastSquares);
            functions.Add(solutionLS.ToIFunction());
            textBox2.Text = "LS: " + Calc(InputData.Solution, solutionLS, InputData.a, InputData.b).ToString();
            functions.Add(InputData.Solution.ToIFunction());
            canvas.Functions = functions;

            btnBG.IsEnabled = true;
        }

        private double Calc(BaseRealFunction f1, BaseRealFunction f2, double a, double b)
        {
            HilbertSpace space = new HilbertSpace(a, b);
            var df = f1.Sum(f2.Minus());
            double val = space.GetScalarProduct(df, df);
            return Math.Sqrt(val);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await BGMethod();
        }

    #endregion

    #region Lab2

        private void goLab2()
        {
            canvas.PlotDrawer.MinimumX = InputData2.a;
            canvas.PlotDrawer.MaximumX = InputData2.b;
            canvas.PlotDrawer.MinimumY = -1;
            canvas.PlotDrawer.MaximumY = 1;

            ObservableCollection<IFunction> functions = new ObservableCollection<IFunction>();

            BaseRealFunction actual = Lab2Solver.Solve();
            functions.Add(actual.ToIFunction());

            BaseRealFunction sol = InputData2.Solution;
            functions.Add(sol.ToIFunction());

            textBox1.Text = "D: " + Calc(sol, actual, InputData2.a, InputData2.b).ToString("F99").TrimEnd("0".ToCharArray());

            canvas.Functions = functions;
        }

    #endregion

    #region Lab3

        private void goLab3()
        {
            canvas.PlotDrawer.MinimumX = 0;
            canvas.PlotDrawer.MaximumX = 1;
            canvas.PlotDrawer.MinimumY = -3;
            canvas.PlotDrawer.MaximumY = 200;

            ObservableCollection<IFunction> functions = new ObservableCollection<IFunction>();

            BaseRealFunction actual = Lab3Solver.Solve();
            functions.Add(actual.ToIFunction());

            double tmp = InputData3.FinishTime;

            InputData3.FinishTime = 0;
            BaseRealFunction init = Lab3Solver.Solve();
            functions.Add(init.ToIFunction());

            InputData3.FinishTime = tmp;

            canvas.Functions = functions;
        }

    #endregion

        private void btnLab2_Click(object sender, RoutedEventArgs e)
        {
            goLab2();
        }

        private void btnLab3_Click(object sender, RoutedEventArgs e)
        {
            goLab3();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            InputData3.FinishTime = ((Slider)sender).Value;
        }

    }
}
