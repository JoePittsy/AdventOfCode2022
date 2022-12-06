using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayFour
{
    public static class Program
    {



        public static int partOne(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] teams = input.Split("\r\n");
            int partOne = 0;
            foreach (var team in teams)
            {
                string[] pairs = team.Split(",");
                int start1 = int.Parse(pairs[0].Split("-")[0]);
                int end1 = int.Parse(pairs[0].Split("-")[1]);

                int start2 = int.Parse(pairs[1].Split("-")[0]);
                int end2 = int.Parse(pairs[1].Split("-")[1]);

                if (start1 <= start2 && end1 >= end2) { partOne++; }
                else if (start2 <= start1 && end2 >= end1) { partOne++; }
                //else { Console.WriteLine($"{start1}-{end1} and {start2}-{end2} don't overlap"); }
            }

            return partOne;
        }

        public static int partTwo(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] teams = input.Split("\r\n");
            int partTwo = 0;
            foreach (var team in teams)
            {
                string[] pairs = team.Split(",");
                int start1 = int.Parse(pairs[0].Split("-")[0]);
                int end1 = int.Parse(pairs[0].Split("-")[1]);

                var range1 = Enumerable.Range(start1, end1 - start1 + 1).ToList();

                int start2 = int.Parse(pairs[1].Split("-")[0]);
                int end2 = int.Parse(pairs[1].Split("-")[1]);

                var range2 = Enumerable.Range(start2, end2 - start2 + 1).ToList();

                var r1ContainsR2 = range1.Intersect(range2).Any();
                var r2ContainsR1 = range2.Intersect(range1).Any();

                if (r1ContainsR2) { partTwo++; }
                else if (r2ContainsR1) { partTwo++; }

            }

            return partTwo;
        }

        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFour\example.txt");
            Debug.Assert(example1 == 2);
            var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFour\example.txt");
            Debug.Assert(example2 == 4);

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFour\real.txt");
            var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFour\real.txt");
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}