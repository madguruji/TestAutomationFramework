using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAUTProj.Utilities
{
    public class ExtentManager
    {
        private static ExtentReports extent;
        public static ExtentReports getInstance()
        {
            if (extent == null) 
            {
                createInstance();
            }
            return extent;
        }
        public static ExtentReports createInstance()
        {
            string fileName = Utils.getReportName();
            string path =Utils.getProjectFolderPath("Reports")+ fileName;
            ExtentSparkReporter reporter = new ExtentSparkReporter(path);
            reporter.Config.Theme=Theme.Standard;
            reporter.Config.DocumentTitle = "Automation Report";
            reporter.Config.Encoding = "utf-8";
            reporter.Config.ReportName = fileName;

            extent=new ExtentReports();
            extent.AddSystemInfo("Organization", "BD");
            extent.AddSystemInfo("TestAutomation", "SeleniumProject");
            extent.AttachReporter(reporter);
            return extent;
        }
    }
}
