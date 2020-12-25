using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace level_2
{
    public class Product
    {
        public string Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public int Price { get; }

        public Product(string id, string brand, string model, string price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = int.Parse(price);
        }

        public override string ToString()
        {
            return $"Product id: {Id}\nBrand: {Brand}\nModel: {Model}\nPrice: {Price}";
        }
    }

    public class Tag
    {
        public string Id { get; }
        public string Value { get; }

        public Tag(string id, string value)
        {
            Id = id;
            Value = value;
        }
    }

    public class Inventory
    {
        public string Id { get; }
        public string Location { get; }
        public int Balance { get; }

        public Inventory(string id, string location, string balance)
        {
            Id = id;
            Location = location;
            Balance = int.Parse(balance);
        }
    }

    class Program
    {
        private static List<Product> _products { get; set; }
        private static List<Tag> _tags { get; set; }
        private static List<Inventory> _inventories { get; set; }
        static void PrintInventory(List<Product> products)
        {
            foreach (var i in products)
            {
                var balance = _inventories.Where(t => t.Id == i.Id).Sum(t => t.Balance);
                Console.Write($"{i}\nBalance: {balance}");
                Console.WriteLine();
                Console.WriteLine("_____________________________________");
            }
        }

        static void MissingProducts()
        {
            var products = _products.Where(p => !_inventories.Any(i => i.Id == p.Id)).ToList();
            PrintProducts(products);
        }

        static void InventoryAsceding()
        {
            var products = _products.OrderBy(p => _inventories.Where(i => i.Id == p.Id).Sum(i => i.Balance));
            PrintInventory(products.ToList());
        }

        static void InventoryDesceding()
        {
            var products = _products.OrderByDescending(p => _inventories.Where(i => i.Id == p.Id).Sum(i => i.Balance));
            PrintInventory(products.ToList());
        }

        static void MenuBalance()
        {
            while (true)
            {
                Console.WriteLine("Products:\na. Back to main menu\nb. Missing products");
                Console.WriteLine("c. Balanced products - by asceding\nd. Balanced products - by desceding");
                Console.WriteLine("e. Balanced products - by id");
                char c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (c)
                {
                    case 'a': return;
                    case 'b': MissingProducts();
                    break;
                    case 'c': InventoryAsceding();
                    break;
                    case 'd': InventoryDesceding();
                    break;
                    case 'e': Console.WriteLine("Coming soon!");
                    break;
                    default: Console.WriteLine("Invalid input.");
                    break;
                }
            }
        }

        static void PrintProducts(List<Product> products)
        {
            foreach (var i in products)
            {
                Console.Write($"{i}\nTags: ");
                _tags.Where(t => t.Id == i.Id)
                     .Select(t => t.Value)
                     .ToList()
                     .ForEach(t => Console.Write($"{t} "));
                Console.WriteLine();
                Console.WriteLine("_____________________________________");
            }
        }

        static void ProductSearch()
        {
            Console.WriteLine("Enter search-string -> ");
            var value = Console.ReadLine();
            Console.WriteLine("Matching for product id:");
            var prods = _products.Where(p => p.Id.Contains(value)).ToList();
            PrintProducts(prods);
            Console.WriteLine("Matching for product model or brand:");
            prods = _products.Where(p => p.Model.Contains(value) || p.Brand.Contains(value)).ToList();
            PrintProducts(prods);
            Console.WriteLine("Matching for product tags:");
            var tags = _tags.Where(t => t.Value.Contains(value));
            prods = _products.Where(p => tags.Any(t => p.Id == t.Id)).ToList();
            PrintProducts(prods);
        }

        static void ProductAsceding()
        {
            var products = _products.OrderBy(p => p.Price).ToList();
            PrintProducts(products);
        }

        static void ProductDesceding()
        {
            var products = _products.OrderByDescending(p => p.Price).ToList();
            PrintProducts(products);
        }

        static void MenuProducts()
        {
            while (true)
            {
                Console.WriteLine("Products:\na. Back to main menu\nb. Product search");
                Console.WriteLine("c. List of products - price asceding\nd. List of products - price desceding");
                char c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (c)
                {
                    case 'a': return;
                    case 'b': ProductSearch();
                    break;
                    case 'c': ProductAsceding();
                    break;
                    case 'd': ProductDesceding();
                    break;
                    default: Console.WriteLine("Invalid input.");
                    break;
                }
            }
        }
        
        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("Menu:\n1. Exit\n2. Products\n3. Balance");
                Console.Write("Enter menu element -> ");
                char c = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (c)
                {
                    case '1': Environment.Exit(0);
                    break;
                    case '2': MenuProducts();
                    break;
                    case '3': MenuBalance();
                    break;
                    default: Console.WriteLine("Invalid input. Try to 1-3.");
                    break;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Task 2. ERP Reports Bot by Andrey Basystyi");
            Console.WriteLine("Rules:");
            _products = new List<Product>();
            _tags = new List<Tag>();
            _inventories = new List<Inventory>();
            try 
            {
                foreach (var i in File.ReadLines("Products.csv").Skip(1).Where(p => p != ""))
                {
                    var datas = i.Split(';');
                    _products.Add(new Product(datas[0], datas[1], datas[2], datas[3]));
                }
                foreach (var i in File.ReadLines("Tags.csv").Skip(1).Where(p => p != ""))
                {
                    var datas = i.Split(';');
                    _tags.Add(new Tag(datas[0], datas[1]));
                }
                foreach (var i in File.ReadLines("Inventory.csv").Skip(1).Where(p => p != ""))
                {
                    var datas = i.Split(';');
                    _inventories.Add(new Inventory(datas[0], datas[1], datas[2]));
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error to read file.");
                Environment.Exit(-1);
            }
            Menu();
        }
    }
}
