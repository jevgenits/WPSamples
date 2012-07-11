namespace WP.Performance.BasicFrameworkTests
{
    public class TestReport
    {
        public string TestTitle { get; set; }
        public int NumberOfTestRuns { get; set; }

        public long TotalElapsedMilliseconds { get; set; }
        public long TotalMemoryDelta { get; set; }

        public long AverageElapsedMillisecondsPerTest { get; set; }
        public long AverageMemoryDeltaPerTest { get; set; }
    }
}
