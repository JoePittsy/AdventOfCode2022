using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DaySeven
{
    public static class Program
    {

        public static Tuple<int, int> partOneAndTwo(string inputPath)
        {
            var input = File.ReadAllLines(inputPath);
            ElfFolder baseDir = new ElfFolder(null, "/");

            ElfFolder currentDir = baseDir;

            string currentCommand = "";

            // Skip one as the first command is to enter the / dir
            foreach (var line in input.Skip(1))
            {
                bool isCommand = line[0] == '$';
                if (isCommand)
                {
                    if (line.StartsWith("$ ls")) { currentCommand = "ls";  }
                    if (line.StartsWith("$ cd"))
                    {
                        currentCommand = "cd";
                        var dir = line.Replace("$ cd ", "");
                        if (dir == "..") { currentDir = currentDir.Parent; }
                        else { currentDir = (ElfFolder)currentDir.GetFolder(dir); }
                    }
                }
                else if (currentCommand == "ls")
                {
                    // Folder
                    if (line[0] == 'd')
                    {
                        currentDir.AddChild(new ElfFolder(currentDir, line.Replace("dir ", "")));
                    }
                    // File
                    else
                    {
                        var sizeName = line.Split(" ");
                        currentDir.AddChild(new ElfFile(currentDir, sizeName[1], int.Parse(sizeName[0])));
                    }
                }
            }

            baseDir.PrintSelf(0);
            var smallFolders = baseDir.GetSmallChildren();
            var partOne = smallFolders.Sum(folder => folder.TotalSize);

            var totalDiskSpace = 70000000;
            var updateSpace = 30000000;
            var usedSpace = baseDir.TotalSize;
            var spaceRemaining = totalDiskSpace - usedSpace;
            var neededSpace = updateSpace - spaceRemaining;
            Console.WriteLine($"We need to free up {neededSpace}!");

            var allFolders = baseDir.GetAllChildren();
            var folderSizes = allFolders.FindAll(folder => folder.TotalSize >= neededSpace).Select(folder => folder.TotalSize).ToList();
            folderSizes.Sort();
            return Tuple.Create(partOne, folderSizes[0]);

        }

       
        public static void Main()
        {
            var example = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DaySeven\example.txt");
            Debug.Assert(example.Item1 == 95437);
            Debug.Assert(example.Item2 == 24933642);


            var actual = partOneAndTwo(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DaySeven\real.txt");
            Console.WriteLine($"Part One = {actual.Item1} Part Two = {actual.Item2}");
        }
    }
}