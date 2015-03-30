using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aml.LoggingService
{
    public interface ILoggingService
    {
        void LogError(Exception ex);
        void LogInfo(string info);
    }
}