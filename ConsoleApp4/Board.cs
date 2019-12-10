using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4
{
    class Board
    {
        private Dictionary<Cell, List<Pawn>> cells;
        public Dictionary<Cell, List<Pawn>> Cells { get => cells; set => cells = value; }
     
        private static Board _board;
        public static Board GetInstance()
        {          
            if (_board == null)
                _board = new Board();

            return _board;
        }

        private Board()
        {
            cells = new Dictionary<Cell, List<Pawn>>();
            for (int i = 0; i <= 39; i++)
            {
                Cell cell = new Cell(i);

                if (i == 10)
                {
                    cell = Jail.GetInstance();
                }

                cells.Add(cell, new List<Pawn>());
            }
        }

        public KeyValuePair<Cell, List<Pawn>> FindPlayer(Pawn player)
        {
            KeyValuePair<Cell, List<Pawn>> playerPosition = new KeyValuePair<Cell, List<Pawn>>();

            foreach (KeyValuePair<Cell, List<Pawn>> k in cells) 
            {
                if (Array.Find(k.Value.ToArray(), x => x == player) != null)
                {
                    playerPosition = k;
                    break;
                }
            }
            return playerPosition;
        }       

        public void GoToJail(Pawn player)
        {
            player.ChangeState();
        }
    }
}
