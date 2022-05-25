using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BloqueoThreads
{
    internal class Program
    {
        static void Main(string[] args)
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
                hilosPersonas[i].Join(); //ayuda a sincronizar (hasta que termine un hilo, no ejecute el siguiente)
            }

        }
    }
    class CuentaBancaria
    {
        double Saldo { get; set; }
        private Object bloqueaSaldoPositivo = new Object();
        public CuentaBancaria(double saldo)
        {
            this.Saldo = saldo;
        }
        
        public double RetirarEfectivo(double cantidad)
        {
            if ((Saldo - cantidad) < 0)
            {
                Console.WriteLine($"Solo queda {Saldo} en la cuenta. Hilo: {Thread.CurrentThread.Name}");
                return Saldo;
            }
            //lock (bloqueaSaldoPositivo) 
                ///hace que solo lo ejecute un hilo a la vez
                //evita la concurrencia
                // se debe identificar el trozo clave que se debe bloquear
                //para que no se vuelva a continuar las operaciones simultaneamente
            //{
                if ((Saldo - cantidad) >= 0) // solo lo debe ejecutar un hilo
                {
                    this.Saldo -= cantidad;
                    Console.WriteLine($"Retiro de {cantidad} realizado \nsolo queda {Saldo} en la cuenta. Hilo: {Thread.CurrentThread.Name}");
                }
            //}
            
            return Saldo;
        }
        public void VamosARetirarEfectivo()
        {
            Console.WriteLine($"Esta sacando dinero el Hilo: {Thread.CurrentThread.Name}");
            RetirarEfectivo(cantidad: 5000);
        }
    }
}
