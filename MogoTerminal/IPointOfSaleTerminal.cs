using MogoTerminal.Models;
using System.Collections.Generic;
namespace MogoTerminal
{
    public interface IPointOfSaleTerminal
    {

        void LoadPrices(string jsonFile);

        void ScanProduct(string product);

        double CalculateTotal();

        void ClearCart();
    }
}

