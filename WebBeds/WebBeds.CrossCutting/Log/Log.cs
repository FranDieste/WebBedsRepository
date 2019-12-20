using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using WebBeds.CrossCutting.Interfaces;

namespace WebBeds.CrossCutting.Log
{
    public class LogEnvironment : ILog
    {
              
        private static LogEnvironment logInstance;
        private Logger log;

        public static LogEnvironment Instance
        {
            get
            {
                if (logInstance == null)
                    logInstance = new LogEnvironment();

                return logInstance;
            }
        }

        public void Add(string message)
        {
            if (log == null)
                log = LogManager.GetCurrentClassLogger();

            log.Info("WebBeds: " + message);
        }
    }
}
