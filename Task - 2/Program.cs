namespace Task___2
{
    internal class Program
    {
        static void PrintMainMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("P - Print Numbers. ");
            Console.WriteLine("A - Add A Number. ");
            Console.WriteLine("M - Display Mean Of The Numbers. ");
            Console.WriteLine("S - Display The Smallest Numbers. ");
            Console.WriteLine("L - Display The Largest Numbers. ");
            Console.WriteLine("F - Find A Number. ");
            Console.WriteLine("C - Claer The Whole List. ");
            Console.WriteLine("Q - Quit. ");
            Console.WriteLine("===============================================");
        }

        static void Main(string[] args)
        {
            
            List<int> ListNumber = new List<int>();
            PrintMainMenu();
            char EnterCharacter = ' ';

            while (EnterCharacter != 'Q')
            {
                Console.Write("Chosse Any Character : ");
                EnterCharacter = char.Parse(Console.ReadLine().ToUpper());
               
                if (EnterCharacter == 'P')
                {
                    Console.Write("Print Numbers : ");
                    if (ListNumber.Count > 0)
                    {
                        Console.WriteLine(string.Join(", ", ListNumber));
                    }
                    else
                    {
                        Console.WriteLine("The List Empty");
                    }
                }
               
                else if (EnterCharacter == 'A')
                {
                    Console.Write("Add Number : ");
                    int EnterNumber = int.Parse(Console.ReadLine());
                    ListNumber.Add(EnterNumber);
                    Console.WriteLine($"Number {EnterNumber} Is Added");
                }
               
                else if (EnterCharacter == 'C')
                {
                    Console.WriteLine("Clear The Whole List");
                    ListNumber.Clear();
                }
                else if (EnterCharacter == 'M')
                {
                    if (ListNumber.Count > 0)
                    {

                    }
                    else
                    {
                        Console.WriteLine("The List Empty , You Can First Enter Numbers");
                    }
                }          
            Console.WriteLine("===============================================");
            Console.WriteLine("Happy Ending Goodbye");
            Console.WriteLine("===============================================");






        }
    }
}
