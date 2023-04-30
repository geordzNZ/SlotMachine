using System.Linq;

namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX_VALUE = 3;
        const int START_MONEY_VALUE = 5;
        const int GRID_ROWS = 3;
        const int GRID_COLUMNS = 3;
        const string DIVIDER = "================================================================";
        const string BLANKER = "                                                                ";

        static void Main(string[] args)
        {
            // Game set up, defaulted for now, but could be made user input
            int linesPlayed = 1;
            int lineStake = 1;
            int moniesAmount = START_MONEY_VALUE;

            char playAgainAnswer = ' ';
            do
            {
                // Output Header section
                Console.Clear();
                Console.WriteLine("\t\t\tWELCOME");
                Console.WriteLine("\t\t   Slot Machine Game");
                Console.WriteLine($"\t    Wager on the middle horizontal row - 1 money per line");
                Console.WriteLine($"\t    You currrently have {moniesAmount} monies");
                Console.WriteLine($"{DIVIDER}\n");

                // Generate slot grid data
                int[,] slotGrid = new int[GRID_ROWS, GRID_COLUMNS];
                Random randomSlot = new Random();

                for (int generateRowCounter = 0; generateRowCounter < GRID_ROWS; generateRowCounter++)
                {
                    for (int generateColumnCounter = 0; generateColumnCounter < GRID_COLUMNS; generateColumnCounter++)
                    {
                        int randomSlotValue = randomSlot.Next(SLOTS_MAX_VALUE);
                        slotGrid[generateRowCounter, generateColumnCounter] = randomSlotValue;
                        //Console.Write($"{randomSlotValue} ");
                    }
                }

                // Output slot grid on screen
                for (int outputRowCounter = 0; outputRowCounter < GRID_ROWS; outputRowCounter++)
                {
                    string rowDetail = "";
                    for (int outputColumnCounter = 0; outputColumnCounter < GRID_COLUMNS; outputColumnCounter++)
                    {
                        rowDetail += slotGrid[outputRowCounter, outputColumnCounter];
                    }
                    char[] rowDetailChars = rowDetail.ToArray();
                    //Console.WriteLine($"Row {k} is {String.Join(' ', rowDetailChars)}");
                    Console.WriteLine($"\t\t{String.Join(' ', rowDetailChars)}");
                }

                // Check middle row for matches
                bool win = false;
                int winLossAmount = lineStake * linesPlayed;

                if (slotGrid[1, 0] == slotGrid[1, 1] && slotGrid[1, 0] == slotGrid[1, 2])
                {
                    win = true;
                    moniesAmount += winLossAmount;
                }
                else
                {
                    moniesAmount -= winLossAmount;
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

                Console.WriteLine($"\tYou now have {moniesAmount} monies");
                Console.WriteLine($"{DIVIDER}\n");

                // Play again loop
                do
                {
                    Console.Write($"\n\tDo you what to play again? (y/n): ");
                    ConsoleKeyInfo playAgainInput = Console.ReadKey();

                    if ((int)playAgainInput.KeyChar == 110 || (int)playAgainInput.KeyChar == 121) // n or y
                    {
                        playAgainAnswer = char.Parse(playAgainInput.KeyChar.ToString());
                        break;
                    }
                } while (true);  // Play again loop

            } while (playAgainAnswer == 'y' && moniesAmount > 0);  // Game Loop end

            //  Final out put message
            if (moniesAmount == 0)
            {
                Console.WriteLine($"\n\n\tSorry ... not enough monies for more games ...");
                Console.WriteLine($"\tCalling the bouncers to escort you out of the building!");
            }
            else
            {
                Console.WriteLine($"\n\n\tDont forget to pick up all your winnings ...");
                Console.WriteLine($"\tThanks for playing, see you real soon!!");
            }
            Console.WriteLine($"{DIVIDER}\n");
        }
    }
}