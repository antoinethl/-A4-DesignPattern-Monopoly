using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Dice : ISubject
    {
        int die1;
        int die2;
        List<IObserver> _observers = new List<IObserver>();

        public int Die2 { get => die2; }
        public int Die1 { get => die1; }

        public Dice()
        {
        }

        public int GetValue()
        {
            return die1 + die2;
        }

        public bool IsDouble()
        {
            return die1 == die2;
        }

        public void ThrowDice()
        {
            Random rnd = new Random();
            die1 = rnd.Next(1, 7);
            die2 = rnd.Next(1, 7);
            if (die1 == die2)
            {
                Notify();
            }
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        private void Notify()
        {
            foreach(var obs in _observers)
            {
                obs.Update();
            }
        }
    }
}
