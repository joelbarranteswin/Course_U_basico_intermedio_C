using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvancedLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stopWatch = Stopwatch.StartNew();
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);
            foreach (string file in Directory.GetFiles(@"C:\Users\joel_\OneDrive\Documentos\GitHub\C_Sharp_beyond_practices\.Clases"))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(file);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10))
                    {
                        graphics.DrawString("Banketeshvar", arialFont, Brushes.Blue, firstLocation);
                        graphics.DrawString("Narayan", arialFont, Brushes.Red, secondLocation);
                    }
                }
                bitmap.Save(Path.GetDirectoryName(file) + "Foreachloop" + "\\" + Path.GetFileNameWithoutExtension(file) + Guid.NewGuid()
                    .ToString() + ".jpg");
            }
            Console.WriteLine("foreach loop execution time = {0} seconds\n", stopWatch.Elapsed.TotalSeconds);
            
            
        }
        
        public void pruebaDeForEachVSParallel()
        {
            List<string> fruits = new List<string>();
            fruits.Add("Apple");
            fruits.Add("Banana");
            fruits.Add("Bilberry");
            fruits.Add("Blackberry");
            fruits.Add("Blackcurrant");
            fruits.Add("Blueberry");
            fruits.Add("Cherry");
            fruits.Add("Coconut");
            fruits.Add("Cranberry");
            fruits.Add("Date");
            fruits.Add("Fig");
            fruits.Add("Grape");
            fruits.Add("Guava");
            fruits.Add("Jack-fruit");
            fruits.Add("Kiwi fruit");
            fruits.Add("Lemon");
            fruits.Add("Lime");
            fruits.Add("Lychee");
            fruits.Add("Mango");
            fruits.Add("Melon");
            fruits.Add("Olive");
            fruits.Add("Orange");
            fruits.Add("Papaya");
            fruits.Add("Plum");
            fruits.Add("Pineapple");
            fruits.Add("Pomegranate");

            Console.WriteLine("Printing list using foreach loop\n");

            var stopWatch = Stopwatch.StartNew();
            foreach (string fruit in fruits)
            {
                Console.WriteLine("Fruit Name: {0}, Thread Id= {1}", fruit, Thread.CurrentThread.ManagedThreadId);
            }
            Console.WriteLine("foreach loop execution time = {0} seconds\n", stopWatch.Elapsed.TotalSeconds);
            Console.WriteLine("Printing list using Parallel.ForEach");


            stopWatch = Stopwatch.StartNew();
            Parallel.ForEach(fruits, fruit =>
            {
                Console.WriteLine("Fruit Name: {0}, Thread Id= {1}", fruit, Thread.CurrentThread.ManagedThreadId);

            }
            );
            Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", stopWatch.Elapsed.TotalSeconds);
            Console.Read();
        }
        
        public static void usingForEach()
        {
            var stopWatch = Stopwatch.StartNew();
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);
            foreach (string file in Directory.GetFiles(@"C:\Users\joel_\OneDrive\Documentos\GitHub\C_Sharp_beyond_practices\.Clases"))
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(file);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10))
                    {
                        graphics.DrawString("Banketeshvar", arialFont, Brushes.Blue, firstLocation);
                        graphics.DrawString("Narayan", arialFont, Brushes.Red, secondLocation);
                    }
                }
                bitmap.Save(Path.GetDirectoryName(file) + "Foreachloop" + "\\" + Path.GetFileNameWithoutExtension(file) + Guid.NewGuid()
                    .ToString() + ".jpg");
            }
            Console.WriteLine("foreach loop execution time = {0} seconds\n", stopWatch.Elapsed.TotalSeconds);
        }
        
        public void usingParallel()
        {
            var stopWatch = Stopwatch.StartNew();
            PointF firstLocation = new PointF(10f, 10f);
            PointF secondLocation = new PointF(10f, 50f);
            Parallel.ForEach(Directory.GetFiles(@"C:\Users\joel_\OneDrive\Documentos\GitHub\C_Sharp_beyond_practices\.Clases"), file =>
            {
                Bitmap bitmap = (Bitmap)Image.FromFile(file);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Font arialFont = new Font("Arial", 10))
                    {
                        graphics.DrawString("Banketeshvar", arialFont, Brushes.Blue, firstLocation);
                        graphics.DrawString("Narayan", arialFont, Brushes.Red, secondLocation);
                    }
                }
                bitmap.Save(Path.GetDirectoryName(file) + "Parallel" + "\\" + Path.GetFileNameWithoutExtension(file) + Guid.NewGuid()
                    .ToString() + ".jpg");
            });
            Console.WriteLine("Parallel.ForEach() execution time = {0} seconds", stopWatch.Elapsed.TotalSeconds);
            Console.Read();
        }
    }
}
