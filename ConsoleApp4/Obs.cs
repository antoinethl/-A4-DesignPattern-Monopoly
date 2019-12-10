using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Obs : IObserver
    {
        Pawn player;
        int numberOfDouble;

        public Obs(Pawn p)
        {
            player = p;
            numberOfDouble = 0;
        }

        public void Update()
        {
            numberOfDouble++;
            if (player.IsFree())
            {
                if (numberOfDouble >= 2)
                {
                    player.ChangeState();
                }
            }
            else
            {
                player.ChangeState();
            }
            
        }
    }
}
