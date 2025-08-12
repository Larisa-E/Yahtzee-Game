using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee
{
    public class ScoreCard
    {
        private readonly int?[] _scores = new int?[17];
        private static readonly string[] value = [
            "1'ere",
            "2'ere",
            "3'ere",
            "4'ere",
            "5'ere",
            "6'ere",
            "SUM",
            "Bonus",
            "1 par",
            "2 par",
            "3 ens",
            "4 ens",
            "Lille straight",
            "Stor straight",
            "Hus",
            "Chance",
            "YATZY"
        ];
        private readonly string[] _categories =
        value;

        public int TotalScore => _scores.Where(s => s.HasValue).Sum(s => s ?? 0); // Total score including bonus
        private static readonly int[] second = [1, 2, 3, 4, 5];
        private static readonly int[] secondArray = [2, 3, 4, 5, 6];

        public void ChooseCategory(int[] dice)
        {
            Console.WriteLine("Choose a categorie to score:");
            // Display available categories
            for (int i = 0; i < _categories.Length; i++)
            {
                _scores[6] = _scores.Take(6).Where(s => s.HasValue).Sum(s => s ?? 0);
                if (i == 6 || i == 7) continue; // skip Sum and Bonus
                if (!_scores[i].HasValue)
                Console.WriteLine($"{i + 1}: {_categories[i]}");
            }

            int cat;
            // Read user input and validate
            while (!int.TryParse(Console.ReadLine(), out cat) || cat < 1 || cat > _categories.Length || _scores[cat - 1].HasValue || cat == 7 || cat == 8)
            {                 
                Console.WriteLine("Invalid choice. Please choose a valid category.");
            }
            _scores[cat - 1] = ScoreCategory(cat - 1, dice); // score the chosen category

            // calculate upper sum and bonus
            _scores[6] = _scores.Take(6).Where(s => s.HasValue).Sum(s => s ?? 0);
            _scores[7] = _scores[6] >= 63 ? 50 : 0; // bonus if upper sum is 63 or more
        }

        public void PrintScoreCard()
        {
            Console.WriteLine("\nScore Card:");
            for (int i = 0; i < _categories.Length; i++)
            {
                if (i == 6 || i == 7) continue; // skip Sum and Bonus
                Console.WriteLine($"{_categories[i],-15}: {_scores[i] ?? 0}");
            }
        }

        // Mark ScoreCategory as static since it does not access instance data
        private static int ScoreCategory(int category, int[] dice)
        {
            var groups = dice.GroupBy(d => d).ToDictionary(g => g.Key, g => g.Count());
            switch (category)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    return dice.Where(d => d == category + 1).Sum();
                case 8:
                    return groups.Where(g => g.Value >= 2).Select(g => g.Key * 2).DefaultIfEmpty(0).Max();
                case 9:
                    var pairs = groups.Where(g => g.Value >= 2).OrderByDescending(g => g.Key).Take(2).ToArray();
                    return pairs.Length == 2 ? pairs[0].Key * 2 + pairs[1].Key * 2 : 0;
                case 10:
                    return groups.Where(g => g.Value >= 3).Select(g => g.Key * 3).DefaultIfEmpty(0).Max();
                case 11:
                    return groups.Where(g => g.Value >= 4).Select(g => g.Key * 4).DefaultIfEmpty(0).Max();
                case 12:
                    return dice.OrderBy(x => x).SequenceEqual(second) ? 15 : 0;
                case 13:
                    return dice.OrderBy(x => x).SequenceEqual(secondArray) ? 20 : 0;
                case 14:
                    return groups.ContainsValue(3) && groups.ContainsValue(2) ? dice.Sum() : 0;
                case 15:
                    return dice.Sum();
                case 16:
                    return groups.ContainsValue(5) ? 50 : 0;
                default:
                    return 0;
            }
        }
    }
}
