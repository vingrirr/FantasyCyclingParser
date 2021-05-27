using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FantasyCyclingWeb.Models;
using FantasyCyclingParser;

namespace WindowsService1
{
    public class Worker :IDisposable
    {

        private Thread _mythread;
        private readonly object padlock = new object();
        private volatile bool stopping = false;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Worker()
        {
            _mythread = new Thread(ThreadedWork);
            _mythread.Name = "Fantasy Cycling Watcher";
            _mythread.IsBackground = true;
            _mythread.Start();

        }
        private void ThreadedWork()
        {
            while (!stopping)
            {
                GetData();
                
                lock(padlock)
                {
                    Monitor.Wait(padlock, TimeSpan.FromMinutes(360));
                }
            }
        }

        private void GetData()
        {
            try
            {
                FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
                DashboardViewModel vm = new DashboardViewModel(config); //get the data
         
        //store the data. 
    }

            catch(ThreadInterruptedException TIE)
            {
                Logger.Error(TIE);
                return; 
            }
            catch(ThreadAbortException TAE)
            {
                Logger.Error(TAE);
                return; 
            }
            catch(Exception)
            {
                for (int i = 1; i <= 120; i++)
                    System.Threading.Thread.Sleep(1000);
            }
        }

        ~Worker()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            try
            {
                if (_mythread != null)
                {
                    if (!_mythread.Join(100))
                        _mythread.Abort();
                }
            }
            catch { }
        }

     

        public void Stop()
        {
            stopping = true;
            lock (padlock)
            {

            }
        }
    }
}
