﻿using System.Linq;
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

        static void Main(string[] args)
        {

            // Game set up
            Random randomSlot = new Random();
            int geozAmount = START_MONEY_VALUE;
            char playAgainAnswer = ' ';
            
            do
            {
                // Set up and make grid
                int winCounter = 0;
                int[,] slotGrid = new int[GRID_ROWS, GRID_COLUMNS];

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
                string lineSelection = UI_Methods.GetLineSelection(geozAmount);

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


                //  - Output win / loss outcome and current geoz
                UI_Methods.DisplayMatchesMessage(winCounter);
                UI_Methods.DisplayCurrentGeoz(geozAmount);


                // Play again user input
                playAgainAnswer = UI_Methods.GetPlayAgainAnswer();

            } while (playAgainAnswer == 'y' && geozAmount > 0);  // Game Loop end

            //  Final out put message
            UI_Methods.DisplayLeavingMessage(geozAmount);
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