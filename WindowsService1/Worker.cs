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
            Logger.Debug("In Worker");
            _mythread = new Thread(ThreadedWork);
            _mythread.Name = "Fantasy Cycling Watcher";
            _mythread.IsBackground = true;
            _mythread.Start();

        }
        private void ThreadedWork()
        {
            while (!stopping)
            {
                Logger.Debug("Getting Data");
                GetData();
                
                lock(padlock)
                {
                    //Monitor.Wait(padlock, TimeSpan.FromMinutes(360));
                    //Monitor.Wait(padlock, TimeSpan.FromSeconds(3));
                    Logger.Debug("Waiting 10mins");
                    Monitor.Wait(padlock, TimeSpan.FromMinutes(10));
                }
            }
        }

        private void GetData()
        {
            try
            {
                
            Logger.Debug("Starting to update season");
                
                FantasyYearConfig config = Repository.FantasyYearConfigGetDefault();
                Logger.Debug("Got config");
                
                PDC_Season season = Repository.PDCSeasonGet(config.Year);
                Logger.Debug("Got season");
                
                season.UpdateResults();

                Logger.Debug("Saving updated season to database" );
                
                Repository.PDCSeasonUpdate(season);
                Logger.Debug("Season updated");

            }

            catch (ThreadInterruptedException TIE)
            {
                Logger.Error("Thread Interupt Exception in parsing service");
                Logger.Error(TIE.Message);
                return; 
            }
            catch(ThreadAbortException TAE)
            {
                Logger.Error("Thread Abort Exception in parsing service");
                Logger.Error(TAE.Message);
                
                return; 
            }
            catch(Exception ex)
            {
                Logger.Error("General Exception in parsing service");
                Logger.Error(ex.Message);                
                for (int i = 1; i <= 30; i++)
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
