using System;
using System.Collections.Generic;

namespace Collecciones
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Collection tipo Lista */
            // se puede agregar elementos en tiempo de ejecucion
            List<int> numeros = new List<int>();
            numeros.Add(4);
            numeros.Add(9);
            numeros.Add(7);


            int[] listaNumero = new int[3] { 2,3,5};
            for (int i = 0; i < listaNumero.Length; i++)
            {
                numeros.Add(listaNumero[i]);
            }

            for (int i = 0; i < numeros.Count; i++)
            {
                Console.WriteLine(numeros[i]);
            }

            Console.WriteLine("-------------------------------------------");
            numeros.RemoveAt(numeros.Count-1);
            foreach (int numero in numeros)
            {
                Console.WriteLine(numero);
            }
            Console.WriteLine("hay: " + numeros.Count);



            Console.WriteLine("-------------------LINKEDLIST------------------------");
            /* Linked list */
            LinkedList<int> numerosLinked = new LinkedList<int>();
            numerosLinked.AddLast(4);
            numerosLinked.AddLast(9);
            numerosLinked.AddFirst(7);
            numerosLinked.AddLast(6);
            numerosLinked.AddLast(3);

            //numerosLinked.RemoveFirst();

            foreach (int numero in numerosLinked)
            {
                Console.WriteLine(numero);
            }

            Console.WriteLine("-------------------------------------------");
            numerosLinked.Remove(6);

            LinkedListNode<int> nodoImportante = new LinkedListNode<int>(15);
            numerosLinked.AddFirst(nodoImportante);

            for (LinkedListNode<int> nodo = numerosLinked.First; nodo != null; nodo = nodo.Next)
            {
                Console.WriteLine(nodo.Value);
            }



            Console.WriteLine("------------------QUEUE-------------------------");
            /* Colas - Queue */
            Queue<int> cola = new Queue<int>();
            cola.Enqueue(4);
            cola.Enqueue(9);
            cola.Enqueue(7);

            foreach (int numero in new int[4] { 2, 3, 5, 6 })
            {
                cola.Enqueue(numero);
            }

            foreach (int numero in cola)
            {
                Console.WriteLine(numero);
            }
            
            Console.WriteLine("-------------------------------------------");
            
                       
            Console.WriteLine(cola.Dequeue());

            /* Stack */
            Console.WriteLine("--------------------STACK-----------------------");
            Stack<int> pila = new Stack<int>();
            pila.Push(4);
            pila.Push(9);
            pila.Push(7);

            foreach (int numero in new int[4] { 2, 3, 5, 6 })
            {
                pila.Push(numero);
            }

            foreach (int numero in pila)
            {
                Console.WriteLine(numero);
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(pila.Pop());

            Console.WriteLine("--------------------DICTIONARY-----------------------");
            /* Diccionario */
            Dictionary<string, int> diccionario = new Dictionary<string, int>();
            diccionario.Add("Juan", 18);
            diccionario.Add("Pedro", 20);
            diccionario.Add("Maria", 22);
            

            foreach (KeyValuePair<string, int> par in diccionario)
            {
                Console.WriteLine(par.Key + " " + par.Value);
            }


        }
    }
}
