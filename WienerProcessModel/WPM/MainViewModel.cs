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

namespace WPM
{
    public class MainViewModel: ViewModelBase
    {
        private WienerProcess proc = new WienerProcess();

        public MainViewModel()
        {
            this.Functions = new ObservableCollection<IFunction>();
            AddFunction(1000);
            AddFunction(1000);
            AddFunction(1000);
        }

        public ObservableCollection<IFunction> Functions { get; private set; }


        private async void AddFunction(int pointsCount)
        {
            var func = await proc.GenerateAsync(Enumerable.Range(0, pointsCount).Select(x => x / (decimal)pointsCount).ToArray());
            Functions.Add(func.ConvertToIFunction());
        }
    }
}
