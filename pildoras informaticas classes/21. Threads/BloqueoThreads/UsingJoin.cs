using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloqueoThreads
{
    internal class UsingJoin
    {
        /// <summary>
        /// Join help us to syncronize the threads
        /// </summary>
        public static void Run()
        {
            CuentaBancaria countFamily = new CuentaBancaria(saldo: 30000);

            Thread[] hilosPersonas = new Thread[15];

            for (int i = 0; i < hilosPersonas.Length; i++)
            {
                Thread t = new Thread(countFamily.VamosARetirarEfectivo);
                hilosPersonas[i] = t;
                t.Name = "Hilo " + i.ToString();
            }

            for (int i = 0; i < hilosPersonas.Length; i++)
            {
                hilosPersonas[i].Start();
                //hilosPersonas[i].Join(); //ayuda a sincronizar (hasta que termine un hilo, no ejecute el siguiente)
            }
        }
    }



    public class CuentaBancaria
    {
        double Saldo { get; set; }
        public CuentaBancaria(double saldo)
        {
            this.Saldo = saldo;
        }

        public double RetirarEfectivo(double cantidad)
        {
            if ((Saldo - cantidad) < 0)
            {
                Console.WriteLine($"Solo queda {Saldo} en la cuenta. Hilo: {Thread.CurrentThread.Name}");
                Thread.Sleep(500);
                return Saldo;
            }

            if ((Saldo - cantidad) >= 0) // solo lo debe ejecutar un hilo
            {
                this.Saldo -= cantidad;
                Thread.Sleep(500);
                Console.WriteLine($"Retiro de {cantidad} realizado \nsolo queda {Saldo} en la cuenta. Hilo: {Thread.CurrentThread.Name}");
            }


            return Saldo;
        }
        public void VamosARetirarEfectivo()
        {
            Console.WriteLine($"Esta sacando dinero el Hilo: {Thread.CurrentThread.Name}");
            RetirarEfectivo(cantidad: 5000);
        }
    }
}
