using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MaxProductCalculator.Tests")]

namespace MaxProductCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input numbers: ");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Error: Input string is empty.");
                return;
            }

            string[] inputStringsArray = input.Split();
            var inputArray = new int[inputStringsArray.Length];

            for (int i = 0; i < inputStringsArray.Length; i++)
            {
                if (!int.TryParse(inputStringsArray[i], out inputArray[i]))
                {
                    Console.WriteLine($"Error: Couldn't parse input array. {args[i]} is not Int32.");
                    break;
                }
            }

            var maxProductCalculator = new MaxProductCalculator(new ArraysInitializer(), 3);
            long maxProduct = maxProductCalculator.FindMaxProduct(inputArray);

            Console.WriteLine($"Max product of three numbers from input is {maxProduct}.");
            Console.ReadLine();
        }
    }
}
