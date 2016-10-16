namespace MaxProductCalculator
{
    internal interface IArraysInitializer
    {
        void InitArrays(int[] posMax, int[] negMax, int[] negMin, int[] arr, int numberOfElementsInProduct, out int positiveNumbers);
    }
}