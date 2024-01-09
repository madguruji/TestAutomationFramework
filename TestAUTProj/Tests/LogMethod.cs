using log4net;
using log4net.Config;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace TestAUTProj.Tests
{
    public class LogMethod
    {
        private ILog logger = LogManager.GetLogger(typeof(LogMethod));
        [Test]
        public void Logg()
        {
            Console.WriteLine("Logg");
            logger.Debug("Some debug log");
            logger.Info(String.Format("Person1:"));
            logger.Info(String.Format("Car2"));
            logger.Warn(String.Format("Warning accrued at {0}", DateTime.Now));
            logger.Error(String.Format("Error accrued at {0}", DateTime.Now));
            logger.Fatal(String.Format("Serious problem with car accrued at  {0}", DateTime.Now));

        }
    }
}
