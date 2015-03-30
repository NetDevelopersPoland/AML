using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.Targets;
using NLog.Config;

namespace Aml.LoggingService
{
    public class LoggingService : ILoggingService
    {
        Logger logger;
        public LoggingService()
        {
            FileTarget fileTarget = CreateNlogFileConfiguration();
            SimpleConfigurator.ConfigureForTargetLogging(fileTarget, LogLevel.Info);
            logger = LogManager.GetCurrentClassLogger();
        }
        public void LogError(Exception ex)
        {
            logger.Error(ex);
        }
        public void LogInfo(string info)
        {
            logger.Info(info);
        }
        private static FileTarget CreateNlogFileConfiguration()
        {
            //SimpleConfigurator.ConfigureForTargetLogging(new AsyncTargetWrapper(new EventLogTarget()));
            FileTarget fileTarget = new FileTarget();
            fileTarget.Name = "Aml LoggingService";
            fileTarget.CreateDirs = true;
            fileTarget.Layout = "${date} ${level} ${callsite:className=true:includeSourcePath=false:methodName=true} ${message}";
            fileTarget.FileName = @"C:/logs/Aml/AmlLoggingService.${date:format=yyyy.MM.dd}.log";
            fileTarget.KeepFileOpen = false;
            fileTarget.Encoding = Encoding.UTF8;
            return fileTarget;
        }
    }
}