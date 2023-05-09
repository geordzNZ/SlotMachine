using System.Linq;
using SlotMachine;



namespace SlotMachine
{
    internal class Program
    {
        const int SLOTS_MAX_VALUE = 3;
        const int START_MONEY_VALUE = 15;
        const int WIN_AMOUNT = 2;
        const int GRID_ROWS = 3;
        const int GRID_COLUMNS = 3;
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
                UI_Methods.DisplayHeader();
                UI_Methods.DisplayInstructions();
                UI_Methods.DisplayCurrentGeoz(geozAmount);

                // Game option input and check amount wagered
                string lineSelection = "";
                do
                {
                    // Get line selection from user
                    string lineSelectionInput = UI_Methods.GetLineSelection();

                    // Validation of user inputs
                    foreach (char lineInput in lineSelectionInput)
                    {
                        if (POSSIBLE_LINE_OPTIONS.Contains(lineInput))
                        {
                            if (!lineSelection.Contains(lineInput))
                            {
                                lineSelection += lineInput;
                            }
                        }
                    }

                    // Exit if no valid line chocies
                    if (lineSelection.Length == 0)
                    {
                        continue;
                    }

                    // Confirm enough geoz to cover validated line choices
                    if (geozAmount < lineSelection.Length)
                    {
                        UI_Methods.DisplayInsufficientGeoz(geozAmount);
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
                            winCounter += lineWinnings(topRow);
                            break;
                        case 'm':
                            winCounter += lineWinnings(middleRow);
                            break;
                        case 'b':
                            winCounter += lineWinnings(bottomRow);
                            break;
                        case 'l':
                            winCounter += lineWinnings(leftColumn);
                            break;
                        case 'c':
                            winCounter += lineWinnings(centreColumn);
                            break;
                        case 'r':
                            winCounter += lineWinnings(rightColumn);
                            break;
                        case 'd':
                            winCounter += lineWinnings(downDiagonal);
                            break;
                        case 'u':
                            winCounter += lineWinnings(upDiagonal);
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
                UI_Methods.DisplaySlotGrid(topRow,middleRow,bottomRow);


                //  - Output win / loss outcome and sign off
                if (winCounter > 0)
                {
                    UI_Methods.DisplayMatchesMessage(winCounter);
                }
                else
                {
                    UI_Methods.DisplayNoMatchesMessage();
                }

                UI_Methods.DisplayCurrentGeoz(geozAmount);


                // Play again loop
                do
                {
                    // Get user input
                    ConsoleKeyInfo playAgainInput = UI_Methods.GetYesNoAnswer("Do you what to play again?");

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
                string msgLoss1 = "Sorry ... not enough geoz for more games ...";
                string msgLoss2 = "Calling the bouncers to escort you out of the building!";
                UI_Methods.DisplayLeavingMessage(msgLoss1, msgLoss2);
            }
            else
            {
                string msgWin1 = "Dont forget to pick up all your winnings ...";
                string msgWin2 = "Thanks for playing, see you real soon!!";
                UI_Methods.DisplayLeavingMessage(msgWin1, msgWin2);
            }
        }

        ///// <summary>
        ///// Checks the line string for matches
        ///// </summary>
        ///// <param name="lineToCheck">The line to be cheched, as a string</param>
        ///// <returns>bool value to indicate matching line or not</returns>
        //static bool isLineWin(string lineToCheck)
        //{
        //    bool winningLine = (lineToCheck[0] == lineToCheck[1] && lineToCheck[0] == lineToCheck[2]);
        //    return winningLine;
        //}

        /// <summary>
        /// Takes line input, check if all match
        /// </summary>
        /// <param name="lineToCheck">The line to be checked, as a string</param>
        /// <returns>Returns 1 if matching, to indicate winning line, 0 is losing line</returns>
        static int lineWinnings(string lineToCheck)
        {
            if (lineToCheck[0] == lineToCheck[1] && lineToCheck[0] == lineToCheck[2])
            {
                return 1;
            }
            return 0;
         }
    }
}