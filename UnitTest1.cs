namespace XunitParallelExecutionVsRunnerBug
{
    [Collection("Foobar")]
    public class UnitTest1
    {
        private static readonly bool[] TestRun = new bool[typeof(UnitTest1).GetMethods().Length];

        [Fact]
        public async Task Test1()
        {
            StartTestRun(1);

            await Task.Delay(1000);

            EndTestRun(1);
        }

        [Fact]
        public async Task Test2()
        {
            StartTestRun(2);

            await Task.Delay(1000);

            EndTestRun(2);
        }
        
        [Fact]
        public async Task Test3()
        {
            StartTestRun(3);

            await Task.Delay(1000);

            EndTestRun(3);
        }

        [Fact]
        public async Task Test4()
        {
            StartTestRun(4);

            await Task.Delay(1000);

            EndTestRun(4);
        }

        private static void StartTestRun(int number)
        {
            lock (TestRun)
            {
                // Assert no other tests is running.
                for (var i = 0; i < TestRun.Length; i++)
                {
                    Assert.False(TestRun[i], $"The Test{i + 1} is running in parallel with the Test{number}...");
                }

                TestRun[number - 1] = true;
            }
        }

        private static void EndTestRun(int number)
        {
            lock (TestRun)
            {
                TestRun[number - 1] = false;
            }
        }
    }
}