using System;
using System.Collections.Generic;
using System.Text;

namespace Astron.Logging
{
    public class LogConfig
    {
        public bool PrintDate { get; }
        public LogLevel MinLevel { get; }
        public LogLevel MaxLevel { get; }

        public LogConfig(bool printDate, LogLevel minLevel, LogLevel maxLevel)
        {
            PrintDate = printDate;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }
    }
}
