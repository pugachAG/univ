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

        private async Task BGMethod()
        {
            btnBG.IsEnabled = false;

            canvas.PlotDrawer.MinimumX = InputData.a - 1;
            canvas.PlotDrawer.MaximumX = InputData.b + 1;
            canvas.PlotDrawer.MinimumY = 0;
            canvas.PlotDrawer.MaximumY = 100;


            ObservableCollection<IFunction> functions = new ObservableCollection<IFunction>();
            BaseRealFunction solution = await BubnovGalerkinMethod.GetSolutionAsync();
            functions.Add(solution.ToIFunction());
            functions.Add(InputData.Solution.ToIFunction());
            canvas.Functions = functions;

            btnBG.IsEnabled = true;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await BGMethod();
        }
    }
}
