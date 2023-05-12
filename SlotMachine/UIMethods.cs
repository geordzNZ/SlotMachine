using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public static class UIMethods
    {
        const string DIVIDER = "======================================================================================";
        const string BLANKER = "                                                                                      ";
        const string POSSIBLE_LINE_OPTIONS = "tmblcrdu";

        /// <summary>
        /// Outputs Welcome game header text
        /// </summary>
        public static void DisplayHeader()
        {
            Console.Clear();
            Console.WriteLine("\t\t\tWELCOME");
            Console.WriteLine("\t\t   Slot Machine Game\n");
        }

        /// <summary>
        /// Outputs game instruction text
        /// </summary>
        public static void DisplayInstructions()
        {
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
        }

        /// <summary>
        /// Outputs current geoz section
        /// </summary>
        /// <param name="geoz">The current geoz value</param>
        public static void DisplayCurrentGeoz(int geoz)
        {
            Console.WriteLine($"\t\tYou have {geoz} geoz");
            Console.WriteLine($"{DIVIDER}\n");
        }

        /// <summary>
        /// Prompts user to select line options and validates input
        /// </summary>
        /// <param name="geoz">The current geoz value</param>
        /// <returns>validated string of the user entered line options</returns>
        public static string GetLineSelection(int geoz)
        {
            // Game option input and check amount wagered
            string lineSelection = "";
            do
            {

                Console.WriteLine($"\tChoose lines based on Game Control Menu above...");
                Console.Write($"\tLine selection: ");
                string lineSelectionInput = Console.ReadLine();

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
                if (geoz < lineSelection.Length)
                {
                    UIMethods.DisplayInsufficientGeoz(geoz);
                    lineSelection = "";
                    continue;
                }

                // All good then exit loop
                break;

            } while (true);  // Game option input loop;




            return lineSelection;
        }

        /// <summary>
        /// Output the slot grid to the screen
        /// </summary>
        /// <param name="slotGrid">the array representing the full slotGrid to be displayed</param>
        public static void DisplaySlotGrid(int[,] slotGrid)
        //public static void DisplaySlotGrid(string topRow, string middleRow, string bottomRow)
        {
            Console.WriteLine($"\n\t\tCurrent Spin");
            for (int row = 0; row < slotGrid.GetLength(0); row++)
            {
                string line = "";
                for (int col = 0; col < slotGrid.GetLength(1); col++)
                {
                    line += slotGrid[row, col];
                }
                Console.Write($"\t\t  {String.Join(' ', line.ToCharArray())}\n");
            }
        }

        /// <summary>
        /// Output message showing if there are winning lines or not, and bonus 
        /// </summary>
        /// <param name="wins">the number of wins calculated from main logic</param>
        /// <param name="played">the number of lines played, used to display bonus message</param>
        public static void DisplayMatchesMessage(int wins, int played)
        
        {

            if (wins > 0)
            {
                Console.WriteLine($"\n\tWINNER WINNER - {wins} MATCHED - {wins * 2}g PAYOUT!!!");
                if (wins == played)
                {
                    Console.WriteLine($"\tALL LINES WON             - {wins}g BONUS PAYOUT!!!");
                }
            }
            else
            {
                Console.WriteLine($"\n\tNO MATCHES THIS SPIN!!!");
            }
        }

        /// <summary>
        /// Gets user input to play again and validates answer
        /// </summary>
        /// <returns>validated answer as a char</returns>
        public static char GetPlayAgainAnswer()
        {
            char playAgainAnswer = ' ';
            do
            {
                // Get user input
                //ConsoleKeyInfo playAgainInput = UI_Methods.GetYesNoAnswer("");
                Console.Write($"\n\tDo you what to play again? (y/n): ");
                ConsoleKeyInfo playAgainInput = Console.ReadKey();

                if ((int)playAgainInput.KeyChar == 110 || (int)playAgainInput.KeyChar == 121) // only allow - n or y
                {
                    playAgainAnswer = char.Parse(playAgainInput.KeyChar.ToString());
                    break;
                }
            } while (true);  // Play again loop

            return playAgainAnswer;
        }

        /// <summary>
        /// Output message when user wagers more then they have left.
        /// </summary>
        /// <param name="geoz">the current amount of geoz</param>
        public static void DisplayInsufficientGeoz(int geoz)
        {
            Console.WriteLine($"\n\t\tNot enough geoz ... choose upto {geoz} lines!");
        }

        /// <summary>
        /// Output appropriate message if user is leaving with or with out geoz
        /// </summary>
        /// <param name="geoz">Amount of geoz to control which message is displayed</param>
        public static void DisplayLeavingMessage(int geoz)
        {
            if (geoz == 0)
            {
                Console.WriteLine($"\n\n\tSorry ... not enough geoz for more games ...");
                Console.WriteLine($"\n\n\tCalling the bouncers to escort you out of the building!)";
            }
            else
            {
                Console.WriteLine($"\n\n\tDont forget to pick up all your winnings ...");
                Console.WriteLine($"\n\n\tThanks for playing, see you real soon!!");
            }
            Console.WriteLine($"\n{DIVIDER}\n");
        }
    }
}