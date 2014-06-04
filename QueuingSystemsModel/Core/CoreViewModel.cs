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
        private long sumTotalRequestsCount = 0;
        private double time = 0;

        private IDistribution inflowDistribution = null;
        private IDistribution serviceDistribution = null;

        public ObservableCollection<ReportEntity> Log { get; private set; }
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

        public double AverageTotalRequestsCount
        {
            get
            {
                return (double)this.sumTotalRequestsCount / this.iterationCount;
            }
        }


        public CoreViewModel(IDistribution inflowDistribution, IDistribution serviceDistribution, int servicesCount)
        {
            this.Log = new ObservableCollection<ReportEntity>();
            this.inflowDistribution = inflowDistribution;
            this.serviceDistribution = serviceDistribution;       
            
            this.endServingTimes = Enumerable.Repeat<double>(-1, servicesCount).ToArray();
            this.nextPersonComingTime = 0;
            this.queueSize = 0;                             
        }


        public void NextStep()
        {
            iterationCount++;
            double newTime = this.Time + this.DeltaT;
            while (newTime > nextPersonComingTime)
            {
                this.QueueSize++;
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
            this.sumTotalRequestsCount += busyServicesCount + QueueSize;
            this.Time = newTime;
            this.BusyServices = busyServicesCount;
            RaisePropertyChanged(() => this.AveragePendingRequestsCount);
            RaisePropertyChanged(() => this.AverageTotalRequestsCount);
        }
    }
}
