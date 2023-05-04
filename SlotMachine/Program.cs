using System.Linq;

namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX_VALUE = 3;
        const int START_MONEY_VALUE = 15;
        const int WIN_AMOUNT = 2;
        const int GRID_ROWS = 3;
        const int GRID_COLUMNS = 3;
        const string DIVIDER = "==========================================================================";
        const string BLANKER = "                                                                          ";
        const string POSSIBLE_LINE_OPTIONS = "tmblcrdu";

        static void Main(string[] args)
        {

            // Game set up
            int geozAmount = START_MONEY_VALUE;

            char playAgainAnswer = ' ';
            do
            {
                // Set up and make grid
                int winCounter = 0;
                int[,] slotGrid = new int[GRID_ROWS, GRID_COLUMNS];

                Random randomSlot = new Random();
                for (int row = 0; row < GRID_ROWS; row++)
                {
                    for (int col = 0; col < GRID_COLUMNS; col++)
                    {
                        int randomSlotValue = randomSlot.Next(SLOTS_MAX_VALUE);
                        slotGrid[row, col] = randomSlotValue;
                    }
                }

                // Store line representations
                string topRow = Convert.ToString(slotGrid[0, 0]) + Convert.ToString(slotGrid[0, 1]) + Convert.ToString(slotGrid[0, 2]);
                string middleRow = Convert.ToString(slotGrid[1, 0]) + Convert.ToString(slotGrid[1, 1]) + Convert.ToString(slotGrid[1, 2]);
                string bottomRow = Convert.ToString(slotGrid[2, 0]) + Convert.ToString(slotGrid[2, 1]) + Convert.ToString(slotGrid[2, 2]);

                string leftColumn = Convert.ToString(slotGrid[0, 0]) + Convert.ToString(slotGrid[1, 0]) + Convert.ToString(slotGrid[2, 0]);
                string centreColumn = Convert.ToString(slotGrid[0, 1]) + Convert.ToString(slotGrid[1, 1]) + Convert.ToString(slotGrid[2,1]);
                string rightColumn = Convert.ToString(slotGrid[0, 2]) + Convert.ToString(slotGrid[1, 2]) + Convert.ToString(slotGrid[2, 2]);
                
                string downDiagonal = Convert.ToString(slotGrid[0, 0]) + Convert.ToString(slotGrid[1, 1]) + Convert.ToString(slotGrid[2, 2]);
                string upDiagonal = Convert.ToString(slotGrid[2, 0]) + Convert.ToString(slotGrid[1, 1]) + Convert.ToString(slotGrid[0, 2]);

                // Output Header section
                Console.Clear();
                Console.WriteLine("\t\t\tWELCOME");
                Console.WriteLine("\t\t   Slot Machine Game\n");

                Console.WriteLine($"\tInstructions:");
                Console.WriteLine($"\t   * Line selection menu:");
                Console.WriteLine($"\t\tHorizontals:\tt = Top / m = Middle / b = Bottom");
                Console.WriteLine($"\t\tVerticals:\tl = Left / c = Centre / r = Right");
                Console.WriteLine($"\t\tDiagonals:\td = Down (Top Left->Bottom Right) / u = Up (Bottom Left->Top Right)");
                Console.WriteLine($"\t\t...ie to play the top / left / down / right lines  ... enter tldr");
                Console.WriteLine($"\tCosts and Winnings");
                Console.WriteLine($"\t   * Lines cost 1 geo(g) each");
                Console.WriteLine($"\t\t...ie tldr = 4 lines ... costs 4 geoz");
                Console.WriteLine($"\t   * Winning Lines pay out 2 geoz(g) each");
                Console.WriteLine($"\t\t...ie td = 2 matching lines ... pays out 4 geoz");
                Console.WriteLine($"\t   * BONUS pay out if ALL lines you play win, pays out your initial wager");
                Console.WriteLine($"\t\t...ie tldr = 4 lines ... pays out your initial 4 geoz wager");
                Console.WriteLine($"{DIVIDER}");

                Console.WriteLine($"\t\tYou currrently have {geozAmount} geoz");
                Console.WriteLine($"{DIVIDER}\n");

                // Game option input and check amount wagered
                string lineSelectionInput = "";
                string lineSelection = "";
                do
                {
                    Console.WriteLine($"\n\tChoose lines based on Game Control Menu above.");
                    Console.Write($"\tLine selection: ");
                    lineSelectionInput = Console.ReadLine();
                    
                    // Validation of user inputs
                    if (lineSelectionInput.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (char lineInput in lineSelectionInput)
                        {
                            //Console.WriteLine($"Input char is {lineInput}");
                            if (POSSIBLE_LINE_OPTIONS.Contains(lineInput))
                            {
                                if (!lineSelection.Contains(lineInput))
                                {
                                    lineSelection += lineInput;
                                }
                            }
                        }
                    }

                    // Confirm enough geoz to cover validated line choices
                    if (geozAmount < lineSelection.Length)
                    {
                        Console.WriteLine($"\n\t\tNot enough geoz ... choose upto {geozAmount} lines!");
                        lineSelection = "";
                        continue;
                    }

                    // All good then exit loop
                    break;

                } while (true);  // Game option input loop;

                // Adjust geoz amount for lines played
                geozAmount -= lineSelection.Length;

                // Check for matches
                foreach (char lineChoice in lineSelection) // t/m/b/l/c/r/d/u
                {
                    switch (lineChoice)
                    {
                        case 't':
                            if (topRow[0] == topRow[1] && topRow[0] == topRow[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'm':
                            if (middleRow[0] == middleRow[1] && middleRow[0] == middleRow[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'b':
                            if (bottomRow[0] == bottomRow[1] && bottomRow[0] == bottomRow[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'l':
                            if (leftColumn[0] == leftColumn[1] && leftColumn[0] == leftColumn[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'c':
                            if (centreColumn[0] == centreColumn[1] && centreColumn[0] == centreColumn[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'r':
                            if (rightColumn[0] == rightColumn[1] && rightColumn[0] == rightColumn[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'd':
                            if (downDiagonal[0] == downDiagonal[1] && downDiagonal[0] == downDiagonal[2])
                            {
                                winCounter++;
                            }
                            break;
                        case 'u':
                            if (upDiagonal[0] == upDiagonal[1] && upDiagonal[0] == upDiagonal[2])
                            {
                                winCounter++;
                            }
                            break;
                    }  // End switch (lineChoice) ...
                }  //  End foreach (char lineChoice...

                // Adjust geoz amount for wins (and bonus if applicable)
                if (winCounter > 0)
                {
                    geozAmount += (winCounter * WIN_AMOUNT);

                    if (winCounter == lineSelection.Length)
                    {
                        geozAmount += winCounter;
                    }
                }

                // Output section
                //  - Slot grid on screen
                Console.WriteLine($"\n\t\tCurrent Spin");
                Console.Write($"\t\t  {String.Join(' ', topRow.ToCharArray())}\n");
                Console.Write($"\t\t  {String.Join(' ', middleRow.ToCharArray())}\n");
                Console.Write($"\t\t  {String.Join(' ', bottomRow.ToCharArray())}\n");

                //  - Output win / loss outcome and sign off
                if (winCounter > 0)
                {
                    Console.WriteLine($"\n\tWINNER WINNER - CONGRATULATIONS!!!");
                    Console.WriteLine($"\t{winCounter} MATCHED!!!");
                }
                else
                {
                    Console.WriteLine($"\n\tNO MATCHES THIS SPIN!!!");
                }

                Console.WriteLine($"\tYou now have {geozAmount} geoz");
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

            } while (playAgainAnswer == 'y' && geozAmount > 0);  // Game Loop end

            //  Final out put message
            if (geozAmount == 0)
            {
                Console.WriteLine($"\n\n\tSorry ... not enough geoz for more games ...");
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