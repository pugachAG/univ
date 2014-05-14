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

namespace CalcMethLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.IntegralsDemo();
        }

        private void MeanSquareMethodDemo(bool isDiscrete = false)
        {
            AproximationMethodBase solution = isDiscrete ? (AproximationMethodBase)(new MeanSquareMethodDicrete()) : new MeanSquareMethodContinuous();
            plot.Draw(solution.f, solution.a - 1, solution.b + 1, -5, 5);
            plot.Draw(solution.a);
            plot.Draw(solution.b);

            Func<double, double> g = solution.Solve();
            plot.Draw(g);
            if (isDiscrete)
                plot.DrawDiscrete(solution.f, solution.a, solution.b, solution.M);
            text.Text = solution.diff.ToString();
        }

        private void SmoothingSplineDemo()
        {
            AproximationMethodBase solution = new SmoothingSpline();
            plot.Draw(solution.f, solution.a - 1, solution.b + 1, -5, 5);
            plot.Draw(solution.a);
            plot.Draw(solution.b);

            Func<double, double> g = solution.Solve();
            plot.Draw(g);
            plot.DrawDiscrete(solution.f, solution.a, solution.b, solution.M + 1);
            text.Text = solution.diff.ToString();
        }

        private void MatrixText()
        {
            //MeanSquareMethodDemo(true);
            Matrix A = new Matrix(new double[] { 1, 2, 3, 3, 2, 1, 2, 1, 3 });
            Matrix B = new Matrix(new double[] { 4, 5, 6, 6, 5, 4, 4, 6, 5 });
            Matrix C = A + B;
        }

        private void IntegralsDemo()
        {
            IntegralCalculation solution = new IntegralCalculation();
            double upperBound = solution.GetUpperBound();
            double yDiff = 0.3;
            plot.Draw(solution.F, 0, upperBound, - yDiff, yDiff);

            text.Text = solution.Integrate().ToString();
        }
    }
}
