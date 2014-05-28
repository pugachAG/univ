using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueuingSystemsModel
{
    public class MainViewModel : ViewModelBase
    {
        public DelegateCommand Start { get; private set; }
        public DelegateCommand Pause { get; private set; }
        public DelegateCommand Reset { get; private set; }

        public MainViewModel()
        {
            this.Start = new DelegateCommand(OnStart);
            this.Pause = new DelegateCommand(OnPause);
            this.Reset = new DelegateCommand(OnReset);
        }

        private void OnStart()
        {
        }

        private void OnPause()
        {
        }

        private void OnReset()
        {
        }

    }
}
