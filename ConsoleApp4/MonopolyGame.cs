using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApp4
{
    class MonopolyGame
    {
        Dictionary<Pawn, int> number_of_turn;
        Board board;
        int max_turn;

        private void Init_MonopolyGame(List<Pawn> list_players)
        {
            board = Board.GetInstance();

            number_of_turn = new Dictionary<Pawn, int>();
            foreach (Pawn player in list_players)
            {
                number_of_turn.Add(player, 0);
            }

            KeyValuePair<Cell, List<Pawn>> kvp0 = board.Cells.FirstOrDefault(x => x.Key.CellNumber == 0);
            
            foreach(Pawn players in list_players)
            {
                board.Cells[kvp0.Key].Add(players);
            }
            max_turn = 4;
        }

        public MonopolyGame(List<Pawn> list_players)
        {
            Init_MonopolyGame(list_players);
            bool isEnded = false;          
            while (!isEnded) 
            {
                List<Thread> thread_collection = new List<Thread>();
                foreach (Pawn player in list_players.ToList())
                {
                    Console.WriteLine("\t-=+ " + player.ToString() + " turn +=-");
                    //Single_player_turn(player);
                    Thread thread = new Thread(() => Single_player_turn(player));
                    thread.Start();
                    thread_collection.Add(thread);
                    Console.WriteLine();
                }
                foreach(Thread t in thread_collection)
                {
                    t.Join();
                }
                Jail.GetInstance().AddTurnInJail();
                isEnded = Winner();
                Thread.Sleep(500);
            }
            var winner = number_of_turn.First(x => x.Value == max_turn);
            Console.WriteLine("Winner is " + winner.Key.ToString());
        }

        public void Single_player_turn(Pawn player)
        {
            Console.WriteLine("Player " + player.ToString() + " is in position " + board.FindPlayer(player).Key.CellNumber);
            bool playAgain = true;
            Obs observerForDouble = new Obs(player);
            Dice dice = new Dice();
            dice.Attach(observerForDouble);

            while (playAgain)
            {
                State starting_state = player.State;
                dice.ThrowDice();
                Console.WriteLine("\t" + player.ToString() + " did " + dice.Die1 + " and " + dice.Die2);

                if (starting_state == player.State)
                    playAgain = dice.IsDouble();
                else
                    playAgain = false;

                if (player.IsFree())
                {
                    MovePlayer(player, dice.GetValue());
                    Console.WriteLine("Player " + player.ToString() + " is now in position " + board.FindPlayer(player).Key.CellNumber);
                    if (!player.IsFree())
                        playAgain = false;
                }
                else
                {
                    Console.WriteLine("Player " + player.ToString() + " is in jail !");
                }
                
            }                                        
        }

        public void MovePlayer(Pawn player, int diceValue)
        {
            
            KeyValuePair<Cell, List<Pawn>> position = board.FindPlayer(player);
            int cell_id = ComputePosition(position.Key.CellNumber, diceValue, player);

            position.Value.Remove(player);
            KeyValuePair<Cell, List<Pawn>> kvp = board.Cells.FirstOrDefault(x => x.Key.CellNumber == cell_id);
            if (kvp.Key.CellNumber == 30)
            {
                player.ChangeState();
                kvp = board.Cells.FirstOrDefault(x => x.Key == Jail.GetInstance());
            }
            board.Cells[kvp.Key].Add(player);
        }

        public int ComputePosition(int oldPosition, int diceValue, Pawn player)
        {
            int newPosition = (oldPosition + diceValue) % 39;

            if (oldPosition + diceValue > 39)
                number_of_turn[player]++;

            return newPosition;
        }

        public bool Winner()
        {
            foreach(var v in number_of_turn)
            {
                if (v.Value >= max_turn)
                    return true;                
            }
            return false;
        }
    }
}
