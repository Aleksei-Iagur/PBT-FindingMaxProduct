using System.Linq;
using FsCheck;
using NUnit.Framework;

namespace MaxProductCalculator.Tests
{
    public class MaxProductCalculatorTests
    {
        [Test]
        public void TestReturnsRightResultWhenAllNumbersAreNegative()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer());
            var arr = new int[] { -11, -4, -20, -8 };
            Assert.AreEqual(this.BruteforceTheAnswer(arr), maxProductCalculator.FindMaxProduct(arr));
        }

        [Test]
        public void TestReturnsRightResultOnRandomlyGeneratedInput()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer());
            var gen = Arb.Generate<int[]>().Where(xs => xs.Count() >= 3);
            var arb = Arb.From(gen);
            Prop.ForAll(arb, xs => maxProductCalculator.FindMaxProduct(xs) == this.BruteforceTheAnswer(xs)).QuickCheck();
        }
        
        private long BruteforceTheAnswer(int[] arr)
        {
            var maxValue = long.MinValue;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    for (int k = 0; k < arr.Length; k++)
                    {
                        // Avoid using a single element multiple times
                        if (i == j || i == k || j == k)
                        {
                            continue;
                        }

                        var current = arr[i] * arr[j] * arr[k];

                        if (current > maxValue)
                        {
                            maxValue = current;
                        }
                    }
                }
            }

            return maxValue;
        }
    }
}
