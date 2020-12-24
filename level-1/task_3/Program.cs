using System;
using System.Collections.Generic;
using System.Linq;

namespace task_3
{
    public interface IRegion
    {
        string Brand { get; }
        string Country { get; }
    }

    public interface IRegionSettings
    {
        string WebSite { get; }
    }

    public class Region : IRegion
    {
        public string Brand { get; }
        public string Country { get; }

        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }

        public override int GetHashCode()
        {
            return Brand.GetHashCode() ^ Country.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is Region))
            {
                return false;
            }

            return this.Brand == ((Region)obj).Brand && this.Country == ((Region)obj).Country;
        }
    }

    public class RegionSettings : IRegionSettings
    {
        public string WebSite { get; }
        public RegionSettings(string webSite)
        {
            WebSite = webSite;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.3. Dictionary with key by Andrey Basystyi.");
            Console.WriteLine("Rules: you need to enter dictionary. Dictionarys element example: [UA,UA]=[gra.ua].");
            Console.WriteLine("To close proggram just enter 'exit'.");
            Console.WriteLine("Enter elements count -> ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Invalid. Try to enter again -> ");
            }
            var library = new Dictionary<Region, RegionSettings>(n);
            Console.WriteLine("Enter dictionary elements:");
            for (int i = 0; i < n; )
            {
                var datas = Console.ReadLine().Split('=').ToList();
                if (datas[0] == "exit")
                {
                    Environment.Exit(0);
                }
                var regionDatas = datas[0].Split(',');
                var region = new Region(regionDatas[0].Replace("[", ""), regionDatas[1].Replace("]", ""));
                if (!library.TryAdd(region, new RegionSettings(datas[1].Replace("[", "").Replace("]", ""))))
                {
                    Console.WriteLine("This key exist. Try again.");
                    continue;
                }
                i++;
            }
            Console.WriteLine("Brand\tCountry\tWebSite");
            foreach (var i in library)
            {
                Console.WriteLine($"{i.Key.Brand}\t{i.Key.Country}\t{i.Value.WebSite}");
            }
        }
    }
}
