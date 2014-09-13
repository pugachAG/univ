using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace QueuingSystemsModel
{
    public class MainViewModel : BaseViewModel
    {
        public const double DeltaT = 0.001; //seconds 
        public const int SleepTime = 1; //miliseconds

        private bool isRunning = false;
        private double averageInflow = 16;
        private int servicesCount = 4;
        private double servingTime = 0.2;
        private CoreViewModel core = null;

        public MainViewModel()
        {
            this.ResetCore();

            this.Start = new DelegateCommand(OnStart);
            this.Pause = new DelegateCommand(OnPause, () => IsRunning);
            this.Reset = new DelegateCommand(OnReset);
        }

        public DelegateCommand Start { get; private set; }

        public DelegateCommand Pause { get; private set; }

        public DelegateCommand Reset { get; private set; }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                this.isRunning = value;
                RaisePropertyChanged();
                this.OnIsRunningChanged();
            }
        }

        public double AverageInflow
        {
            get
            {
                return this.averageInflow;
            }
            set
            {
                this.averageInflow = value;
                RaisePropertyChanged();
            }
        }

        public int ServicesCount
        {
            get
            {
                return this.servicesCount;
            }
            set
            {
                this.servicesCount = value;
                RaisePropertyChanged();
            }
        }

        public CoreViewModel Core
        {
            get
            {
                return this.core;
            }
            set
            {
                this.core = value;
                RaisePropertyChanged();
            }
        }

        public double ServingTime
        {
            get
            {
                return this.servingTime;
            }
            set
            {
                this.servingTime = value;
                RaisePropertyChanged();
            }
        }


        private void OnStart()
        {
            if (IsRunning)
                return;
            this.Run();
        }

        private void OnPause()
        {
            this.IsRunning = false;
        }

        private void OnReset()
        {
            this.IsRunning = false;
            this.ResetCore();
        }

        private void ResetCore()
        {
            this.Core = CoreFactory.CreateCoreExponentianInflowExponentianServTime(this.averageInflow, this.servingTime, this.servicesCount);
            this.Core.DeltaT = DeltaT;
        }

        private void OnIsRunningChanged()
        {
            this.Pause.RaiseCanExecuteChanged();
        }

        private void Run()
        {
            this.IsRunning = true;
            Task.Run(() =>
                {
                    while (IsRunning)
                    {
                        this.core.NextStep();
                        //Thread.Sleep(SleepTime);
                        if (Application.Current == null)
                            break;
                    }
                });
        }

    }
}
