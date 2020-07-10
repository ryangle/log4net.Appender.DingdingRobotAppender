using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace log4net.Appender.DingTalkRobotAppender.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");

            var logconfig = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config"));
            XmlConfigurator.Configure(repository, logconfig);
            ILog logger = LogManager.GetLogger(repository.Name, "NETCorelog4net");

            logger.Debug("Debug msg " + DateTime.Now.ToShortTimeString());
            Console.ReadLine();
        }
    }
}
