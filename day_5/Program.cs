using System.Linq;
using System.Collections.Generic;
using System.IO;
using System;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AOC Day 5");

            List<int> seatIds = new List<int>();

            foreach (string boardingPass in File.ReadAllLines("input.txt"))
            {
                // F lower half
                // B upper half
                int row = getBinary(boardingPass.Substring(0, 7), 'F', 'B', 0, 127);
                // L lower half
                // R upper half
                int col = getBinary(boardingPass.Substring(7, 3), 'L', 'R', 0, 7);
                //
                seatIds.Add(row * 8 + col);
            }

            seatIds.Sort();

            Console.WriteLine("Highest Seat ID: " + seatIds[seatIds.Count - 1]);

            int lastSeat = seatIds[0];
            foreach (int seatId in seatIds)
            {
                if (lastSeat != seatId && lastSeat + 1 != seatId)
                {
                    Console.WriteLine("MISSING SEAT " + (lastSeat + 1).ToString());
                }
                lastSeat = seatId;
            }

            return;
        }

        static int getBinary(string boardingPassRow, char lower, char upper, int startP, int endP)
        {
            int end = endP;
            int start = startP;

            foreach (char cE in boardingPassRow.ToCharArray())
            {
                int distance = end - start;
                if (distance == 1)
                {
                    distance = 2;
                }
                if (cE == lower)
                {
                    end = end - (int)Math.Round((float)distance / 2F, MidpointRounding.ToEven);
                }
                else if (cE == upper)
                {
                    start = start + (int)Math.Round((float)distance / 2F, MidpointRounding.AwayFromZero);
                }
                if (start == end)
                {
                    break;
                }
            }
            return end;
        }
    }
}
