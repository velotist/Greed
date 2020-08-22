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
        private int[] arrayOfDice;

        private int DiceEyes()
        {
            int eyes = random.Next(1, 7);

            return eyes;
        }

        public int[] FillDiceCupWithDice(int numberOfDice)
        {
            try
            {
                arrayOfDice = new int[numberOfDice];

                for (int i = 0; i < numberOfDice; i++)
                {
                    arrayOfDice[i] = DiceEyes();
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine(arrayOfDice);
                Console.WriteLine("Error when {0} dice left.", numberOfDice);
            }
            
            return arrayOfDice;
        }
    }
}