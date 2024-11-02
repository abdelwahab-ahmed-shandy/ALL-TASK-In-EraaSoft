using System.Runtime.ConstrainedExecution;

namespace Task_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Estimate For Carpet Cleaning Service");
            Console.Write("How Many Small Carpets : ");
            int SmallCarpet = int.Parse(Console.ReadLine());
            
            Console.Write("How Many Large Carpets : ");
            int LargeCarpet = int.Parse(Console.ReadLine());

            const int PriceSmallCarpets = 25;
            const int PriceLargeCarpets = 35;
            //After searching,
            //I found out that the float must have an f after the value in order for it to be accepted.
            //I don't know why.
            const float Tax = 6.6f;
            Console.WriteLine("Price Per Small Room : 25$ ");
            Console.WriteLine("Price Per Large Room : 35$ ");

            int TotalCost = (SmallCarpet * PriceSmallCarpets) + (LargeCarpet * PriceLargeCarpets);
            Console.WriteLine($"Cost : {TotalCost}");
            Console.WriteLine("Tax : 6.6$");
            Console.WriteLine("=================================================================");
            Console.WriteLine($"Total Estimate : {TotalCost + Tax}");
            Console.WriteLine("This Estimate Is Valid For 30 Days");
        }
    }
}
