using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayFive
{
    public static class Program
    {

        public static string partOne(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] data = input.Split("\r\n\r\n");
            var stringStacks = data[0].Replace("    ", "[0]").Replace(" ", "").Split("\r\n");
            List<List<string>> listStacks = new List<List<string>>();
            foreach (var rawstack in stringStacks.SkipLast(1))
            {
                var stack = rawstack.Substring(1, rawstack.Length-2).Split("][");
                for (int i = 0; i < stack.Length; i++)
                {
                    var crate = stack[i];
                    if (crate == "0") continue;
                    while (i >= listStacks.Count) listStacks.Add(new List<string>());
                    listStacks[i].Add(crate);
                   
                }
            }

            List<Stack<string>> stacks = new List<Stack<string>>();
            for (int i = 0; i < listStacks.Count; i++)
            {
                listStacks[i].Reverse();
                stacks.Add(new Stack<string>(listStacks[i]));
            }

            var instructions = data[1].Split("\r\n");
            foreach (var instruction in instructions)
            {
                var parts = instruction.Split(" ");
                var amount = int.Parse(parts[1]);
                var from = int.Parse(parts[3])-1;
                var to = int.Parse(parts[5])-1;
                for (int i = 0; i < amount; i++)
                {
                    var crate = stacks[from].Pop();
                    stacks[to].Push(crate);
                }
            }
            char[] message = new char[stacks.Count];
            for (int i = 0; i < stacks.Count; i++)
            {
                message[i] = stacks[i].Peek().ToCharArray()[0];
            }
            return new string(message);
        }

        public static string partTwo(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] data = input.Split("\r\n\r\n");
            var stringStacks = data[0].Replace("    ", "[0]").Replace(" ", "").Split("\r\n");
            List<List<string>> listStacks = new List<List<string>>();
            foreach (var rawstack in stringStacks.SkipLast(1))
            {
                var stack = rawstack.Substring(1, rawstack.Length - 2).Split("][");
                for (int i = 0; i < stack.Length; i++)
                {
                    var crate = stack[i];
                    if (crate == "0") continue;
                    while (i >= listStacks.Count) listStacks.Add(new List<string>());
                    listStacks[i].Add(crate);

                }
            }

            List<Stack<string>> stacks = new List<Stack<string>>();
            for (int i = 0; i < listStacks.Count; i++)
            {
                listStacks[i].Reverse();
                stacks.Add(new Stack<string>(listStacks[i]));
            }

            var instructions = data[1].Split("\r\n");
            foreach (var instruction in instructions)
            {
                var parts = instruction.Split(" ");
                var amount = int.Parse(parts[1]);
                var from = int.Parse(parts[3]) - 1;
                var to = int.Parse(parts[5]) - 1;
                string[] toMove = new string[amount];
                for (int i = 0; i < amount; i++)
                {
                    var crate = stacks[from].Pop();
                    toMove[i] = crate;
                }
                foreach (var crate in toMove.Reverse())
                {
                    stacks[to].Push(crate);
                }
            }
            char[] message = new char[stacks.Count];
            for (int i = 0; i < stacks.Count; i++)
            {
                message[i] = stacks[i].Peek().ToCharArray()[0];
            }
            return new string(message);
        }

        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFive\example.txt");
            Debug.Assert(example1 == "CMZ");
            var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFive\example.txt");
            Debug.Assert(example2 == "MCD");

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFive\real.txt");
            var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayFive\real.txt");
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}