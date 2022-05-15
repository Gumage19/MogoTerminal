using System.Collections.Generic;

namespace MogoTerminal.Models
{
    public class Products
    {
        public string Product { get; set; }
        public List<Prices> Prices { get; set; }
    }
}
