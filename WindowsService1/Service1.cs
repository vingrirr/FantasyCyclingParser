using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public partial class FantasyCyclingWatcher : ServiceBase
    {
      
        Worker worker;
        public FantasyCyclingWatcher()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {


            
            Debugger.Launch();
            Debugger.Break();

            worker = new Worker();

        }

        protected override void OnStop()
        {
            worker.Stop();
        }
    }
}
