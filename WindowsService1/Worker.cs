using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FantasyCyclingWeb.Models;

namespace WindowsService1
{
    public class Worker :IDisposable
    {

        private Thread _mythread;
        private readonly object padlock = new object();
        private volatile bool stopping = false; 


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
                int year = DateTime.Now.Year; //todo: read from config file or some shit
                DashboardViewModel vm = new DashboardViewModel(year); //get the data

                FantasyCyclingParser.SeasonSnapshot snap = new FantasyCyclingParser.SeasonSnapshot();

                snap.Teams.Add(vm.CloserEncounter);
                snap.Teams.Add(vm.Cowboys);

                snap.Teams.Add(vm.Goldbar35);
                snap.Teams.Add(vm.KamnaChameleon);
                snap.Teams.Add(vm.Krtecek_35);
                snap.Teams.Add(vm.KruijswijksSnowplows);
                snap.Teams.Add(vm.Plaidstockings);
                snap.Teams.Add(vm.Rubicon);

                snap.Teams.Add(vm.TeamTacoVan);
                snap.Teams.Add(vm.TheBauhausMovement);
                snap.Teams.Add(vm.Zauzage);


                FantasyCyclingParser.Repository.SnapshotInsert(snap);

                //store the data. 
            }

            catch(ThreadInterruptedException TIE)
            {
                return; 
            }
            catch(ThreadAbortException TAE)
            {
                return; 
            }
            catch(Exception ex)
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
