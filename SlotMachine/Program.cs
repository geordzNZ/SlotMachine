using System.Linq;

namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX_VALUE = 3;
        const int START_MONEY_VALUE = 5;
        const string DIVIDER = "================================================================";
        const string BLANKER = "                                                                ";
        static void Main(string[] args)
        {
            // Game set up, defaulted for now, but could be made user input
            int linesPlayed = 1; 
            int lineStake = 1;   

            // Output Header section
            Console.Clear();
            Console.WriteLine("\t\t\tWELCOME");
            Console.WriteLine("\t\t   Slot Machine Game");
            Console.WriteLine($"\t    Wager on the middle horizontal row");
            Console.WriteLine($"\t    You start with {START_MONEY_VALUE} monies - 1 money per line");
            Console.WriteLine($"{DIVIDER}\n");

            // Generate slot grid data
            int[,] slotGrid = new int[3, 3];
            
            for (int i=0; i<3;i++)
            {
                for (int j=0; j<3; j++)
                {
                    Random randomSlot = new Random();
                    int randomSlotValue = randomSlot.Next(SLOTS_MAX_VALUE);
                    slotGrid[i, j] = randomSlotValue;
                    //Console.Write($"{randomSlotValue} ");
                }
            }

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
                Console.WriteLine($"\t\t{String.Join(' ', rowDetailChars)}");
            }

            // Check middle row for matches
            bool win = false;
            int winLossAmount = lineStake * linesPlayed;

            if (slotGrid[1,0] == slotGrid[1, 1] && slotGrid[1, 0] == slotGrid[1, 2])
            {
                win = true;
            }
            else
            {
                winLossAmount *= -1;
            }

            // Output win / loss outcome and sign off
            if (win)
            {
                Console.WriteLine($"\n\tMIDDLE ROW MATCH - CONGRATULATIONS!!!");
            }
            else
            {
                Console.WriteLine($"\n\tNO MATCHES THIS ROUND!!!");
            }
            Console.WriteLine($"\tYou now have {START_MONEY_VALUE + winLossAmount} monies");
            Console.WriteLine($"\n\n\tThanks for playing!");
        }
    }
}