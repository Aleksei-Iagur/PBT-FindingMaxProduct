using System;

namespace MaxProductCalculator
{
    internal class ArraysInitializer : IArraysInitializer
    {
        public void InitArrays(int[] posMax, int[] negMax, int[] negMin, int[] arr, int numberOfElementsInProduct, out int positiveNumbers)
        {
            InitIntArrayWithMinValues(negMin);
            positiveNumbers = 0;

            foreach (var element in arr)
            {
                if (element >= 0)
                {
                    AddToPositiveMaximums(posMax, element);
                    positiveNumbers++;
                }
                else
                {
                    AddToNegativeMaximums(negMax, element);
                    AddToNegativeMinimums(negMin, element);
                }
            }
        }

        private void AddToNegativeMinimums(int[] negativeMin, int value)
        {
            if (value <= negativeMin[negativeMin.Length - 1])
            {
                return;
            }

            for (var i = 0; i < negativeMin.Length; i++)
            {
                if (value <= negativeMin[i])
                {
                    continue;
                }

                var temp = negativeMin[i];
                negativeMin[i] = value;
                value = temp;
            }
        }

        private void AddToNegativeMaximums(int[] negativeMax, int value)
        {
            if (value >= negativeMax[negativeMax.Length - 1])
            {
                return;
            }

            for (var i = 0; i < negativeMax.Length; i++)
            {
                if (value >= negativeMax[i])
                {
                    continue;
                }

                var temp = negativeMax[i];
                negativeMax[i] = value;
                value = temp;
            }
        }

        private void AddToPositiveMaximums(int[] positiveMax, int value)
        {
            if (value <= positiveMax[positiveMax.Length - 1])
            {
                return;
            }

            for (var i = 0; i < positiveMax.Length; i++)
            {
                if (value <= positiveMax[i])
                {
                    continue;
                }

                var temp = positiveMax[i];
                positiveMax[i] = value;
                value = temp;
            }
        }

        private void InitIntArrayWithMinValues(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = Int32.MinValue;
            }
        }
    }
}
