using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PruebasTurboCEV
{
    internal class Program
    {
        public const int NONE_SELECTED = -1;
        private List<int> aptsToExclude = null;
        private List<Tuple<int, int>> aptRangesToExclude = null;


        static void Main(string[] args)
        {
            string toAvoid = "134-567-678-465-456";

            var pathMotorFolder = AppDomain.CurrentDomain.BaseDirectory;
            List<string> floors = Directory.GetDirectories(pathMotorFolder)
                            .Select(Path.GetFileName)
                            .ToList();
            foreach (var floor in floors)
            {
                Console.WriteLine(floor);
            }
            Console.WriteLine(Directory.GetDirectories(pathMotorFolder));
            Console.WriteLine(Directory.GetFiles(pathMotorFolder));
            Console.WriteLine(Directory.GetDirectories(pathMotorFolder)
                            .Select(Path.GetFileName));
            Console.WriteLine(Directory.GetDirectories(pathMotorFolder).ToList());
            Console.WriteLine(Directory.GetDirectories(pathMotorFolder)
                            .Select(Path.GetFileName)
                            .ToList());
            Console.WriteLine("end");
            List<string> a = Directory.GetDirectories(pathMotorFolder).ToList();
            List<string> b = Directory.GetDirectories(pathMotorFolder).Select(Path.GetFileName).ToList();
            List<string> c = Directory.GetDirectories(pathMotorFolder).Select(Path.GetFullPath).ToList();
            Console.WriteLine("end");
            
            /* Use RemoveAll */
            //List<string> c = Directory.GetDirectories(pathMotorFolder).ToList();
            List<string> d = Directory.GetDirectories(pathMotorFolder).Select(Path.GetFileName).ToList();
            c.RemoveAll(x => x.Contains("1"));
            d.RemoveAll(x => x.Contains("1"));
            Console.WriteLine("end");


            Example interop = new Example();
            interop.Main();



            //var allPaths = floors.ConvertAll<List<string>>(
            //    floor =>
            //    {
            //        List<string> apts = new List<string>();
            //        List<string> aptsNames = new List<string>();
            //        apts = Directory.GetDirectories(pathMotorFolder + "\\" + floor)
            //                .Select(Path.GetFullPath)
            //                .ToList();
            //        return apts;
            //    }
            //    );
            //foreach (var apts in allPaths)
            //{
            //    foreach (var apt in apts)
            //    {
            //        Console.WriteLine(apt);
            //    }
            //}


            //List<int> list = new List<int> { 1, 2, 3, 4, 5 };
            //List<int> list1 = list.Select(x => 2 * x).ToList();
            //List<int> list2 = list.ConvertAll(x => 2 * x).ToList();
            //foreach (var item in list1)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in list2)
            //{
            //    Console.WriteLine(item);
            //}

            // call SetAptsToAvoid
            //Program p = new Program();
            //p.SetAptsToAvoid(toAvoid);

            //GetIntOutOfString(toAvoid);
        }


        public int GetIntOutOfString(string toParse)
        {
            var resultString = Regex.Match(toParse, @"\d+").Value;
            Console.WriteLine(int.Parse(resultString));
            return int.Parse(resultString);
        }

        public void SetAptsToAvoid(string toAvoid)
        /// <summary>
        /// Metodo recibe un string con numeros separados por comas y/o guion,
        /// retorna una lista de enteros con los numeros a excluir
        /// </summary>
        {

            List<string> aptsToAvoid = toAvoid.Split(',').ToList();
            List<string> rangesToAvoid = aptsToAvoid.FindAll(item => item.Contains("-"));
            List<Tuple<int, int>> ranges = rangesToAvoid.ConvertAll(item => {
                var convertedTokens = item.Split('-').ToList();
                if (convertedTokens.Count != 2 ||
                    String.IsNullOrWhiteSpace(convertedTokens[0]) ||
                    String.IsNullOrWhiteSpace(convertedTokens[1]) ||
                    GetIntOutOfString(convertedTokens[0]) < 0 ||
                    GetIntOutOfString(convertedTokens[1]) < 0 ||
                    GetIntOutOfString(convertedTokens[0]) >= GetIntOutOfString(convertedTokens[1])
                )
                {
                    return new Tuple<int, int>(NONE_SELECTED, NONE_SELECTED);
                }
                else
                {
                    return new Tuple<int, int>(GetIntOutOfString(convertedTokens[0]), GetIntOutOfString(convertedTokens[1]));
                }
            });


            
            ranges.RemoveAll(item => item.Item1 == NONE_SELECTED || item.Item2 == NONE_SELECTED);
            aptsToAvoid = aptsToAvoid.Except(rangesToAvoid).ToList();
            aptsToAvoid.RemoveAll(item => String.IsNullOrWhiteSpace(item));
            aptsToAvoid.RemoveAll(item => String.IsNullOrWhiteSpace(Regex.Match(item, @"\d+").Value));
            this.aptsToExclude = aptsToAvoid.ConvertAll(item => GetIntOutOfString(item));
            this.aptRangesToExclude = ranges;
            
            foreach (var item in aptsToAvoid)
            {
                Console.WriteLine(item);
            }
        }
        
        
    }
    class Apto
    {
        public string Path { get; set; }
        public string Floor { get; set; }
        public string Apt { get; set; }
    }


    class Example
    {
        // Use DllImport to import the Win32 MessageBox function.
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
        public void Main()
        {
            // Call the MessageBox function using platform invoke.
            MessageBox(new IntPtr(1), "Hello World!", "Hello Dialog", 2);
        }
    }
}
