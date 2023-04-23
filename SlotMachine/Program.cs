using System.Linq;

namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX = 9;
        const string DIVIDER = "================================================================";
        const string BLANKER = "                                                                ";
        static void Main(string[] args)
        {
            // Header section
            Console.Clear();
            Console.WriteLine("\t\t\tWELCOME");
            Console.WriteLine("\t\t   Slot Machine Game");
            Console.WriteLine($"\t    Wager on 1 or multiple lines");
            Console.WriteLine($"{DIVIDER}\n");

            // Generate slot grid
            int[,] slotGrid = new int[3, 3];
            
            for (int i=0; i<3;i++)
            {
                for (int j=0; j<3; j++)
                {
                    Random randomSlot = new Random();
                    int randomSlotValue = randomSlot.Next(SLOTS_MAX);
                    slotGrid[i, j] = randomSlotValue;
                    //Console.Write($"{randomSlotValue} ");
                }
            }
            Console.WriteLine($"\n\n{DIVIDER}\n\n");
            // Output slot grid on screen
            for (int k = 0; k < 3; k++)
            {
                string rowDetail = "";
                for (int l = 0; l < 3; l++)
                {
                    rowDetail += slotGrid[k, l];
                }
                char[] rowDetailChars = rowDetail.ToArray();
                //Console.WriteLine($"Row {k} is {String.Join(' ', rowDetailChars)}");
                Console.WriteLine($"{String.Join(' ', rowDetailChars)}");
            }

            bool win = false;
            // Check middle row for matches
            if (slotGrid[1,0] == slotGrid[1, 1] && slotGrid[1, 0] == slotGrid[1, 2])
            {
                win = true;
            }

            if (win)
            {
                Console.WriteLine($"MIDDLE ROW MATCH - CONGRATULATIONS!!!");
            }
            else
            {
                Console.WriteLine($"NO MATCHES THIS ROUND!!!");
            }
        }
    }
}