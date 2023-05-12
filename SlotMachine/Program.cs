using System.Linq;
using SlotMachine;



namespace SlotMachine
{
    internal class Program
    {
        public const int SLOTS_MAX_VALUE = 3;
        public const int WIN_AMOUNT = 2;
        const int START_MONEY_VALUE = 15;
        const int GRID_ROWS = 3;
        const int GRID_COLUMNS = 3;

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
                slotGrid = LogicMethods.GenerateSlotGrid(slotGrid.GetLength(0), slotGrid.GetLength(1));

                // Output Header section
                UIMethods.DisplayHeader();
                UIMethods.DisplayInstructions();
                UIMethods.DisplayCurrentGeoz(geozAmount);

                // Game option input and check amount wagered
                string lineSelection = UIMethods.GetLineSelection(geozAmount);

                // Adjust geoz amount for lines played
                geozAmount -= lineSelection.Length;

                // Check for matches
                winCounter = LogicMethods.CheckWinningLines(lineSelection, slotGrid);

                // Adjust geoz amount for wins (and bonus if applicable)
                geozAmount += LogicMethods.AllocateWinnings(winCounter, lineSelection.Length);

                // Output section
                //  - Slot grid on screen
                //UIMethods.DisplaySlotGrid(topRow,middleRow,bottomRow);
                UIMethods.DisplaySlotGrid(slotGrid);

                //  - Output win / loss outcome and current geoz
                UIMethods.DisplayMatchesMessage(winCounter, lineSelection.Length);
                UIMethods.DisplayCurrentGeoz(geozAmount);


                // Play again user input
                playAgainAnswer = UIMethods.GetPlayAgainAnswer();

            } while (playAgainAnswer == 'y' && geozAmount > 0);  // Game Loop end

            //  Final out put message
            UIMethods.DisplayLeavingMessage(geozAmount);
        }
    }
}