///<authors>
/// Arya Koukia, Gurjot Mander, Gurjot Sandher
/// </authors>


using System;
using System.Collections.Generic;

namespace BetCoinWpf
{
    /* 
        * GameLogic class holds methods regarding the logic within a game.
        */
    class GameLogic
    {
        /*
         * Generates a single multiplier using custom algorithm.
         */
        public double generateOneMultiplier(int bet)
        {
            var rand = new Random();

            double multiplier = -(0.9 / (rand.NextDouble() - 1));
            Math.Round(multiplier, 1);
            if (multiplier >= 50)
            {
                multiplier = multiplier / 2;
            }
            if (multiplier < 1)
            {
                multiplier = multiplier + 0.1;
            }

            return multiplier;
        }
        /*
         * Generates multiple multipliers (Not implemented in final version)
         */
        public String generateXMultipliers(int bet)
        {
            var rand = new Random();
            List<String> output = new List<String>();


            for (int i = 0; i < 99; i++)
            {
                double multiplier = -(0.9 / (rand.NextDouble() - 1));
                Math.Round(multiplier, 1);
                if (multiplier >= 50)
                {
                    multiplier = multiplier / 2;
                }
                output.Add(multiplier.ToString("n2"));


            }

            String outputList = string.Join(", ", output);
            return outputList;
        }
    }
}
