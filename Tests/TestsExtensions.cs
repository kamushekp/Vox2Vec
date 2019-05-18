using System.Linq;
using NUnit.Framework;

namespace PythonicWayTests.Helpers
{
    public static class TestsExtensions
    {
        public static float[] RandomArray(int count)
        {
            return Enumerable.Range(0, count)
                .Select(_ => (float)TestContext.CurrentContext.Random.NextDouble(-0.01, 0.01)).ToArray();
        }
    }
}