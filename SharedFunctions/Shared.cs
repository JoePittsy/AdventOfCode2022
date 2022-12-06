using System;
using System.Collections.Generic;
using System.Text;

namespace SharedFunctions
{
    public static class Shared
    {
        public static void WriteArray(Array array, string seperator = "")
        {
            foreach (var item in array)
            {
                Console.Write(item);
                Console.Write(seperator);
            }
        }
    }
}
