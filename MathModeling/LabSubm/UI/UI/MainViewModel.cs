using Common.RealAnalysis;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMControls.Drawing;
using NMMP;

namespace Lab
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<IFunction> Functions { get; private set; }

        Func<double, double, double> y = (x, t) =>
            //Math.Sin(t * x);
            //Math.Sin(t * x) * x + Math.Sin(t);
            x * x * t;


        private double tMin = 0;
        private double tMax = 10;
        private double tValue = 0;
        private IFunction yfunc;
        private IFunction ufunc;

        public double TMin
        {
            get
            {
                return tMin;
            }
            set
            {
                tMin = value;
                RaisePropertyChanged();
            }
        }

        public double TMax
        {
            get
            {
                return tMax;
            }
            set
            {
                tMax = value;
                RaisePropertyChanged();
            }
        }

        public double TValue
        {
            get
            {
                return tValue;
            }
            set
            {
                tValue = value;
                RedrawFunction();
                RaisePropertyChanged();
            }
        }

        private string initialValuesString;

        public string InitialValuesString
        {
            get
            {
                return initialValuesString;
            }
            set
            {
                initialValuesString = value;
                InitialValues = TryParseStringFunction(value);
                if (InitialValues.Count > 1)
                {
                    InitialValuesFuncDrawing = PairsToFunc(InitialValues).ToIFunction();
                }

            }
        }

        public List<Tuple<double, double>> InitialValues { get; set; }

        public FuncRealFunction InitialValuesFunc { get; set; }

        public IFunction InitialValuesFuncDrawing { get; set; }

        public MainViewModel()
        {
            this.Functions = new ObservableCollection<IFunction>();
            tValue = tMin;
            yfunc = new FuncRealFunction(x => y(x, TValue)).ToIFunction();

            var fx = new FuncRealFunction(x => y(x, TValue)).GetNthFunctionalDerivative(1);
            var fy = new FuncRealFunction(x =>
                new FuncRealFunction(t =>
                    y(x, t)).GetNthFunctionalDerivative(1).GetValue(TValue));
            ufunc = fy.ToIFunction();

            this.InitialValuesString = "1 2" + Environment.NewLine + 
                "2 3" + Environment.NewLine + 
                "5 4" + Environment.NewLine +
                "0 -2" + Environment.NewLine + 
                "-9 9" + Environment.NewLine +
                "9 3";
        }

        private void RedrawFunction()
        {
            this.Functions.Clear();
            this.Functions.Add(this.yfunc);
            this.Functions.Add(this.ufunc);
            if (InitialValues != null && InitialValues.Count > 1)
            {
                this.Functions.Add(InitialValuesFuncDrawing);
            }
        }

        private List<Tuple<double, double>> TryParseStringFunction(string str)
        {
            string[] strs = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<Tuple<double, double>> res = new List<Tuple<double, double>>();
            foreach(var ss in strs)
            {
                var pair = ss.Split(' ');
                if (pair.Length == 2)
                {
                    double arg = 0;
                    double val = 0;
                    string strArg = pair[0];
                    string strVal = pair[1];

                    if (double.TryParse(strArg.Trim(), out arg) && double.TryParse(strVal.Trim(), out val))
                    {
                        res.Add(new Tuple<double, double>(arg, val));
                    }
                }
            }
            return res;
        }

        private FuncRealFunction PairsToFunc(List<Tuple<double, double>> inputColl)
        {
            inputColl.Sort(new Comparison<Tuple<double, double>>((t1, t2) => t1.Item1.CompareTo(t2.Item1)));
            Func<double, double> func = new Func<double, double>(x =>
                {
                    int index = -1;
                    for (int i = 0; i < inputColl.Count; i++)
                    {
                        if (inputColl[i].Item1 >= x)
                        {
                            index = i;
                            break;
                        }
                    }
                    if(index == -1)
                        return inputColl.Last().Item2;
                    if(index == 0)
                        return inputColl[0].Item2;

                    double k = (x - inputColl[index - 1].Item1) / (inputColl[index].Item1 - inputColl[index - 1].Item1);
                    return k * inputColl[index].Item2 + (1 - k) * inputColl[index - 1].Item2;
                });
            return new FuncRealFunction(func);
        }

    }
}
