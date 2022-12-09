using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayNine
{
    public static class Program
    {
        public static bool TailTouching(int[] head, int[] tail)
        {
            return Math.Abs(head[0] - tail[0]) <= 1 && Math.Abs(head[1] - tail[1]) <= 1;
        }

        public static string TailDirection(int[] head, int[] tail)
        {
            // Calculate the differences between the x and y coordinates of the head and tail
            int dx = head[0] - tail[0];
            int dy = head[1] - tail[1];


            // Head is to the right
            if (dx > 0)
            {
                if (dy == 0) return "E";
                if (dy > 0) return "NE";
                if (dy < 0) return "SE";
            }

            // Head is to the Left
            if (dx < 0)
            {
                if (dy == 0) return "W";
                if (dy > 0) return "NW";
                if (dy < 0) return "SW";
            }

            if (dy > 0 && dx == 0) return "N";
            if (dy < 0 && dx == 0) return "S";

            return "";
        }

        public static int[] UpdateTail(int[] tail, string direction)
        {
            // Update the tail position based on the direction
            switch (direction)
            {
                case "N":
                    tail[1] += 1;
                    break;
                case "NE":
                    tail[0] += 1;
                    tail[1] += 1;
                    break;
                case "NW":
                    tail[0] -= 1;
                    tail[1] += 1;
                    break;
                case "S":
                    tail[1] -= 1;
                    break;
                case "SE":
                    tail[0] += 1;
                    tail[1] -= 1;
                    break;
                case "SW":
                    tail[0] -= 1;
                    tail[1] -= 1;
                    break;
                case "E":
                    tail[0] += 1;
                    break;
                case "W":
                    tail[0] -= 1;
                    break;
            }

            // Return the updated tail position
            return tail;
        }

        public static int partOne(string inputPath)
        {
            var instructions = File.ReadLines(inputPath).ToList();

            var head = new int[] { 0, 0 };
            var tail = new int[] { 0, 0 };

            var visited = new HashSet<string> { "0, 0" };

            foreach (var instruction in instructions)
            {
                var direction = instruction[0];
                var distance = int.Parse(instruction.Substring(2));
                for (int i = 0; i < distance; i++)
                {
                    Console.WriteLine($"Move {direction} {i}/{distance}");
                    if (direction == 'R') head[0] += 1;
                    if (direction == 'L') head[0] -= 1;
                    if (direction == 'U') head[1] += 1;
                    if (direction == 'D') head[1] -= 1;

                    var touching = TailTouching(head, tail);
                    if (!touching)
                    {
                        Console.WriteLine("Head and tail are not touching!");
                        var tDirection = TailDirection(head, tail);
                        tail = UpdateTail(tail, tDirection);
                        visited.Add($"{tail[0]}, {tail[1]}");
                    }
                    Console.WriteLine($"Head = {head[0]}, {head[1]} Tail = {tail[0]}, {tail[1]}");
                }
            }
             WriteArray(visited.ToArray(), "\n");

            return visited.Count;

        }

        public static int partTwo(string inputPath)
        {
            var instructions = File.ReadLines(inputPath).ToList();

            var snake = new int[][]
            {
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
                new int[]{0, 0},
            };

            var visited = new HashSet<string> { "0, 0" };

            foreach (var instruction in instructions)
            {
                var direction = instruction[0];
                var distance = int.Parse(instruction.Substring(2));
                for (int i = 0; i < distance; i++)
                {
                    Console.WriteLine($"Move {direction} {i}/{distance}");
                    if (direction == 'R') snake[0][0] += 1;
                    if (direction == 'L') snake[0][0] -= 1;
                    if (direction == 'U') snake[0][1] += 1;
                    if (direction == 'D') snake[0][1] -= 1;

                    for (int s = 0; s < snake.Length-1; s++)
                    {
                        var head = snake[s];
                        var tail = snake[s + 1];
                        var touching = TailTouching(head, tail);
                        if (!touching)
                        {
                            var tDirection = TailDirection(head, tail);
                            snake[s + 1] = UpdateTail(tail, tDirection);
                            if (s == snake.Length - 2) visited.Add($"{snake[s + 1][0]}, {snake[s + 1][1]}");
                        }
                    }
                }
            }
            return visited.Count;
        }




        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\example.txt");
             Debug.Assert(example1 == 13);
            var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\example.txt");
            Debug.Assert(example2 == 1);

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\real.txt");
            var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayNine\real.txt");
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}