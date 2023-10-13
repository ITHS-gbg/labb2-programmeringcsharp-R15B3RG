using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2ProgTemplate
{
    public class Customer
    {




        public override string ToString() 
        {

            //Sparar användarens värden genom en stringbuilder

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Namn: {Name}");
            stringBuilder.AppendLine($"Lösenord: {Password}");

            stringBuilder.AppendLine("Kundvagn:");
            foreach (Product product in _cart)
            {
                stringBuilder.AppendLine($"- {product.Name} ({product.Price} peggats)");
            }

            stringBuilder.AppendLine($"Totalt pris i kundvagnen: {CartTotal()} peggats");

            return stringBuilder.ToString();
        }




        public string Name { get; private set; }

        private string Password { get; set; }

        private List<Product> _cart;
        public List<Product> Cart { get { return _cart; } }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            _cart = new List<Product>();
        }

        public bool CheckPassword(string password)
        {
            return Password.Equals(password);
        }

        public void AddToCart(Product product, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                _cart.Add(product);
            }
        }

        public void RemoveFromCart(Product product)
        {
            _cart.Remove(product);
        }

        public double CartTotal()
        {
            double total = 0;
            foreach (Product product in _cart)
            {
                total += product.Price;
            }
            return total;
        }
    }
}

