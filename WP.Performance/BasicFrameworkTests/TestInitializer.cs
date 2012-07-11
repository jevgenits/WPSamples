using System;
using System.Diagnostics;
using System.Text;

namespace WP.Performance.BasicFrameworkTests
{
    public sealed class TestInitializer
    {
        public static TestReport Execute(string title, Action testToBeExecuted)
        {
            return Execute(title, testToBeExecuted, 1);
        }

        public static TestReport Execute(string title, Action testToBeExecuted, int numberOfTimes)
        {
            if (testToBeExecuted == null || title == null)
            {
                throw new ArgumentException("Test should have both parameters specified: action and title");
            }

            var report = new TestReport() { TestTitle = title, NumberOfTestRuns = numberOfTimes};

            for (int i = 0; i < numberOfTimes; i++)
            {
                Stopwatch sw = new Stopwatch();
                var initialMemoryUsage = Microsoft.Phone.Info.DeviceStatus.ApplicationCurrentMemoryUsage;

                sw.Start();

                testToBeExecuted();

                sw.Stop();

                report.TotalElapsedMilliseconds += sw.ElapsedMilliseconds;
                report.TotalMemoryDelta += Microsoft.Phone.Info.DeviceStatus.ApplicationCurrentMemoryUsage - initialMemoryUsage; 
            }

            report.AverageElapsedMillisecondsPerTest = report.TotalElapsedMilliseconds / report.NumberOfTestRuns;
            report.AverageMemoryDeltaPerTest = report.TotalMemoryDelta / report.NumberOfTestRuns;

            return report;
        }

        public static void ExecuteAndLogResults(string title, Action testToBeExecuted, int numberOfTimes)
        {
            LogResults(Execute(title, testToBeExecuted, numberOfTimes));
        }

        public static void LogResults(TestReport report)
        {
            var sb = new StringBuilder();

            sb.Append("------------------Start of Test-------------------");
            sb.AppendLine(string.Format(@"Test: {0}", report.TestTitle));
            sb.AppendLine(string.Format(@"Total Memory Delta(bytes): {0}", report.TotalElapsedMilliseconds));
            sb.AppendLine(string.Format(@"Average Memory Delta(bytes): {0}", report.AverageMemoryDeltaPerTest));
            sb.AppendLine(string.Format(@"Total Time(ms): {0}", report.TotalElapsedMilliseconds));
            sb.AppendLine(string.Format(@"Average Time(ms): {0}", report.AverageElapsedMillisecondsPerTest));
            sb.Append("------------------End of Test---------------------");

            Debug.WriteLine(sb.ToString());
        }
    }
}
