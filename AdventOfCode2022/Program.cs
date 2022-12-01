using static SharedFunctions.Shared;
using System.Linq;
using System.Diagnostics;

namespace DayOne
{
    public static class Program
    {
        public static Tuple<int, int> partOneAndTwo(string inputPath)
        {

            string input = File.ReadAllText(inputPath);

            string[] elves = input.Split("\r\n\r\n");
            int[] elvesFood = new int[elves.Length];
            for (int i = 0; i < elves.Length; i++)
            {
                int totalCalories = Array.ConvertAll(elves[i].Split("\r\n"), s => int.Parse(s)).Sum();
                elvesFood[i] = totalCalories;
            }
            Array.Sort(elvesFood);
            Array.Reverse(elvesFood);

            return new Tuple<int, int>(elvesFood[0], elvesFood.Take(3).Sum());
        }

        public static void Main()
        {
            var example = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\AdventOfCode2022\example1.txt");
            Debug.Assert(example.Item1 == 24000);
            Debug.Assert(example.Item2 == 45000);

            var actual = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\AdventOfCode2022\real1.txt");
            Console.WriteLine($"Part One = {actual.Item1}");
            Console.WriteLine($"Part Two = {actual.Item2}");

        }

    }

}





