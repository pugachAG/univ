using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMControls.Drawing;
using WPMMath.Probability;
using WPM.Helpers;
using WPMMath.Common;

namespace WPM
{
    public class MainViewModel: ViewModelBase
    {
        private WienerProcess proc = new WienerProcess();

        public MainViewModel()
        {
            this.Functions = new ObservableCollection<IFunction>();
            //AddFunctionAndSupremum(1000);
            AddFunctionAndIntegral(1000, x => 1000 * x * x);
        }

        public ObservableCollection<IFunction> Functions { get; private set; }


        private async void AddFunction(int pointsCount)
        {
            DiscreteFunction func = await GenerateFunction(pointsCount);
            DisplayFunction(func);
        }

        private async void AddFunctionAndSupremum(int pointsCount)
        {
            DiscreteFunction func = await proc.GenerateAsync(Enumerable.Range(0, pointsCount).Select(x => x / (decimal)pointsCount).ToArray());
            DiscreteFunction sup = FunctionsHelper.GetSupremumFunction(func);
            DisplayFunction(func);
            DisplayFunction(sup);
        }

        private async void AddFunctionAndIntegral(int pointsCount, Func<decimal, decimal> f)
        {
            DiscreteFunction func = await proc.GenerateAsync(Enumerable.Range(0, pointsCount).Select(x => x / (decimal)pointsCount).ToArray());
            DiscreteFunction integ = FunctionsHelper.GetIntegralFunction(f, func);
            DisplayFunction(func);
            DisplayFunction(integ);
        }
            

        private async Task<DiscreteFunction> GenerateFunction(int pointsCount)
        {
            DiscreteFunction func = await proc.GenerateAsync(Enumerable.Range(0, pointsCount).Select(x => x / (decimal)pointsCount).ToArray());
            return func;
        }

        private void DisplayFunction(DiscreteFunction func)
        {
            Functions.Add(func.ConvertToIFunction());
        }

    }
}
