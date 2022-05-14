using System;

namespace Excepciones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /* Capturar una excepcion */
            Console.WriteLine("------------------------------------------------");
            // la captura general siempre va al ultimo

            try
            {
                //int miNumero = int.Parse(Console.ReadLine());
                int miNumero = 32;
            }
            catch (FormatException e)
            {
                Console.WriteLine("Error: e intrujo un valor no valido");
                Console.WriteLine(e.Message);
                //Console.WriteLine(e.StackTrace);
                //Console.WriteLine(e.Source);
                //Console.WriteLine(e.TargetSite);
                //Console.WriteLine(e.HelpLink);
                //Console.WriteLine(e.Data);
                //Console.WriteLine(e.InnerException);
                //Console.WriteLine(e.HResult);
            }
            catch (OverflowException e)
            {
                Console.WriteLine("Error: se introdujo un valor muy alto");
            }
            catch (Exception e) when (e.GetType() == typeof(FormatException))
            {
                Console.WriteLine("Error:" + e.Message);
            }


            /* checked*/
            Console.WriteLine("------------------------------------------------");
            // podemos utilizarlo al activarlo en la configuracion del proyecto
            // Check for aritmethic overflow


            // Max y Min values 
            int maxInt = int.MaxValue;
            int minInt = int.MinValue;
            Console.WriteLine(maxInt);
            Console.WriteLine(minInt);

            // el compilador intenta que el programa no caiga, por eso generar un comportamiento erroneo

            //checked 
            //{
            //    int resultado = maxInt + 20;
            //    Console.WriteLine(resultado);
            //}

            int resultado = maxInt + 20;
            //Console.WriteLine(resultado);


            /* Excepciones Propias */
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Excepciones Propias");

            try
            {
                //int NumeroMes = int.Parse(Console.ReadLine());
                int NumeroMes = 5;
                Console.WriteLine(NombreDelMes(NumeroMes));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            /* Try catch and Finally */
            Console.WriteLine("------------------------------------------------");
            System.IO.StreamReader archivo = null;
            try
            {
                int contador = 0;
                string linea;
                string path = @"C:\Users\joel_\OneDrive\Documentos\GitHub\C_Sharp_beyond_practices\.Clases\Excepciones\Excepciones\database\data.txt";
                archivo = new System.IO.StreamReader(path);

                while ((linea = archivo.ReadLine()) != null)
                {
                    Console.WriteLine(linea);
                    contador++;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error con la lectura del archivo");
            }
            finally
            {
                if (archivo != null) archivo.Close();
                Console.WriteLine("Conexion con el fichero cerrado");

            }
        }


            public static string NombreDelMes(int Mes)
        {
            switch (Mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    throw new ArgumentOutOfRangeException("El numero debe estar entre 1 y 12");
            }
        }
    }
}
