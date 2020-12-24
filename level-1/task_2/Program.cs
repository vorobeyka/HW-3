using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace task_2
{
    public enum PlayerRank
    {
        Private = 2,
        Lieutenant = 21,
        Captain = 25,
        Major = 29,
        Colonel = 33,
        General = 39,
    }

    public interface IPlayer
    {
        int Age { get; }
        string FirstName { get; }
        string LastName { get; }
        PlayerRank Rank { get; }
    }

    class Player : IPlayer
    {
        public int Age { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public PlayerRank Rank { get; }

        public Player(int age, string firstName, string lastName, PlayerRank rank)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
        }

        public override string ToString()
        {
            return $"Age: {Age}\nName: {LastName} {FirstName}\nRank: {Rank.ToString()} ({(int)Rank})";
        }
    }

    class Distinct : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            return x.Age == y.Age && x.Rank == y.Rank
                    && (x.LastName + x.FirstName).Equals(y.LastName + y.FirstName);
        }

        public int GetHashCode([DisallowNull] Player obj)
        {
            return base.GetHashCode();
        }
    }

    class CompareByAge : IComparer<Player>
    {
        int IComparer<Player>.Compare(Player x, Player y)
        {
            return x.Age - y.Age;
        }
    }

    class CompareByName : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            return (x.LastName + x.FirstName).CompareTo(y.LastName + y.FirstName);
        }
    }

    class CompareByRank : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            return x.Rank - y.Rank;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1.2. Three sorts and one comparator by Andrey Basystyi");
            List<Player> _players = new List<Player>()
            {
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(31, "Alex", "Alexeenko", PlayerRank.Major),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(59, "Ivan", "Ivanov", PlayerRank.General),
                new Player(52, "Ivan", "Snezko", PlayerRank.Lieutenant),
                new Player(34, "Alex", "Zeshko", PlayerRank.Colonel),
                new Player(29, "Ivan", "Ivanenko", PlayerRank.Captain),
                new Player(19, "Peter", "Petrenko", PlayerRank.Private),
                new Player(34, "Vasiliy", "Sokol", PlayerRank.Major),
                new Player(31, "Alex", "Alexeenko", PlayerRank.Major),
            };
            var distinctPlayer = _players.Distinct(new Distinct()).ToList();
            Console.WriteLine("\nSorted list by name:\n");
            distinctPlayer.Sort(new CompareByName());
            distinctPlayer.ForEach(p => Console.WriteLine($"{p}\n____________________"));
            Console.WriteLine("\nSorted list by age:\n");
            distinctPlayer.Sort(new CompareByAge());
            distinctPlayer.ForEach(p => Console.WriteLine($"{p}\n____________________"));
            Console.WriteLine("\nSorted list by rank:\n");
            distinctPlayer.Sort(new CompareByRank());
            distinctPlayer.ForEach(p => Console.WriteLine($"{p}\n____________________"));
        }
    }
}
