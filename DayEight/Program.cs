using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DayEight
{
    public static class Program
    {

        public static int partOne(string inputPath)
        {
            var rows = File.ReadLines(inputPath).ToList();
            var columns = Enumerable.Range(0, rows.Max(x => x.Length))
                .Select(i => string.Join("", rows.Where(s => s.Length > i).Select(s => s[i])))
                .ToList();

            var rowWidth = rows.Max(x => x.Length);
            var columnHeight = columns.Max(x => x.Length);
            var visibleTrees = 0;
            //List<string> visibleCoords = new List<string>();

            // OK we know the first & last rows as well as the first & last columns are all visible minus 4 for the corners being counted twice
            visibleTrees = rowWidth + rowWidth + columnHeight + columnHeight - 4;


            for (int y = 1; y < rows.Count - 1; y++)
            {
                string row = rows[y];

                int[] rowTrees = row.Select(c => int.Parse(c.ToString())).ToArray();
                for (int x = 1; x < rowTrees.Length - 1; x++)
                {
                    int[] colTrees = columns[x].Select(c => int.Parse(c.ToString())).ToArray();

                    var tree = rowTrees[x];
                    var leftMax = rowTrees[0..x].Max();
                    var rightMax = rowTrees[(x+1)..rowWidth].Max();
                    var upMax = colTrees[0..y].Max();
                    var downMax = colTrees[(y+1)..columnHeight].Max();
                    if (tree > leftMax || tree > rightMax || tree > upMax || tree > downMax)
                    {
                        visibleTrees++;
                        Console.WriteLine($"Tree {tree} at {x},{y} is visible, there are now {visibleTrees} visible trees");
                        continue;
                    }
                    Console.WriteLine($"Tree {tree} at {x},{y} is invisible");
                }
            }
           return visibleTrees;
        }

        public static int partTwo(string inputPath)
        {
            var rows = File.ReadLines(inputPath).ToList();
            var columns = Enumerable.Range(0, rows.Max(x => x.Length))
                .Select(i => string.Join("", rows.Where(s => s.Length > i).Select(s => s[i])))
                .ToList();

            var rowWidth = rows.Max(x => x.Length);
            var columnHeight = columns.Max(x => x.Length);

            List<int> scores = new List<int>();


            for (int y = 1; y < rows.Count - 1; y++)
            {
                string row = rows[y];

                int[] rowTrees = row.Select(c => int.Parse(c.ToString())).ToArray();
                for (int x = 1; x < rowTrees.Length - 1; x++)
                {
                    int[] colTrees = columns[x].Select(c => int.Parse(c.ToString())).ToArray();

                    var tree = rowTrees[x];
                    var leftTrees = rowTrees[0..x].Reverse().ToArray();
                    var rightTrees = rowTrees[(x + 1)..rowWidth];
                    var upTrees = colTrees[0..y].Reverse().ToArray();
                    var downTrees = colTrees[(y + 1)..columnHeight];

                    Func<int[], int, int[]> createSubarray = (array, key) =>
                    {
                        List<int> subaarray = new List<int>();
                        foreach (var tree in array)
                        {
                            subaarray.Add(tree);
                            if (tree >= key) { break; }
                        }
                        
                        return subaarray.ToArray();
                    };

                    var leftScore = createSubarray(leftTrees, tree).Length;
                    var rightScore = createSubarray(rightTrees, tree).Length;
                    var upScore = createSubarray(upTrees, tree).Length;
                    var downScore = createSubarray(downTrees, tree).Length;
                    var score = leftScore * rightScore * upScore * downScore;
                    Console.WriteLine($"Score for tree {x},{y} is {score}, {leftScore}, {rightScore}, {upScore}, {downScore}");
                    scores.Add(score);

                }
            }
            return scores.Max();
        }


        public static void Main()
        {
            var example1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayEight\example.txt");
            Debug.Assert(example1 == 21);
            var example2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayEight\example.txt");
                Debug.Assert(example2 == 8);

            var actual1 = partOne(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayEight\real.txt");
            var actual2 = partTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DayEight\real.txt");
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}