namespace Yahtzee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Yahtzee!");

            var game = new YahtzeeGame();
            game.Start();
        }
    }
}
