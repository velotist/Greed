using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greed
{
    public class Dice
    {
        private Random random = new Random();
        public int Eyes { get; set; }

        private int DiceEyes()
        {
            int eyes = random.Next(1, 7);

            return eyes;
        }

        public int[] FillDiceCupWithDices(int amountOfDices)
        {
            int[] dices = new int[amountOfDices];

            for (int i = 0; i < amountOfDices; i++)
            {
                dices[i] = DiceEyes();
            }

            return dices;
        }
    }
}