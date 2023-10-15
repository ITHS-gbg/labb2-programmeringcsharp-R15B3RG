using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Labb2ProgTemplate
{
    public class Shop
    {
        private Customer CurrentCustomer { get; set; }

        private List<Product> Products { get; set; } = new List<Product>();

        private List<Customer> customer { get; set; } = new List<Customer>();




        public Shop()
        {

            //Lista på produkter och användare

            Product LightSaber = new Product("Light Saber", 5000);
            Products.Add(LightSaber);

            Product DoubleLightSaber = new Product("Double Light Saber", 10000);
            Products.Add(DoubleLightSaber);

            Product Blaster = new Product("Blaster", 1000);
            Products.Add(Blaster);

            Product WookieePal = new Product("Wookiee Pal", 2500);
            Products.Add(WookieePal);

            Product Fuse = new Product("Fuse", 200);
            Products.Add(Fuse);

            Customer newCustomer1 = new Customer("Luke", "123");

            customer.Add(newCustomer1);

            Customer newCustomer2 = new Customer("Leia", "321");

            customer.Add(newCustomer2);

            Customer newCustomer3 = new Customer("Han", "213");

            customer.Add(newCustomer3);
        }






        public void MainMenu() //Huvudmenyn för affären när man är inloggad
        {
            Console.WriteLine(
                "Välkommen till Tatooine Store mitt ute bland dem djupa öken sanddynerna! \nVälj mellan att logga in eller att registrera 'Ny Användare': \n"
                + "\n - 1. Logga in"
                + "\n - 2. Registrera ny användare"
                + "\n - 3. Avsluta");

            string input;
            input = Console.ReadLine();

            while (true)
            {

                if (input == "1")
                {
                    Console.Clear();
                    Login();
                }
                else if (input == "2")
                {
                    Console.Clear();
                    Register();
                }
                else if (input == "3") // Avslutar programmet genom att gå ut ur applikationen
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
                else
                {

                    Console.WriteLine("Felaktigt menyval. Försök igen!");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu();
                }
            }
        }






        private void Login()
        {

            string Name;
            string Password;

            Console.WriteLine("Ange Ditt användarnamn:");

            Name = Console.ReadLine();

            Console.WriteLine("Ange Ditt lösenord:");

            Password = Console.ReadLine();

            Customer loggedInCustomer = customer.FirstOrDefault(c => c.Name == Name);


            if (loggedInCustomer != null)
            {
                while (true)
                {
                    // Kontrollerar lösenordet
                    if (loggedInCustomer.CheckPassword(Password))
                    {
                        Console.WriteLine("Inloggningen Lyckades!");
                        CurrentCustomer = loggedInCustomer;
                        Console.Clear();
                        ShopMenu();


                    }

                    else if (!loggedInCustomer.CheckPassword(Password))

                    {
                        Console.WriteLine("Felaktigt lösenord! Försökt igen:");

                        Password = Console.ReadLine();
                    }

                }


            }

            else // Säger till om den inte hittar en användare
            {
                Console.WriteLine("Användaren finns ej. Vänligen registrera dig först!");
                Console.ReadKey();
                Console.Clear();
                MainMenu();
            }

        }





        private void Register() // Skapar ny användare
        {
            Console.WriteLine("Ange ett användarnamn: ");

            string input = Console.ReadLine();

            Console.WriteLine("Ange ett lösenord: ");

            string input2 = Console.ReadLine();

            Console.WriteLine("Upprepa lösenord: ");

            string input3 = Console.ReadLine();

            while (true)
            {

                if (input3 == input2) //Dubbelkollar ditt lösenord. Om det stämmer, skapas den nya användaren
                {
                    Customer newCustomer = new Customer(input, input2);

                    customer.Add(newCustomer);

                    Console.Clear();

                    Console.WriteLine("Ny användare skapad:"
                                      + "\n" + newCustomer);

                    Console.ReadKey();

                    Console.Clear();

                    MainMenu();

                    break;

                }
                else if (input3 != input2)
                {
                    Console.Clear();

                    Console.WriteLine("Fel lösenord! Försök igen!");

                    input3 = Console.ReadLine();

                }

            }

        }






        private void ShopMenu()
        {

            Console.WriteLine($"Välkommen {CurrentCustomer.Name}! Du är nu inloggad! Välj mellan att:"
                              + "\n - 1. Handla"
                              + "\n - 2. Kolla kundvagnen"
                              + "\n - 3. Gå till kassan"
                              + "\n - 4. Mina uppgifter");

            string input = Console.ReadLine();

            if (input == "1") // Väljer att handla/ gå till varor i affären
            {

                Console.Clear();

                while (true)
                {

                    Console.WriteLine("Produkter tillgängliga i affären:");

                    // Visar listan av varor/produkter med pris

                    for (int i = 0; i < Products.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {Products[i].Name} - {Products[i].Price} peggats");
                    }

                    Console.WriteLine(
                        "Välj produktnummer att lägga till i kundvagnen (0 eller ENTER för att avbryta):");
                    string produktVal = Console.ReadLine();

                    if (int.TryParse(produktVal, out int productIndex) && productIndex >= 1 &&
                        productIndex <= Products.Count)
                    {
                        Console.WriteLine(
                            $"Ange antal av {Products[productIndex - 1].Name} att lägga till i kundvagnen:");
                        string quantityInput = Console.ReadLine();

                        if (int.TryParse(quantityInput, out int quantity) && quantity >= 0)
                        {
                            // Lägg till vald produkt och antal i kundvagnen
                            Product selectedProduct = Products[productIndex - 1];
                            CurrentCustomer.AddToCart(selectedProduct, quantity);

                            Console.Clear();

                            Console.WriteLine($"{quantity} st av {selectedProduct.Name} har lagts till i kundvagnen.");

                            Console.ReadKey();

                            Console.Clear();

                        }
                    }
                    // Avbryter handeln
                    else if (productIndex == 0)
                    {
                        Console.WriteLine("Handeln avbruten.");
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt val. Försök igen!");
                        Console.Clear();
                    }
                }
            }

            else if (input == "2") // Väljer att kolla i och/eller justera kundvagnen
            {
                Console.Clear();
                ViewCart();
            }

            else if (input == "3") // Går till kassan
            {
                Console.Clear();

                List<Product> cartItems = CurrentCustomer.Cart;

                Console.WriteLine("\nVill du fortsätta till kassan? (Ja/Nej)");
                string checkoutChoice = Console.ReadLine().ToLower();

                if (checkoutChoice == "ja")
                {
                    if (cartItems.Count > 0) // Kollar din kundvagn innan du går vidare till kassan
                    {
                        Checkout();
                    }
                    else
                    {
                        Console.WriteLine("Din kundvagn är tom.");
                        Console.ReadKey();
                        Console.Clear();
                        ShopMenu();
                    }
                }

                else // Går tillbaka till huvudmenyn ifall man inte vill fortsätta till kassan
                {
                    Console.Clear();
                    ShopMenu();
                }
            }

            else if (input == "4") //Här loggar man ut ur affären
            {

                while (true)
                {
                    Console.Clear();

                    string customerInfo = CurrentCustomer.ToString();

                    Console.WriteLine(CurrentCustomer);

                    Console.WriteLine("Vill du logga ut eller fortsätta handla?"
                                      + "\n - 1. Logga ut"
                                      + "\n - 2. Fortsätt handla");

                    string Val = Console.ReadLine();

                    if (Val == "1")
                    {
                        CurrentCustomer = null;
                        Console.WriteLine("Du har loggats ut.");
                        Console.ReadLine();
                        Console.Clear();
                        MainMenu();
                        break;
                    }
                    else if (Val == "2")
                    {

                        Console.Clear();
                        ShopMenu();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt Menyval.");
                        Console.ReadKey();
                    }
                }
            }

            else
            {
                Console.WriteLine("Felaktigt Menyval. Försök igen!");
                Console.ReadLine();
            }

        }







        private void ViewCart()
        {
            Console.WriteLine("Din kundvagn:");

            // Om kundvagnen är tom
            List<Product> cartItems = CurrentCustomer.Cart;

            // Gruppera varor efter namn och beräkna antalet varje produkt
            var groupedCartItems = cartItems.GroupBy(item => item.Name)
                .Select(group => new
                {
                    ProductName = group.Key,
                    Quantity = group.Count(),
                    Price = group.First().Price
                });

            // Visa varje produkt i kundvagnen separat
            int index = 1;
            double totalCartPrice = 0;

            foreach (var groupedItem in groupedCartItems)
            {
                double productTotal = groupedItem.Price * groupedItem.Quantity;
                totalCartPrice += productTotal;
                Console.WriteLine(
                    $"{index}. {groupedItem.ProductName} - x {groupedItem.Quantity} {groupedItem.Price} peggats = {productTotal} peggats");
                index++;

            }

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine($"Totalt pris för kundvagnen: {totalCartPrice} peggats");
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("Ange numret på produkten du vill ta bort (0 eller ENTER för att fortsätta handla): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice > 0 && choice <= groupedCartItems.Count())
                {
                    Console.Write($"Hur många av {groupedCartItems.ElementAt(choice - 1).ProductName} vill du ta bort: ");

                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        // Ta bort det angivna antalet exemplar av produkten från kundvagnen
                        var selectedGroup = groupedCartItems.ElementAt(choice - 1);
                        Product selectedProduct = cartItems.First(item => item.Name == selectedGroup.ProductName);
                        CurrentCustomer.RemoveFromCart(selectedProduct, quantity);

                        Console.WriteLine($"{quantity} st {selectedProduct.Name} har tagits bort från kundvagnen.");
                        Console.ReadKey();
                        Console.Clear();
                        ViewCart();
                    }
                }
                else if (choice != 0)
                {
                    Console.WriteLine("Ogiltigt val!");
                    Console.ReadKey();
                    Console.Clear();
                    ViewCart();
                }
            }
            else
            {
                Console.WriteLine("Din kundvagn är tom.");
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"Totalt pris för kundvagnen: 0 peggats");
                Console.WriteLine("---------------------------------------------------------");
            }

            Console.Clear();
            ShopMenu();

        }








        private void Checkout()
        {
            List<Product> cartItems = CurrentCustomer.Cart;

            Console.WriteLine("Din kundvagn:");

            if (cartItems.Count == 0)
            {
                Console.WriteLine("Din kundvagn är tom. Handla något först.");
            }
            else
            {
                // Gruppera varor efter namn och beräkna antalet varje produkt
                var groupedCartItems = cartItems.GroupBy(item => item.Name)
                    .Select(group => new
                    {
                        ProductName = group.Key,
                        Quantity = group.Count(),
                        Price = group.First().Price
                    });

                // Visa varje produkt i kundvagnen separat och beräkna det totala priset
                int index = 1;
                double totalCartPrice = 0;

                foreach (var groupedItem in groupedCartItems)
                {
                    double productTotal = groupedItem.Price * groupedItem.Quantity;
                    totalCartPrice += productTotal;

                    Console.WriteLine(
                        $"{index}. {groupedItem.ProductName} - x {groupedItem.Quantity} {groupedItem.Price} peggats = {productTotal} peggats");
                    index++;
                }

                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine($"Totalt pris för kundvagnen: {totalCartPrice} peggats");

                Console.WriteLine("Vill du fortsätta till betalningen? (Ja/Nej)");
                string checkout = Console.ReadLine().ToLower();

                if (checkout == "ja")
                {
                    Console.Clear();
                    Console.WriteLine($"Tack för att du handlade hos Tatooine Store! Du ligger nu back: {totalCartPrice} peggats! Have a nice day! ;)");
                    cartItems.Clear();
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                }

                ShopMenu();
            }
        }


    }

}


