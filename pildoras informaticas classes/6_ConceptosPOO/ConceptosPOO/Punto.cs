using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptosPOO
{
    internal class Punto
    {
        private int x, y; //Campos de clase, son accesibles desde cualquier lugar de la clase


        public Punto(int x, int y)
        {
            //Console.WriteLine("Coordenada: {0},{1}", x, y);
            this.x = x;
            this.y = y;
            contadorDeObjetos++;

        }

        // hay mayor herarqui en un constructor que un contructor con parametros definidos
        public Punto()
        {
            Console.WriteLine("Llamada desde un constructor sin parametros");
            this.x = 0;
            this.y = 0;

            contadorDeObjetos++;
        }

        public double Distancia(Punto otroPunto) //Composicion: utilizar una clase dentro de otra
        {
            int xDif = this.x - otroPunto.x;
            int yDif = this.y - otroPunto.y;
            return Math.Sqrt(Math.Pow(xDif, 2) + Math.Pow(yDif, 2));
        }

        /* Variable STATIC */
        // ninguna es poseedora de la clase de la propiedad mas que las misma clase donde fue declarada
        // no se pueden modificar cuando se llama
        private static int contadorDeObjetos = 0;

        // todos los valores constantes son estaticas
        private const int valorConstante = 10;

        // el unico objetivo del metodo estatico es poder obtener su valor
        public static int ContadorDeObjetos() => contadorDeObjetos;
        
    }
}
