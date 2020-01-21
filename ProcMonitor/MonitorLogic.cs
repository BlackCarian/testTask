using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProcMonitor
{
    class MonitorLogic
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string _monitorName;
        private int _lifeTime;
        private int _interval;

        public MonitorLogic(string monitorName,
                            int lifeTime,
                            int interval)
        {
            _monitorName = monitorName;
            _lifeTime = lifeTime;
            _interval = interval;
        }

        public void StartMonitor(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                Task.Run(() => Monitor());
                WaitForCheck(token);
            }
        }
        private void Monitor()
        {
            Process[] processes = Process.GetProcessesByName(_monitorName);

            if (processes.Length > 0)
            {
                var proc = processes[0];
                if (IsLifeTimeMoreThenAllowable(proc))
                {
                    KillProcess(proc);
                }
            }

            else
            {
                logger.Info("There is no process with {0} name on local computer", _monitorName);
            }
        }

        private bool IsLifeTimeMoreThenAllowable(Process process)
        {
            try
            {
                TimeSpan timeSpan = DateTime.Now - process.StartTime;

                if (timeSpan.TotalMinutes >= _lifeTime)
                {
                    return true;
                }
                else
                {
                    logger.Info("Process {0} lives less then {1} minutes", _monitorName, _lifeTime);
                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured while trying get start time of proccess {0}: {1}", _monitorName, ex);
                return false;
            }
        }

        private void KillProcess(Process process)
        {
            try
            {
                process.Kill();
                process.WaitForExit(5000);
                if (!process.HasExited)
                {
                    logger.Error("Failed to kill process {0} in 5 seconds", _monitorName);
                }
                else
                {
                    logger.Info("Successfully killed process {0}", _monitorName);
                }
            }
            catch (Exception ex)
            {
                logger.Error("An error occured while trying to kill proccess {0}: {1}", _monitorName, ex);
            }
        }

        private void WaitForCheck(CancellationToken token)
        {
            var t = TimeSpan.FromMinutes(_interval);
            var timeout = DateTime.Now.Add(t);
            while (DateTime.Now < timeout)
            {
                if (!token.IsCancellationRequested)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                }

                else break;
            }
        }
    }
}
