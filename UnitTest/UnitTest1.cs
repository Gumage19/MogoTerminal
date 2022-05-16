using MogoTerminal;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace UnitTest
{
    public class Tests
    {
        private static string jsonFile;
        private static string workingdir;

        public Tests()
        {
            workingdir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            jsonFile = File.ReadAllText(workingdir + "\\Data\\Products.json");
        }

        [SetUp]
        public void Setup()
        {    
        }

        [TestCase(13.25, new string[] { "A", "B", "C", "D", "A", "B", "A"})]
        [TestCase(6.00, new string[] { "C", "C", "C", "C", "C", "C", "C" })]
        [TestCase(7.25, new string[] { "A", "B", "C", "D"})]
        public void Test_ScanValue(double expectedValue, string[] scanData)
        {
            // Arrange
            var terminal = new PointOfSaleTerminal();
            terminal.LoadPrices(jsonFile);

            // Act
            foreach(var s in scanData)
            {
                terminal.ScanProduct(s);
            }

            //Assert
            Assert.AreEqual(expectedValue, terminal.CalculateTotal());
            terminal.ClearCart();
            Assert.AreEqual(0, terminal.CalculateTotal());
        }

    }
}