using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class DiceSet
    {
        public int[] Values { get; private set; } = new int[5]; // array to hold the values of the dice

        public void RollAllDice() // method to roll all dice
        {
            for (int i = 0; i < 5; i++)
                Values[i] = RollDie();
        }

        public void Reroll(string keepInput) // method to reroll specific dice based on user input
        {
            bool[] keep = new bool[5]; // array to track which dice to keep
            foreach (var index in keepInput.Split(' '))
                if (int.TryParse(index, out int n) && n >= 1 && n <= 5)
                    keep[n - 1] = true; // mark the dice to keep

            for (int i = 0; i < 5; i++)
                if (!keep[i]) // if the die is not marked to keep, roll it
                    Values[i] = RollDie();
        }

        private static int RollDie() => Random.Shared.Next(1, 7); // method to roll a single die

        public override string ToString() => string.Join(", ", Values); // returns a string representation of the dice
    }
}
