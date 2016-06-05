namespace MaxProductCalculator
{
    internal class MaxProductCalculator
    {
        private readonly IArraysInitializer _arraysInitializer;

        public MaxProductCalculator(IArraysInitializer arraysInitializer)
        {
            _arraysInitializer = arraysInitializer;
        }

        public long FindMaxProduct(int[] arr)
        {
            if (arr.Length < 3)
            {
                return -1;
            }

            if (arr.Length == 3)
            {
                return arr[0] * arr[1] * arr[2];
            }

            var posMax = new int[3];
            var negMax = new int[3];
            var negMin = new int[3];

            int positiveNumbersCount;

            _arraysInitializer.InitArrays(posMax, negMax, negMin, arr, out positiveNumbersCount);

            long maxProduct = GetMaxProduct(posMax, negMax, negMin, positiveNumbersCount);

            return maxProduct;
        }

        private long GetMaxProduct(int[] posMax, int[] negMax, int[] negMin, int positiveNumbersCount)
        {
            switch (positiveNumbersCount)
            {
                case 0:
                    return negMin[0] * negMin[1] * negMin[2];

                case 1:
                case 2:
                    return posMax[0] * negMax[0] * negMax[1];

                default:
                    var posMaxValue = posMax[0] * posMax[1] * posMax[2];
                    var posNegValue = posMax[0] * negMax[0] * negMax[1];
                    return posMaxValue > posNegValue ? posMaxValue : posNegValue;
            }
        }
    }
}
