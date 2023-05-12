using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public static class LogicMethods
    {
        
        /// <summary>
        /// Generate slot grid
        /// </summary>
        /// <param name="rows">number of grid rows</param>
        /// <param name="cols">number of grid columns</param>
        /// <param name="slotMaxValue">used as upper limit in random number generate</param>
        /// <returns>the completed slot grid array</returns>
        public static int[,] GenerateSlotGrid(int rows, int cols)
        {
            Random randomSlot = new Random();
            int[,] tempSlotGrid = new int[rows, cols];
            
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int randomSlotValue = randomSlot.Next(Program.SLOTS_MAX_VALUE);
                    tempSlotGrid[row, col] = randomSlotValue;
                }
            }
            return tempSlotGrid;
        }


        /// <summary>
        /// processes wagered lines for wins
        /// </summary>
        /// <param name="lineSelection">user selected, validated lines</param>
        /// <param name="slotGrid">the slot grid to check against</param>
        /// <returns>number of winning lines</returns>
        public static int CheckWinningLines(string lineSelection, int[,] slotGrid)
        {
            int winCounter = 0;
            foreach(char lineChoice in lineSelection) // t/m/b/l/c/r/d/u
                {
                switch (lineChoice)
                {
                    case 't':
                        winCounter += LogicMethods.LineWinnings(slotGrid[0, 0], slotGrid[0, 1], slotGrid[0, 2]);
                        break;
                    case 'm':
                        winCounter += LogicMethods.LineWinnings(slotGrid[1, 0], slotGrid[1, 1], slotGrid[1, 2]);
                        break;
                    case 'b':
                        winCounter += LogicMethods.LineWinnings(slotGrid[2, 0], slotGrid[2, 1], slotGrid[2, 2]);
                        break;
                    case 'l':
                        winCounter += LogicMethods.LineWinnings(slotGrid[0, 0], slotGrid[1, 0], slotGrid[2, 0]);
                        break;
                    case 'c':
                        winCounter += LogicMethods.LineWinnings(slotGrid[0, 1], slotGrid[1, 1], slotGrid[2, 1]);
                        break;
                    case 'r':
                        winCounter += LogicMethods.LineWinnings(slotGrid[0, 2], slotGrid[1, 2], slotGrid[2, 2]);
                        break;
                    case 'd':
                        winCounter += LogicMethods.LineWinnings(slotGrid[0, 0], slotGrid[1, 1], slotGrid[2, 2]);
                        break;
                    case 'u':
                        winCounter += LogicMethods.LineWinnings(slotGrid[2, 0], slotGrid[1, 1], slotGrid[0, 2]);
                        break;
                }  // End switch (lineChoice) ...
            }  //  End foreach (char lineChoice...
            return winCounter;
        }


        /// <summary>
        /// Takes 3 paramaters and checks if they match
        /// </summary>
        /// <param name="num1">slot 1 value to check against others</param>
        /// <param name="num2">slot 2 value to check against others</param>
        /// <param name="num3">slot 3 value to check against others</param>
        /// <returns>Returns 1 if matching, to indicate winning line, 0 is losing line</returns>
        public static int LineWinnings(int num1, int num2, int num3)
        {
            if (num1 == num2 && num1 == num3)
            {
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Calculate new geoz about based on winning lines / payout and if bonus qualifies
        /// </summary>
        /// <param name="wins">the number of winning lines</param>
        /// <param name="played">the number of lines played</param>
        /// <returns></returns>
        public static int AllocateWinnings(int wins, int played)
        {
            int total = 0;
            if (wins > 0)
            {
                total += (wins * Program.WIN_AMOUNT);

                if (wins == played)
                {
                    total += wins;
                }
            }
            return total;
        }
    
    }
}
