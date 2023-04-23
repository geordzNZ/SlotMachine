namespace SlotMachine
{
    internal class Program
    {
        const string DIVIDER = "================================================================";
        const string BLANKER = "                                                                ";
        static void Main(string[] args)
        {
            // Header section
            Console.Clear();
            Console.WriteLine("\t\t\tWELCOME");
            Console.WriteLine("\t\tSlot Machine Game");
            Console.WriteLine($"\t\tWager on 1 or multiple lines");
            Console.WriteLine($"{DIVIDER}\n");

            // Generate slot grid
            int[,] slotGrid = new int[3, 3];
            
            for (int i=0; i<3;i++)
            {
                for (int j=0; j<3; j++)
                {
                    Random randomSlot = new Random();
                    int randomSlotValue = randomSlot.Next(4);
                    slotGrid[i, j] = randomSlotValue;
                    Console.Write($"{randomSlotValue} ");
                }
            }
            Console.WriteLine($"\n\n{DIVIDER}\n\n");
        }
    }
}