using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4
{
    class Jail : Cell
    {
        Dictionary<Pawn, int> inJailPlayers;

        private static Jail _jail;
        public static Jail GetInstance()
        {
            if (_jail == null)
                _jail = new Jail(10);

            return _jail;
        }

        private Jail(int i) : base(i)
        {
            inJailPlayers = new Dictionary<Pawn, int>();
        }

        public void AddTurnInJail()
        {
            var item = inJailPlayers.Where(x => x.Value >= 2).ToArray();
            foreach (var v in item)
            {
                inJailPlayers.Remove(v.Key);
                v.Key.ChangeState();
            }
            foreach(var x in inJailPlayers.ToList())
            {
                inJailPlayers[x.Key]++;
            }
        }

        public void AddInJail(Pawn player)
        {
            inJailPlayers.Add(player, 0);
        }

        public void RemoveFromJail(Pawn player)
        {
            inJailPlayers.Remove(player);
        }
    }
}
