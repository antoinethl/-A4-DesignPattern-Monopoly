using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    enum State { free, inJail }

    class Pawn
    {
        public string name;
        State state;

        public State State { get => state; }

        public Pawn(string s)
        {
            state = 0;
            name = s;
        }      

        public void ChangeState()
        {
            if (state == State.free)
            {
                state = State.inJail;
                Jail.GetInstance().AddInJail(this);
            }
            else
            {
                state = State.free;
                Jail.GetInstance().RemoveFromJail(this);
            }
        }

        public bool IsFree()
        {
            return state == State.free;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
