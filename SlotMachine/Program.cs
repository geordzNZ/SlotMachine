using System.Linq;

namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX_VALUE = 3;
        const int START_MONEY_VALUE = 5;
        const int GRID_ROWS = 3;
        const int GRID_COLUMNS = 3;
        const string DIVIDER = "==========================================================================";
        const string BLANKER = "                                                                          ";

        static void Main(string[] args)
        {
            // Game set up, defaulted for now, but could be made user input
            int linesPlayed = 0;
            //int lineStake = 1;
            int moniesAmount = START_MONEY_VALUE;

            char playAgainAnswer = ' ';
            do
            {
                // Set up for start of game
                int winCounter = 0;
                int[,] slotGrid = new int[GRID_ROWS, GRID_COLUMNS];
                Random randomSlot = new Random();

                for (int generateRowCounter = 0; generateRowCounter < GRID_ROWS; generateRowCounter++)
                {
                    for (int generateColumnCounter = 0; generateColumnCounter < GRID_COLUMNS; generateColumnCounter++)
                    {
                        int randomSlotValue = randomSlot.Next(SLOTS_MAX_VALUE);
                        slotGrid[generateRowCounter, generateColumnCounter] = randomSlotValue;
                    }
                }

                // Output Header section
                Console.Clear();
                Console.WriteLine("\t\t\tWELCOME");
                Console.WriteLine("\t\t   Slot Machine Game");
                Console.WriteLine($"\tWager on matching lines - 1 monies(m) per line");

                // Option A -- pros, less user input / cons, lots of options
                Console.WriteLine($"\tGame Control Menu:");
                Console.WriteLine($"\t\tRows:\t\ta = Middle row (1m) / b = All rows (3m)");
                Console.WriteLine($"\t\tColumns:\tc = All columns (3m)");
                Console.WriteLine($"\t\tDiagonals:\td = Both Diagonals (2m)");
                Console.WriteLine($"{DIVIDER}");

                Console.WriteLine($"\t    You currrently have {moniesAmount} monies");
                Console.WriteLine($"{DIVIDER}\n");

                // Game option input and check amount wagered
                char gameOptionAnswer = ' ';
                do
                {
                    Console.WriteLine($"\n\tChoose lines based on game menu above.");
                    Console.Write($"\tOptions (a/b/c/d): ");
                    ConsoleKeyInfo gameOptionInput = Console.ReadKey();

                    if ((int)gameOptionInput.KeyChar >= 97 && (int)gameOptionInput.KeyChar <= 100) // a / b / c / d
                    {
                        gameOptionAnswer = char.Parse(gameOptionInput.KeyChar.ToString());

                        // Assign lines played
                        switch (gameOptionAnswer)
                        {
                            case 'a':
                                linesPlayed = 1;
                                break;
                            case 'b':
                                linesPlayed = 3;
                                break;
                            case 'c':
                                linesPlayed = 3;
                                break;
                            case 'd':
                                linesPlayed = 2;
                                break;
                        }

                        if (moniesAmount < linesPlayed)
                        {
                            Console.WriteLine($"\n\t\tNot enough monies ... choose again!!");
                            continue;
                        }
                        break;
                    }
                } while (true);  // Game option input loop

 

                // Output slot grid on screen
                Console.WriteLine($"\n\n\t\tSlot Grid");
                for (int outputRowCounter = 0; outputRowCounter < GRID_ROWS; outputRowCounter++)
                {
                    string rowDetail = "";
                    for (int outputColumnCounter = 0; outputColumnCounter < GRID_COLUMNS; outputColumnCounter++)
                    {
                        rowDetail += slotGrid[outputRowCounter, outputColumnCounter];
                    }
                    char[] rowDetailChars = rowDetail.ToArray();
                    Console.Write($"\t\t  {String.Join(' ', rowDetailChars)}\n");
                }

                // Check for matches in appropriate rows
                bool win = false;

                // Row Checking section
                if (gameOptionAnswer == 'a' || gameOptionAnswer == 'b')
                {
                    // Middle row check - option a / b
                    if (slotGrid[1, 0] == slotGrid[1, 1] && slotGrid[1, 0] == slotGrid[1, 2])
                    {
                        win = true;
                        winCounter++;
                    }

                    // Other row check - option b
                    if (gameOptionAnswer == 'b' )
                    {
                        // Top row
                        if (slotGrid[0, 0] == slotGrid[0, 1] && slotGrid[0, 0] == slotGrid[0, 2])
                        {
                            win = true;
                            winCounter++;
                        }

                        // Bottom row
                        if (slotGrid[2, 0] == slotGrid[2, 1] && slotGrid[2, 0] == slotGrid[2, 2])
                        {
                            win = true;
                            winCounter++;
                        }
                    }
                }

                // Column Checking section
                if (gameOptionAnswer == 'c' )
                {
                    // Left column check - option c
                    if (slotGrid[0, 0] == slotGrid[1, 0] && slotGrid[0, 0] == slotGrid[2, 0])
                    {
                        win = true;
                        winCounter++;
                    }
                    // Centre column check - option c
                    if (slotGrid[0, 1] == slotGrid[1, 1] && slotGrid[0, 1] == slotGrid[2, 1])
                    {
                        win = true;
                        winCounter++;
                    }
                    // Right column check - option c
                    if (slotGrid[0, 2] == slotGrid[1, 2] && slotGrid[0, 2] == slotGrid[2, 2])
                    {
                        win = true;
                        winCounter++;
                    }
                }

                // Diagonals Checking section
                if (gameOptionAnswer == 'd' )
                {
                    // Top left to bottom right check - option d
                    if (slotGrid[0, 0] == slotGrid[1, 1] && slotGrid[0, 0] == slotGrid[2, 2])
                    {
                        win = true;
                        winCounter++;
                    }
                    // Top right to bottom left check - option d
                    if (slotGrid[0, 2] == slotGrid[1, 1] && slotGrid[0, 2] == slotGrid[2, 0])
                    {
                        win = true;
                        winCounter++;
                    }
                }

                // Output win / loss outcome and sign off
                if (win)
                {
                    Console.WriteLine($"\n\tWINNER WINNER - CONGRATULATIONS!!!");
                    Console.WriteLine($"\t{winCounter} MATCHED!!!");
                }
                else
                {
                    Console.WriteLine($"\n\tNO MATCHES THIS SPIN!!!");
                }

                Console.WriteLine($"\tYou now have {moniesAmount += (-linesPlayed + winCounter)} monies");
                Console.WriteLine($"{DIVIDER}\n");

                // Play again loop
                do
                {
                    Console.Write($"\n\tDo you what to play again? (y/n): ");
                    ConsoleKeyInfo playAgainInput = Console.ReadKey();

                    if ((int)playAgainInput.KeyChar == 110 || (int)playAgainInput.KeyChar == 121) // only allow - n or y
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