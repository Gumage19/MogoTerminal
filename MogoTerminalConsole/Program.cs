using System;
using MogoTerminal;

namespace MogoTerminalConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            PointOfSaleTerminal.LoadPrices();
            Console.WriteLine("Running Test One");
            Console.WriteLine("Scanning Product A");
            PointOfSaleTerminal.ScanProduct("A");
            Console.WriteLine("Scanning Product B");
            PointOfSaleTerminal.ScanProduct("B");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product D");
            PointOfSaleTerminal.ScanProduct("D");
            Console.WriteLine("Scanning Product A");
            PointOfSaleTerminal.ScanProduct("A");
            Console.WriteLine("Scanning Product B");
            PointOfSaleTerminal.ScanProduct("B");
            Console.WriteLine("Scanning Product A");
            PointOfSaleTerminal.ScanProduct("A");
            Console.WriteLine("Total Price should be $13.25 actual price is $" + PointOfSaleTerminal.CalculateTotal());
            double test1Expected = 13.25;
            bool test1 = PointOfSaleTerminal.CalculateTotal() == test1Expected;
            Console.WriteLine("Test Pass is " + test1);
            PointOfSaleTerminal.ClearCart();
            Console.WriteLine();


            Console.WriteLine("Running Test Two");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Total Price should be $6 actual price is $" + PointOfSaleTerminal.CalculateTotal());
            double test2Expected = 6.00;
            bool test2 = PointOfSaleTerminal.CalculateTotal() == test2Expected;
            Console.WriteLine("Test Pass is " + test2);
            PointOfSaleTerminal.ClearCart();
            Console.WriteLine();

            Console.WriteLine("Running Test Three");
            Console.WriteLine("Scanning Product A");
            PointOfSaleTerminal.ScanProduct("A");
            Console.WriteLine("Scanning Product B");
            PointOfSaleTerminal.ScanProduct("B");
            Console.WriteLine("Scanning Product C");
            PointOfSaleTerminal.ScanProduct("C");
            Console.WriteLine("Scanning Product D");
            PointOfSaleTerminal.ScanProduct("D");
            Console.WriteLine("Total Price should be $7.25 actual price is $" + PointOfSaleTerminal.CalculateTotal());
            double test3Expected = 7.25;
            bool test3 = PointOfSaleTerminal.CalculateTotal() == test3Expected;
            Console.WriteLine("Test Pass is " + test3);
            PointOfSaleTerminal.ClearCart();
            Console.WriteLine();

            Console.WriteLine("Running Test Four");
            Console.WriteLine("Scanning Product E");
            try
            {
                PointOfSaleTerminal.ScanProduct("E");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            Console.WriteLine();

            Console.WriteLine("Running Test Five");
            Console.WriteLine("Scanning Blank Product");
            try
            {
                PointOfSaleTerminal.ScanProduct("");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

        }   
    }
}
