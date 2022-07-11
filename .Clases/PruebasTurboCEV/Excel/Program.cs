using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> MyList = new List<string> { "TEXT", "NOTEXT", "test", "notest" };
            var SList = MyList.FindAll(item => item == item.ToLower());

            foreach (var s in SList)
            {
                Console.WriteLine(s);
            }
        }
    }
}
