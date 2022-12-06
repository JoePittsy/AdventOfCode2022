using System.Diagnostics;
using System.Linq;
using static SharedFunctions.Shared;
namespace DaySix
{
    public static class Program
    {

        public static int packetDetector(string input, int packetLen = 4)
        {
            int start = 0;
            int end = packetLen;
            var startSequence = false;
            while (startSequence == false)
            {
                startSequence = new HashSet<char>(input[start..end].ToCharArray()).Count == packetLen;
                start++; end++;
            }
            return end-1;
        }

      

        public static void Main()
        {
            // Part One
            Debug.Assert(packetDetector("mjqjpqmgbljsphdztnvjfqwrcgsmlb") == 7);
            Debug.Assert(packetDetector("bvwbjplbgvbhsrlpgdmjqwftvncz") == 5);
            Debug.Assert(packetDetector("nppdvjthqldpwncqszvftbrmjlhg") == 6);
            Debug.Assert(packetDetector("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg") == 10);
            Debug.Assert(packetDetector("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw") == 11);

            // Part Two
            Debug.Assert(packetDetector("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 14) == 19);
            Debug.Assert(packetDetector("bvwbjplbgvbhsrlpgdmjqwftvncz", 14) == 23);
            Debug.Assert(packetDetector("nppdvjthqldpwncqszvftbrmjlhg", 14) == 23);
            Debug.Assert(packetDetector("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 14) == 29);
            Debug.Assert(packetDetector("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 14) == 26);

            var input = File.ReadAllText(@"C:\Users\Joe\Documents\Repos\Personal\AdventOfCode2022\AdventOfCode2022\DaySix\real.txt");
            var actual1 = packetDetector(input);
            var actual2 = packetDetector(input, 14);
            Console.WriteLine($"Part One = {actual1} Part Two = {actual2}");
        }
    }
}