using System;

namespace Task2
{
    class Program
    {
        public class TestCase
        {
            public int Input { get; set; }
            public int Expected { get; set; }
        }

        public static int BinarySearch(int[] inputArray, int searchValue)
        {
            int min = 0;
            int max = inputArray.Length - 1;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (searchValue == inputArray[mid])
                {
                    return inputArray[mid];
                }
                else if (searchValue < inputArray[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }
            return -1;
        }

        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var testData = new TestCase[4];
            testData[0] = new TestCase()
            {
                Input = 7,
                Expected = 7
            };
            testData[1] = new TestCase()
            {
                Input = 2,
                Expected = 2
            };
            testData[2] = new TestCase()
            {
                Input = 14,
                Expected = 14
            };
            testData[3] = new TestCase()
            {
                Input = 16,
                Expected = -1
            };

            foreach ( var testCase in testData)
            {
                var result = BinarySearch(array, testCase.Input);
                if (result == testCase.Expected)
                {
                    Console.WriteLine("Test passed");
                }
                else
                {
                    Console.WriteLine("Test failed");
                }               
            }
        }
    }
}
// Асимптотическая сложность бинарного поиска O(log(N))