using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QueuingSystemsModel
{
    public class CoreViewModel : BaseViewModel
    {
        private double nextPersonComingTime = 0;
        private double[] endServingTimes = null;
        private int queueSize = 0;
        private int busyServices = 0;
        private int iterationCount = 0;
        private long sumPendingRequestsCount = 0;
        private long sumBusyServices = 0;
        private double time = 0;
        private long totalRequestsCount = 0;
        private long waitedRequestsCount = 0;
        private double pWaitingTheoretical = 0;
        private double averagePendingRequestsCountTheorectical = 0;
        private double averageBusyServicesTheorectical = 0;

        private IDistribution inflowDistribution = null;
        private IDistribution serviceDistribution = null;

        public double Time
        {
            get
            {
                return this.time;
            }
            private set
            {
                this.time = value;
                RaisePropertyChanged();
            }
        }
        public int QueueSize
        {
            get
            {
                return this.queueSize;
            }
            private set
            {
                this.queueSize = value;
                RaisePropertyChanged();
            }
        }
        public int BusyServices
        {
            get
            {
                return this.busyServices;
            }
            private set
            {
                this.busyServices = value;
                RaisePropertyChanged();
            }
        }
        public double DeltaT { get; set;}

        public double AveragePendingRequestsCount
        {
            get
            {
                return (double)this.sumPendingRequestsCount / this.iterationCount;
            }
        }

        public double AverageBusyServices
        {
            get
            {
                return (double)this.sumBusyServices / this.iterationCount;
            }
        }

        public int ServiecesCount
        {
            get
            {
                return this.endServingTimes.Length;
            }
        }

        public double Rho
        {
            get
            {
                return this.serviceDistribution.Mean / (this.inflowDistribution.Mean);
            }        
        }

        public double WaitingProbability
        {
            get
            {
                return (double)this.waitedRequestsCount / (double)this.totalRequestsCount;
            }
        }

        public CoreViewModel(IDistribution inflowDistribution, IDistribution serviceDistribution, int servicesCount)
        {
            this.inflowDistribution = inflowDistribution;
            this.serviceDistribution = serviceDistribution;       
            
            this.endServingTimes = Enumerable.Repeat<double>(-1, servicesCount).ToArray();
            this.nextPersonComingTime = 0;
            this.queueSize = 0;
            this.calcTheoretical();                 
        }

        public double PWaitingTheoretical
        {
            get
            {
                return pWaitingTheoretical;
            }
            set
            {
                pWaitingTheoretical = value;
                RaisePropertyChanged();
            }
        }

        public double AveragePendingRequestsCountTheorectical
        {
            get
            {
                return this.averagePendingRequestsCountTheorectical;
            }
            set
            {
                this.averagePendingRequestsCountTheorectical = value;
                RaisePropertyChanged();
            }
        }

        public double AverageBusyServicesTheorectical
        {
            get
            {
                return this.averageBusyServicesTheorectical;
            }
            set
            {
                this.averageBusyServicesTheorectical = value;
                RaisePropertyChanged();
            }
        }

        public void NextStep()
        {
            iterationCount++;
            double newTime = this.Time + this.DeltaT;
            while (newTime > nextPersonComingTime)
            {
                this.QueueSize++;
                this.totalRequestsCount++;
                if (this.BusyServices == this.ServiecesCount)
                    this.waitedRequestsCount++;
                RaisePropertyChanged(() => this.WaitingProbability);

                this.nextPersonComingTime = this.nextPersonComingTime + this.inflowDistribution.GetDistributionRandomValue();
            }
            sumPendingRequestsCount += QueueSize;
            int busyServicesCount = 0;
            for(int i = 0; i < endServingTimes.Length; i++)
            {
                double endServTime = endServingTimes[i];
                bool free = false;
                if (endServTime < 0 || newTime >= endServTime)
                    free = true;

                if (free && QueueSize > 0)
                {
                    endServingTimes[i] = newTime + this.serviceDistribution.GetDistributionRandomValue();
                    QueueSize--;
                    free = false;
                }
                if (!free)
                    busyServicesCount++;
                else
                    endServingTimes[i] = -1;
            }
            this.sumBusyServices += busyServicesCount;
            this.Time = newTime;
            this.BusyServices = busyServicesCount;
            RaisePropertyChanged(() => this.AveragePendingRequestsCount);
            RaisePropertyChanged(() => this.AverageBusyServices);
        }

        private void calcTheoretical()
        {
            int n = this.ServiecesCount;
            double denom = 0;
            double fact = 1;
            for (int i = 0; i < n; i++)
            {
                fact *= this.Rho / (double) Math.Max(i, 1);
                denom += fact;
            }
            fact *= this.Rho / (double)n;
            double tmp = fact * (n / (n - Rho));
            denom += tmp;
            double num =  tmp;
            this.PWaitingTheoretical = Math.Min(1, num / denom);
            this.AveragePendingRequestsCountTheorectical = n >= this.Rho ? this.PWaitingTheoretical * this.Rho / (n - this.Rho) : double.PositiveInfinity;
            this.AverageBusyServicesTheorectical = Math.Min(this.Rho, n);
        }
            
    }
}
