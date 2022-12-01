using System;
using System.Collections.Generic;
using System.Text;

namespace SharedFunctions
{
    public static class Shared
    {
        public static void WriteArray(Array array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
    }
}
