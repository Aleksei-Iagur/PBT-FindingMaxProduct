using System.Linq;

namespace MaxProductCalculator
{
    internal class MaxProductCalculator
    {
        private readonly IArraysInitializer _arraysInitializer;
        private readonly int _numberOfElementsInProduct;

        internal MaxProductCalculator(IArraysInitializer arraysInitializer, int numberOfElementsInProduct)
        {
            _arraysInitializer = arraysInitializer;
            _numberOfElementsInProduct = numberOfElementsInProduct;
        }

        internal long FindMaxProduct(int[] arr)
        {
            if (arr.Length < _numberOfElementsInProduct)
            {
                return -1;
            }

            if (arr.Length == _numberOfElementsInProduct)
            {
                return arr.Aggregate(1, (a, b) => a * b);
            }

            var posMax = new int[_numberOfElementsInProduct];
            var negMax = new int[_numberOfElementsInProduct];
            var negMin = new int[_numberOfElementsInProduct];

            int positiveNumbersCount;

            _arraysInitializer.InitArrays(posMax, negMax, negMin, arr, _numberOfElementsInProduct, out positiveNumbersCount);

            long maxProduct = GetMaxProduct(posMax, negMax, negMin, _numberOfElementsInProduct, positiveNumbersCount);
            
            return maxProduct;
        }

        private long GetMaxProduct(int[] posMax, int[] negMax, int[] negMin, int numberOfElementsInProduct, int positiveNumbersCount)
        {
            switch (positiveNumbersCount)
            {
                case 0:
                    return negMin.Take(numberOfElementsInProduct).Aggregate(1, (a, b) => a * b);

                case 1:
                case 2:
                    return posMax.First() * negMax.Take(numberOfElementsInProduct-1).Aggregate((a,b)=> a * b);

                default:
                    var posMaxValue = posMax.Take(numberOfElementsInProduct).Aggregate(1, (a,b) => a*b);
                    var posNegValue = posMax.First() * negMax.Take(numberOfElementsInProduct - 1).Aggregate((a, b) => a * b);
                    return posMaxValue > posNegValue ? posMaxValue : posNegValue;
            }
        }
    }
}
