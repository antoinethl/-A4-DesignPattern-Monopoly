using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {

        static void Init()
        {
            
        }

        static void Main(string[] args)
        {
            List<Pawn> l = new List<Pawn>();
            Pawn boat = new Pawn("Boat");
            Pawn hat = new Pawn("Hat");
            l.Add(boat);
            l.Add(hat);
            MonopolyGame monopolyGame = new MonopolyGame(l);
            Console.ReadKey();
        }
    }
}
