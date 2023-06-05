using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExprecionesRegulares
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string toAvoid = "dpto 201";

            List<string> deptsToAvoid = toAvoid.Split(',').ToList();
            deptsToAvoid.RemoveAll(item => String.IsNullOrWhiteSpace(item));
            var aptsToExclude = deptsToAvoid.ConvertAll(item => item.Trim().ToLower());
            
            List<string> _allPaths = new List<string>
            {
                @"C:\Users\joel_\OneDrive\Escritorio\pruebas\Dpto 203",
                @"C:\Users\joel_\OneDrive\Escritorio\pruebas\Dpto 201",
                @"C:\Users\joel_\OneDrive\Escritorio\pruebas\Dpto 202"
            };

            //List<string> allpaths = _allPaths.ConvertAll(
            //    path =>
            //    {
            //        foreach(string apt in aptsToExclude)
            //        {
            //            if (path.Contains(apt))
            //            {
            //                return path;
            //            }
            //            else
            //            {
            //                return null;
            //            }
            //        }
            //        return path;
            //    }
            //    );


            //List<string> allpaths = _allPaths.ConvertAll(
            //    path =>
            //    {
            //        if (aptsToExclude.Any(s => path.ToLower().Contains(s)))
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            return path;
            //        }
            //    }
            //    );

            List<string> allpaths = _allPaths.ConvertAll(
                path =>
                {
                    if (aptsToExclude.Any(s => path.ToLower().Contains(s)))
                    {
                        return null;
                    }
                    else
                    {
                        return path;
                    }
                }
                );

            allpaths.RemoveAll(apt => apt == null); //quita los apt que son iguales a null
            allpaths.ForEach(Console.WriteLine);
            //string a = "  2019";
            //string trimmed = a.Trim();
            //Console.WriteLine(trimmed);

            //aptsToAvoid.RemoveAll(item => String.IsNullOrWhiteSpace(item.Trim()));
            //string path = @"C:\Users\joel_\OneDrive\Documentos\GitLab\turbocev\containers\app\Presentation\External Files\02.-PBTD-Motor-de-cálculo-v2.2 (2019).xlsm";
            ////string result = Regex.Match(input: path, pattern: @"2019").Value;


            string a = @"C: \Users\joel_\OneDrive\Escritorio\pruebas\Dpto 201";
            bool value = aptsToExclude.Any(s => a.ToLower().Contains(s));

            Console.WriteLine(value);
        }

        public void Example1()
        {
            /* expresione Regulares */
            string frase1 = "Mi nombre es Juan y mi n° es (+51)134-45-56 (+34)124-85-56 y mi codigo postal es 1234";
            string dominio2 = "mi web es https://www.dominio.com";
            //string patron = "[J]";

            //string patron = @"\d{3}-\d{2}-\d{2}";
            //string patron = @"\+51|\+34";
            string patron = "https://(www.)?dominio.com";

            Regex miRegex = new Regex(patron);
            MatchCollection matches = miRegex.Matches(dominio2);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                }
            }
            else
            {
                Console.WriteLine("No hay coincidencias");
            }
        }
    }
}
