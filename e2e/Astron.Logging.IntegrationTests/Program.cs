using System;

namespace Astron.Logging.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Displayer.Register<DisplayedInstance>(di => $"[{di.Name}]\nId={di.Id},\nProperty={di.Property}\n");

            var logger = Logger.CreateBuilder()
                .Register(new ConsoleOutputStrategy(false, LogLevel.Debug)) // no date printing, no trace logging
                .Build();

            logger.Log(LogLevel.Trace, "Should not be written on console");
            logger.Log(LogLevel.Debug, "Should be green");
            logger.Log(LogLevel.Info, "Should be blue");
            logger.Log(LogLevel.Warn, "Should be dark yellow");
            logger.Log(LogLevel.Error, "Should be red");
            logger.Log(LogLevel.Fatal, "Should be dark red");
            logger.Log(LogLevel.None); // empty line

            var toDisplay = new DisplayedInstance(1, "MyFirstInstance", short.MaxValue);
            logger.Log(LogLevel.Info, string.Empty, toDisplay);

            Console.ReadLine();
        }
    }
}
