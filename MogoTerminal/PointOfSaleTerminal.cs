using MogoTerminal.Models;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace MogoTerminal
{
    public class PointOfSaleTerminal
    {

        static List<Products> products = new List<Products>();
        static readonly List<CartItem> cartItems = new List<CartItem>();
        private static readonly string workingdir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public static void LoadPrices()
        {
            //Load products from JSON file
            string JsonFile = workingdir + "\\Data\\Products.json";
            products = JsonConvert.DeserializeObject<List<Products>>(File.ReadAllText(JsonFile));
        }

        public static void ScanProduct(string product)
        {
            //throw exception if the scan is invalid
            if (product == string.Empty) { throw new ArgumentNullException(); };
            if (!IsValidProduct(product)) { throw new Exception("Scanned product " + product + " is not valid"); };

            //Product is Valid, Add to Cart
            AddToCart(product);
        }

        private static void AddToCart(string product)
        {
            //Work out if the product is allredy in the cart
            var index = ExistsInCart(product);
            //If it exists then increase units
            if ( index != -1)
            {
                int cartQuantity = cartItems[index].Quantity;
                cartItems[index].Quantity = ++cartQuantity;
            }
            //If it does not exist then add
            else
            {
                cartItems.Add(new CartItem() {Product = product, Quantity = 1 });
            }
        }

        private static int ExistsInCart(string product)
        {
            CartItem cartItem = new CartItem();
            //Loop the cart to see if it exists 
            cartItems.ForEach(item => {
                if (item.Product == product)
                {
                    //Set the cart Item
                    cartItem = item;
                }

            });
            //Find the Index
            return cartItems.IndexOf(cartItem);
        }

        private static bool IsValidProduct(string product)
        {
            //Set the default value
            bool exists = false;
            //Loop the products to see if it exists 
            products.ForEach(item => {
                if (item.Product == product)
                {
                    //Note that we found it
                    exists =  true;
                } 
            });
            //Return the value
            return exists;
        }

        public static double CalculateTotal()
        {
            // set the default working value
            double cartPrice = 0.00;
            //Loop through each item in the cart
            cartItems.ForEach(cartitem =>
            {
                //Get the working Qty
                int quantity = cartitem.Quantity;
                //Get the item prices and loop each price break
                GetPrices(cartitem.Product).ForEach(price => {
                    //Get the price break qty
                    int chargeQty = quantity / price.Quantity;
                    //Add the price break value to the working value
                    cartPrice += chargeQty * price.Price;
                    //Change the working quantity to what has not been charged 
                    quantity %= price.Quantity;
                });
            });
            return cartPrice;
        }

        public static List<Prices> GetPrices(string product)
        {
            //Build a empty List
            List<Prices> prices = new List<Prices>();
            //Get the ID of the product to return the pricing
            int productIndex = GetProductID(product);
            //Loop the product pricing and add to the list
            products[productIndex].Prices.ForEach(price => {
                prices.Add(price);
            });
            //Sort the list so that the quantity break has biggest to the top
            prices.Sort(delegate (Prices x, Prices y)
            {
                return y.Quantity.CompareTo(x.Quantity);
            });
            //Return the list
            return prices;
        }

        private static int GetProductID(string productID)
        {
            Products product = new Products();
            //Loop the products 
            products.ForEach(item => {
                if (item.Product == productID)
                {
                    //set the product 
                    product = item;
                }

            });
            //Return the product Index
            return products.IndexOf(product);
        }

        public static void ClearCart()
        {
            //Remove all Items from the cart
            cartItems.Clear();
        }
    }
}
