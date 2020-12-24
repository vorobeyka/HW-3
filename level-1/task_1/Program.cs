using System;
using System.Linq;
using System.Collections.Generic;

namespace task_1
{
    class Program
    {
        static bool tryParseIntArray(string str, ref List<int> array)
        {
            var arr = str.Split(',').Select(p => p.Trim()).Where(p => p.Length > 0);
            try
            {
                array = arr.Select(p => int.Parse(p)).ToList<int>();
            }
            catch (Exception)
            {
                Console.Write("Invalid array.");
                return false;
            }
            return array.Count > 0;
        }

        static void ArrayStatistic(List<int> array)
        {
            var average = array.Average();
            Console.WriteLine($"Minimum element: {array.Min()}");
            Console.WriteLine($"Maximum element: {array.Max()}");
            Console.WriteLine($"Sum of array: {array.Sum()}");
            Console.WriteLine($"Average of array: {Math.Round(average, 4)}");
            var sumSquares = array.Select(a => (a - average) * (a - average)).Sum();
            var standardDeviation = Math.Sqrt(sumSquares / array.Count);
            Console.WriteLine($"Standard deviation: {Math.Round(standardDeviation, 4)}");
            Console.WriteLine($"Sorted distinct array:");
            array.Distinct().OrderBy(p => p).ToList().ForEach(p => Console.WriteLine(p));
            // foreach (var i in array.Distinct().OrderBy(p => p))
            // {
                // Console.WriteLine(i);
            // }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.1. LINQ array statistics by Andrey Basystyi.");
            Console.WriteLine("Rules: you must enter the array of integer numbers separated by comma.");
            Console.WriteLine("Enter array:");
            string line = Console.ReadLine();
            List<int> array = null;
            while (!tryParseIntArray(line, ref array))
            {
                Console.WriteLine("Try again:");
                line = Console.ReadLine();
            }
            ArrayStatistic(array);
        }
    }
}
