﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPMControls.Drawing;

namespace Lab
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<IFunction> Functions { get; private set; }

        private double tMin = 0;
        private double tMax = 10;
        private double tValue = 0;
        private FuncWrapper<double, double> func;

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

        public string InitialValuesString
        {
            set
            {
                InitialValues = TryParseString(value);
            }
        }

        public List<double> InitialValues { get; set; }

        public MainViewModel()
        {
            this.Functions = new ObservableCollection<IFunction>();
            tValue = tMin;
            func = new FuncWrapper<double, double>(x => Math.Sin(TValue * x) * x + Math.Sin(TValue));
        }

        private void RedrawFunction()
        {
            this.Functions.Clear();
            this.Functions.Add(this.func);
        }

        private List<double> TryParseString(string str)
        {
            string[] strs = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<double> res = new List<double>();
            foreach(var s in strs)
            {
                double dbl = 0;
                if (double.TryParse(s.Trim(), out dbl))
                {
                    res.Add(dbl);
                }
            }
            return res;
        }

    }
}
