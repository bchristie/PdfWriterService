﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Atlas;

namespace PdfWriterService.Host
{
    // Service hosted under a windows service. Used after the service has been installed by SvcUtil
    public class PdfWriterServiceHost : ServiceBase
    {
        public List<IAmAHostedProcess> HostedServices
        {
            get { return this.hostedServices ?? (this.hostedServices = new List<IAmAHostedProcess>()); }
            private set { this.hostedServices = value; }
        }
        private List<IAmAHostedProcess> hostedServices;

        public PdfWriterServiceHost()
        {
            this.ServiceName = "PDF Writer Service";

            //this.EventLog = new System.Diagnostics.EventLog();
            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";

            this.HostedServices.Add(new PdfWriterServiceHost<PdfWriterService.Service1>());
            this.HostedServices.Add(new PdfWriterServiceHost<PdfWriterService.Service2>());
            this.HostedServices.Add(new PdfWriterServiceHost<PdfWriterService.Service3>());

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            this.EventLog.WriteEntry(String.Format("OnPowerEvent({0})", powerStatus), EventLogEntryType.Information);

            switch (powerStatus)
            {
                case PowerBroadcastStatus.BatteryLow:
                case PowerBroadcastStatus.Suspend:
                    this.OnPause();
                    return true;
                case PowerBroadcastStatus.ResumeAutomatic:
                case PowerBroadcastStatus.ResumeCritical:
                case PowerBroadcastStatus.ResumeSuspend:
                    this.OnContinue();
                    return true;
                default:
                    return false;
            }
        }

        protected override void OnStart(string[] args)
        {
            this.EventLog.WriteEntry(String.Format("OnStart({0})", String.Join(",", args ?? new String[0])), EventLogEntryType.Information);

            foreach (IAmAHostedProcess service in this.hostedServices)
            {
                service.Start();
            }
        }

        protected override void OnPause()
        {
            this.EventLog.WriteEntry("OnPause()", EventLogEntryType.Information);

            this.OnStop();
        }

        protected override void OnContinue()
        {
            this.EventLog.WriteEntry("OnContinue()", EventLogEntryType.Information);

            this.OnStart(null);
        }

        protected override void OnStop()
        {
            this.EventLog.WriteEntry("OnStop()", EventLogEntryType.Information);

            foreach (IAmAHostedProcess service in this.hostedServices)
            {
                service.Stop();
            }
        }

        #region Run as console application

        // this is generally only ran while you're in the IDE running the service. A windows service does not use Main()
        // so this essentially does what windows does in the backgroudn (in terms of spinning up the service and llowing
        // you to access it)

        [STAThread]
        public static void Main(String[] args)
        {
            PdfWriterServiceHost service = new PdfWriterServiceHost();
            if (Environment.UserInteractive)
            {
                service.OnStart(args);
                Thread.Sleep(TimeSpan.FromSeconds(1));

                Console.Title = "Started";
                Console.WriteLine("Your service is now running.");
                ConsoleKeyInfo keyInfo;
                Boolean runService = true;
                while (runService)
                {
                    Console.Write("Options [P]ause, [C]ontinue or [Q]uit: ");
                    keyInfo = Console.ReadKey();
                    Console.WriteLine();
                    switch (keyInfo.KeyChar)
                    {
                        case 'P':
                        case 'p':
                            if (Console.Title == "Started")
                            {
                                service.OnPause();
                                Console.Title = "Paused";
                            }
                            else
                            {
                                Console.WriteLine("\tService is already paused");
                            }
                            break;
                        case 'C':
                        case 'c':
                            if (Console.Title == "Paused")
                            {
                                service.OnContinue();
                                Console.Title = "Started";
                            }
                            else
                            {
                                Console.WriteLine("\tService is already started");
                            }
                            break;
                        case 'Q':
                        case 'q':
                            runService = false;
                            Console.Title = "Exiting...";
                            break;
                    }
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        runService = false;
                    }
                }

                service.OnStop();
                Console.Title = "Stopped";
            }
            else
            {
                ServiceBase.Run(service);
            }
        }

        #endregion
    }

    /// <summary>
    /// Generic service host
    /// </summary>
    public class PdfWriterServiceHost<TService> : IAmAHostedProcess
        where TService : class, new()
    {
        #region Properties

        /// <summary>
        /// Gets if the service is running
        /// </summary>
        public Boolean IsRunning
        {
            get { return this.ServiceThread != null; }
        }

        private Thread ServiceThread = null;
        private ManualResetEvent StopService = null;

        #endregion

        #region IAmAHostedProcess Members

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public void Pause()
        {
            this.Stop();
        }

        /// <summary>
        /// Resumes this instance.
        /// </summary>
        public void Resume()
        {
            this.Start();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (this.IsRunning)
            {
                this.Stop();
                this.ServiceThread.Join();
            }

            this.StopService = new ManualResetEvent(false);

            this.ServiceThread = new Thread(new ThreadStart(this.Worker));
            this.ServiceThread.Name = typeof(TService).Name;
            this.ServiceThread.Start();
        }

        /// <summary>
        /// Stops the executing service.
        /// </summary>
        public void Stop()
        {
            if (this.IsRunning)
            {
                this.StopService.Set();
                this.ServiceThread.Join();
            }
        }

        #endregion

        #region Methods

        private void Worker()
        {
            using (ServiceHost host = new ServiceHost(typeof(TService)))
            {
                host.Open();

                foreach (var endpoint in host.Description.Endpoints)
                {
                    Debug.WriteLine(String.Format("{0}: {1}", typeof(TService).Name, endpoint.Address));
                }

                this.StopService.WaitOne();

                host.Close();
            }
        }

        #endregion
    }
}
