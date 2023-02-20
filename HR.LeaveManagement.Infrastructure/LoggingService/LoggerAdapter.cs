using HR.LeaveManagement.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.LoggingService
{
    public class LoggerAdapter<T> : IAppLogger<T>
    {
        private ILogger<T> logger { get; }
        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<T>();
        }

        public void Log(string message, params object[] args)
        {
            LogInformation(message, args);
        }

        public void LogInformation(string message, params object[] args)
        {
            logger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            logger.LogWarning(message, args);
        }
    }
}
