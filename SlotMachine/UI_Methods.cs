using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine
{
    public static class UI_Methods
    {
        const string DIVIDER = "==========================================================================";
        const string BLANKER = "                                                                          ";

        public static void DisplayHeader()
        {
            Console.Clear();
            Console.WriteLine("\t\t\tWELCOME");
            Console.WriteLine("\t\t   Slot Machine Game\n");
        }

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

        public static void DisplayCurrentGeoz(int geozAmount)
        {
            Console.WriteLine($"\t\tYou have {geozAmount} geoz");
            Console.WriteLine($"{DIVIDER}\n");
        }

        public static string GetLineSelection()
        {
            Console.WriteLine($"\tChoose lines based on Game Control Menu above...");
            Console.Write($"\tLine selection: ");
            string lineSelectionInput = Console.ReadLine();

            return lineSelectionInput;
        }


        public static void DisplaySlotGrid(string topRow, string middleRow, string bottomRow)
        {
            Console.WriteLine($"\n\t\tCurrent Spin");
            Console.Write($"\t\t  {String.Join(' ', topRow.ToCharArray())}\n");
            Console.Write($"\t\t  {String.Join(' ', middleRow.ToCharArray())}\n");
            Console.Write($"\t\t  {String.Join(' ', bottomRow.ToCharArray())}\n");
        }


        public static void DisplayMatchesMessage(int wins)
        {
            Console.WriteLine($"\n\tWINNER WINNER - CONGRATULATIONS!!!");
            Console.WriteLine($"\t{wins} MATCHED!!!");
        }

        public static void DisplayNoMatchesMessage()
        {
            Console.WriteLine($"\n\tNO MATCHES THIS SPIN!!!");

        }


        public static ConsoleKeyInfo GetYesNoAnswer(string userPrompt)
        {
            Console.Write($"\n\t{userPrompt} (y/n): ");
            ConsoleKeyInfo playAgainInput = Console.ReadKey();
            return playAgainInput;
        }


        public static void DisplayInsufficientGeoz(int geoz)
        {
            Console.WriteLine($"\n\t\tNot enough geoz ... choose upto {geoz} lines!");
        }

        public static void DisplayLeavingMessage(string msg1, string msg2)
        {
            Console.WriteLine($"\n\n\t{msg1}");
            Console.WriteLine($"\t{msg2}");
            Console.WriteLine($"\n{DIVIDER}\n");
        }
    }
}