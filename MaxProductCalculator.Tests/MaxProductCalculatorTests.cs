using System;
using System.Diagnostics;
using System.Linq;
using FsCheck;
using NUnit.Framework;

namespace MaxProductCalculator.Tests
{
    public class MaxProductCalculatorTests
    {
        private delegate long CalcSumDelegate(int[] arr);

        [Test]
        public void TestReturnsRightResultWhenAllNumbersAreNegative()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer(), numberOfElementsInProduct: 3);
            var arr = new[] { -11, -4, -20, -8 };
            Assert.AreEqual(this.BruteforceTheAnswer(arr), maxProductCalculator.FindMaxProduct(arr));
        }

        [Test]
        public void TestReturnsRightResultWhenAllNumbersAreNegative2()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer(), numberOfElementsInProduct: 3);
            var arr = new[] { -11, -4, -20, -8 };
            Assert.AreEqual(this.BruteforceTheAnswer(arr), maxProductCalculator.FindMaxProduct(arr));
        }

        [Test]
        public void TestReturnsRightResultOnRandomlyGeneratedInput()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer(), numberOfElementsInProduct: 3);
            var gen = Arb.Generate<int[]>().Where(xs => xs.Count() >= 5);
            var arb = Arb.From(gen);
            Prop.ForAll(arb, xs => this.RunWithStopwatch(maxProductCalculator.FindMaxProduct, xs) == this.RunWithStopwatch(this.BruteforceTheAnswer, xs)).QuickCheck();
        }

        [Test]
        public void TestReturnsRightResultOnRandomlyGeneratedInput2()
        {
            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer(), numberOfElementsInProduct: 3);
            var gen = Arb.Generate<int[]>().Where(xs => xs.Count() >= 5);
            var arb = Arb.From(gen);
            Prop.ForAll(arb, xs => this.RunWithStopwatch(maxProductCalculator.FindMaxProduct, xs) == this.RunWithStopwatch(CalcWithInterviewCakeSolution, xs)).QuickCheck();
        }

        private long BruteforceTheAnswer(int[] arr)
        {
            var sw = Stopwatch.StartNew();
            var maxValue = long.MinValue;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    for (int k = 0; k < arr.Length; k++)
                    {
                        // Avoid using the same element multiple times
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

        private long CalcWithInterviewCakeSolution(int[] arr)
        {
            if (arr.Length < 3)
            {
                return -1;
            }

            var highest = Math.Max(arr[0], arr[1]);
            var lowest = Math.Min(arr[0], arr[1]);
            var highestProductOf2 = arr[0] * arr[1];
            var lowestProductOf2 = arr[0] * arr[1];
            var highestProductOf3 = arr[0] * arr[1] * arr[2];

            for (int i = 2; i < arr.Length; i++)
            {
                highestProductOf3 = Math.Max(highestProductOf3,
                    Math.Max(arr[i] * highestProductOf2, arr[i] * lowestProductOf2));
                highestProductOf2 = Math.Max(highestProductOf2,
                    Math.Max(arr[i]*highest, arr[i]*lowest));
                lowestProductOf2 = Math.Min(lowestProductOf2,
                    Math.Min(arr[i]*highest, arr[i]*lowest));
                highest = Math.Max(highest, arr[i]);
                lowest = Math.Min(lowest, arr[i]);
            }
            
            return highestProductOf3;
        }

        private long RunWithStopwatch(CalcSumDelegate calcSumDelegate, int[] arr)
        {
            var sw = Stopwatch.StartNew();
            var result = calcSumDelegate.Invoke(arr);
            Console.WriteLine($"{calcSumDelegate.Method.Name}: {sw.ElapsedTicks}.");
            return result;
        }
    }
}
