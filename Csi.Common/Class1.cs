using System;
using System.Text.RegularExpressions;

namespace Csi.Common
{
    public class Game
    {
        Random rnd = new Random();

        public int Roll(string notation)
        {
            Regex diceNotationRegex = new Regex("(?<num>\\d+)?[d|D](?<side>\\d+)");

            Match m = diceNotationRegex.Match(notation);

            if (!m.Success)
                return 0;

            int num = 0;
            int side = 0;

            if (m.Groups["num"].Success)
            {
                if (!int.TryParse(m.Groups["num"].Value, out num))
                {
                    num = 1;
                }
                if (num <= 0)
                {
                    num = 1;
                }
            }
            else 
            {
                num = 1;
            }

            if (m.Groups["side"].Success)
            {
                if (!int.TryParse(m.Groups["side"].Value, out side))
                {
                    return 0;
                }
                if (side <= 0)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

            return Roll(num, side);
        }

        public int Roll(int num, int side)
        {
            int result = 0;

            for (int i = 0; i < num; i++)
            {
                result = result + rnd.Next(1, side + 1);
            }

            return result;
        }

    }
}
