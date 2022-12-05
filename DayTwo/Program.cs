using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayTwo
{
    public static class Program
    {
        enum Outcomes
        {
            Win = 6,
            Draw = 3,
            Loss = 0
        }
        enum Points 
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
        private static int scorePlay(string p1, string p2)
        {

            switch (p1)
            {
                //Rock
                case "A":
                    if (p2 == "X") return (int)Outcomes.Draw + (int)Points.Rock;
                    if (p2 == "Y") return (int)Outcomes.Win + (int)Points.Paper;
                    if (p2 == "Z") return (int)Outcomes.Loss + (int)Points.Scissors;
                    break;
                // Paper
                case "B":
                    if (p2 == "X") return (int)Outcomes.Loss + (int)Points.Rock;
                    if (p2 == "Y") return (int)Outcomes.Draw + (int)Points.Paper;
                    if (p2 == "Z") return (int)Outcomes.Win + (int)Points.Scissors;
                    break;
                // Scissors
                case "C":
                    if (p2 == "X") return (int)Outcomes.Win + (int)Points.Rock;
                    if (p2 == "Y") return (int)Outcomes.Loss + (int)Points.Paper;
                    if (p2 == "Z") return (int)Outcomes.Draw + (int)Points.Scissors;
                    break;
                default:
                    throw new Exception($"Unexpected plays {p1} & {p2}");
            }
            return 0;
        }


        private static int scorePlayPartTwo(string p1, string outcome)
        {

            // X: Lose
            // Y: Draw
            // Z: Win

            switch (p1)
            {
                //Rock
                case "A":
                    if (outcome == "X") return (int)Outcomes.Loss + (int)Points.Scissors;
                    if (outcome == "Y") return (int)Outcomes.Draw + (int)Points.Rock;
                    if (outcome == "Z") return (int)Outcomes.Win + (int)Points.Paper;
                    break;
                // Paper
                case "B":
                    if (outcome == "X") return (int)Outcomes.Loss + (int)Points.Rock;
                    if (outcome == "Y") return (int)Outcomes.Draw + (int)Points.Paper;
                    if (outcome == "Z") return (int)Outcomes.Win + (int)Points.Scissors;
                    break;
                // Scissors
                case "C":
                    if (outcome == "X") return (int)Outcomes.Loss + (int)Points.Paper;
                    if (outcome == "Y") return (int)Outcomes.Draw + (int)Points.Scissors;
                    if (outcome == "Z") return (int)Outcomes.Win + (int)Points.Rock;
                    break;
                default:
                    throw new Exception($"Unexpected plays {p1} & {outcome}");
            }
            return 0;
        }

        public static Tuple<int, int> partOneAndTwo(string inputPath)
        {
            string input = File.ReadAllText(inputPath);

            string[] cheatSheet = input.Split("\r\n");
            int part1Score = 0;
            int part2Score = 0;

            foreach (var line in cheatSheet)
            {
                var round = line.Split(" ");
                var theirPlay = round[0];
                var myPlay  = round[1];
                var p1Score = scorePlay(theirPlay, myPlay);
                part1Score += p1Score;
                var p2Score = scorePlayPartTwo(theirPlay, myPlay);
                part2Score += p2Score;

            }

            return Tuple.Create(part1Score, part2Score);
        
        }

        public static void Main()
        {
            var example = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayTwo\example.txt");
            Debug.Assert(example.Item1 == 15);
            Debug.Assert(example.Item2 == 12);

            var actual = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayTwo\real.txt");
            Console.WriteLine($"Part One = {actual.Item1} Part Two = {actual.Item2}");
        }
    }
}