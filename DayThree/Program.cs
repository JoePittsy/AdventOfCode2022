using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayTwo
{
    public static class Program
    {
      
        public static int partOne(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] backpacks = input.Split("\r\n");
            int partOne = 0;
            foreach (var backpack in backpacks)
            {
                var comp1 = backpack.Substring(0, backpack.Length / 2).ToCharArray();
                var comp2 = backpack.Substring(backpack.Length / 2).ToCharArray();
                var issues = comp1.Intersect(comp2).ToArray();
                var bagScore = 0;
                foreach (var issue in issues)
                {
                    int intIssue = issue;
                    if (intIssue >= 97) intIssue -= 96;
                    else intIssue -= 38;
                    bagScore += intIssue;
                }
                partOne += bagScore;
            }
            return partOne;
        }

        public static int partTwo(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] backpacks = input.Split("\r\n");
            int partTwo = 0;
            for (int i = 0; i < backpacks.Length; i+=3)
            {
                var b1 = backpacks[i];
                var b2 = backpacks[i+1];
                var b3 = backpacks[i+2];
                var issues = b1.Intersect(b2).Intersect(b3).ToArray();
                var bagScore = 0;
                foreach (var issue in issues)
                {
                    int intIssue = issue;
                    if (intIssue >= 97) intIssue -= 96;
                    else intIssue -= 38;
                    bagScore += intIssue;
                }
                partTwo += bagScore;

            }
            return partTwo;
        }

        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayThree\example.txt");
            Debug.Assert(example1 == 157);
            var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayThree\example.txt");
            Debug.Assert(example2 == 70);

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayThree\real.txt");
            var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayThree\real.txt");
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}