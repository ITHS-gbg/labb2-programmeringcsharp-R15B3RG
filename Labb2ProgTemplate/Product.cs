using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Labb2ProgTemplate
{
    public record Product : IEquatable<Product>
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Product(string product, double price)
        {
            Name = product;
            Price = price;
        }
    }
}
