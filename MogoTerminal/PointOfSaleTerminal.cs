using MogoTerminal.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MogoTerminal
{
    public class PointOfSaleTerminal : IPointOfSaleTerminal
    {

        private static List<Products> products;
        private static List<CartItem> cartItems;

        public PointOfSaleTerminal()
        {
            cartItems = new List<CartItem>();
        }

        /* 
         * Description: Load the prices from supplied serialized string.
         * Parameters: string
         * Returns: void
         */
        public void LoadPrices(string jsonFile)
        {
            products = JsonConvert.DeserializeObject<List<Products>>(jsonFile);
        }

        /* 
         * Description: Validates the string is valid and adds to cart.
         * Parameters: string
         * Returns: void
         */
        public void ScanProduct(string product)
        {
            if (product == string.Empty) { throw new ArgumentNullException(); };
            if (!IsValidProduct(product)) { throw new Exception("Scanned product " + product + " is not valid"); };
            AddToCart(product);
        }

        /* 
         * Description: Checks if the item is in the cart then increases or adds value .
         * Parameters: string
         * Returns: void
         */
        private void AddToCart(string product)
        {
            var index = ExistsInCart(product);
            if ( index != -1)
            {
                int cartQuantity = cartItems[index].Quantity;
                cartItems[index].Quantity = ++cartQuantity;
            }
            else
            {
                cartItems.Add(new CartItem() {Product = product, Quantity = 1 });
            }
        }

        /* 
         * Description: Checks if the item is in the cart and returs the index.
         * Parameters: string
         * Returns: int
         */
        private int ExistsInCart(string product)
        {
            CartItem cartItem = new CartItem();
            cartItems.ForEach(item => {
                if (item.Product == product)
                {
                    cartItem = item;
                }
            });
            return cartItems.IndexOf(cartItem);
        }

        /* 
         * Description: Checks if the item is valid.
         * Parameters: string
         * Returns: bool
         */
        private bool IsValidProduct(string product)
        {
            bool exists = false;
            products.ForEach(item => {
                if (item.Product == product)
                {
                    exists =  true;
                } 
            });
            return exists;
        }

        /* 
         * Description: Loops the cart products and prices and returns the total of the cart.
         * Parameters: none
         * Returns: double
         */
        public double CalculateTotal()
        {
            double cartPrice = 0.00;
            cartItems.ForEach(cartitem =>
            {
                int quantity = cartitem.Quantity;
                GetPrices(cartitem.Product).ForEach(price => {
                    int chargeQty = quantity / price.Quantity;
                    cartPrice += chargeQty * price.Price;
                    quantity %= price.Quantity;
                });
            });
            return cartPrice;
        }

        /* 
         * Description: Returns the prices for a product.
         * Parameters: string
         * Returns: List of Prices
         */
        private List<Prices> GetPrices(string product)
        {
            List<Prices> prices = new List<Prices>();
            int productIndex = GetProductID(product);
            products[productIndex].Prices.ForEach(price => {
                prices.Add(price);
            });
            prices.Sort(delegate (Prices x, Prices y)
            {
                return y.Quantity.CompareTo(x.Quantity);
            });
            return prices;
        }

        /* 
         * Description: Returns the ID for a product.
         * Parameters: string
         * Returns: int
         */
        private int GetProductID(string productID)
        {
            Products product = new Products();
            products.ForEach(item => {
                if (item.Product == productID)
                {
                    product = item;
                }

            });
            return products.IndexOf(product);
        }

        /* 
         * Description: Clears all items from the cart.
         * Parameters: none
         * Returns: void
         */
        public void ClearCart()
        {
            cartItems.Clear();
        }
    }
}
