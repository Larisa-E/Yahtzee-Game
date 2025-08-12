using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class YahtzeeGame // the main game logic class
    {
        private readonly ScoreCard _scoreCard = new(); // create a score card
        private readonly DiceSet _diceSet = new(); // create a set of dice to maqnage dice

        public void Start() // run the game
        {
            for (int round = 1; round <= 15; round++) // loop for 15 rounds
            {
                _diceSet.RollAllDice(); // Roll all dice at the start of each round
                int rolls = 1; // initialize the roll counter

                while (rolls < 3) // allow up to 3 rolls per turn
                {
                    Console.WriteLine($"\nRoll {rolls}: {_diceSet}"); // display the current roll of dice
                    Console.WriteLine("Choose dice to keep (e.g., 1,3,5) or press Enter to roll all dice again:");
                    string? input = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(input)) break; // if input is empty, roll all dice again

                    _diceSet.Reroll(input); // Reroll dice not kept
                    rolls++;
                }

                Console.WriteLine($"\nFinal roll: {_diceSet}");
                _scoreCard.ChooseCategory(_diceSet.Values); // let the player to choose a category
                _scoreCard.PrintScoreCard(); // print the current scorecard
            }   

            Console.WriteLine("Thanks for playing Yahtzee!");
        }
    }
}
