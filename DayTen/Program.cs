using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayTen
{
    public static class Program
    {
      
        public static int partOne(string inputPath)
        {
            var instructions = File.ReadLines(inputPath).ToList();

            int cycle = 0;
            int x = 1;
            int instructionIndex = 0;
            string currentInstruction = instructions[instructionIndex];
            int cyclesSinceInstructionStart = 0;

            int[] logCyles = new int[] { 20, 60, 100, 140, 180, 220 };
            List<int> signalStrengths = new();

            var row = 0;

            while (true)
            {
                if (logCyles.Contains(cycle))
                {
                    //Console.WriteLine($"Cycle {cycle} X is {x}");
                    signalStrengths.Add(cycle * x);
                }
                //Console.WriteLine($"The current instruction is {currentInstruction} it's been running for {cyclesSinceInstructionStart} cycles");
                // noop
                if (currentInstruction[0] == 'n' && cyclesSinceInstructionStart == 1)
                {
                    //Console.WriteLine($"{currentInstruction} has finished running");
                    cyclesSinceInstructionStart = 0;
                    instructionIndex++;
                    if (instructionIndex == instructions.Count) break;
                    currentInstruction = instructions[instructionIndex];
                }
                if (currentInstruction[0] == 'a' && cyclesSinceInstructionStart == 2)
                {
                    //Console.WriteLine($"{currentInstruction} has finished running");
                    int val = int.Parse(currentInstruction.Replace("addx ", ""));
                    x += val;
                    cyclesSinceInstructionStart = 0;
                    instructionIndex++;
                    if (instructionIndex == instructions.Count) break;
                    currentInstruction = instructions[instructionIndex];
                }

                // Drawing pixel # cycle
                var pixel0 = (x - 1) + (40*row);
                var pixel1 = x + (40 * row);
                var pixel2 = x + 1 + (40 * row);
                if (cycle == pixel0 || cycle == pixel1 || cycle == pixel2) Console.Write("█");
                else Console.Write(" ");
                if ((cycle + 1) % 40 == 0)
                {
                    Console.WriteLine();
                    row += 1;
                }
                cyclesSinceInstructionStart++;
                cycle++;
            }
            Console.WriteLine($"\nProgram finished after {cycle} cycles X is {x} the sum of signal strengths is {signalStrengths.Sum()}\n");
            return signalStrengths.Sum();

        }


        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayTen\example.txt");
            Debug.Assert(example1 == 13140);
            //var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\example.txt");
            //Debug.Assert(example2 == 1);

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayTen\real.txt");
            //var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\real.txt");
            Console.WriteLine($"Part One = {actual1}");// Part Two = {actual2}");
        }
    }
}