using System.IO;
using System;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AOC - Day 3");

            int[,] slopes = new int[,] { { 1, 1 }, { 3, 1 }, { 5, 1 }, { 7, 1 }, { 1, 2 } };

            ulong treeMult = 1;

            for (int slopeIdx = 0; slopeIdx < slopes.Length / slopes.Rank; slopeIdx++)
            {
                int slopeR = slopes[slopeIdx, 0];
                int slopeD = slopes[slopeIdx, 1];

                ulong treeCounter = 0;
                int xPos = 0;
                int yPos = 0;

                foreach (string line in File.ReadAllLines("input.txt"))
                {
                    char[] lineChars = line.ToCharArray();
                    if (yPos != 0 && yPos % slopeD == 0)
                    {
                        xPos += slopeR;
                        if (lineChars[xPos % lineChars.Length] == '#')
                        {
                            treeCounter++;
                        }
                    }
                    yPos += 1;
                }
                treeMult *= treeCounter;
                Console.WriteLine("Slope " + slopeR + ":" + slopeD + " Trees: " + treeCounter);
            }

            Console.WriteLine("Tree multi: " + treeMult);
        }
    }
}
