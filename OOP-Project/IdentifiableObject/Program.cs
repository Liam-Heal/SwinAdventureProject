using System;
using System.IO;
using System.Collections.Generic;
using SwinAdventure;

namespace MainProgram
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            int x = 5;
            int y = 5;

            // Create the players
            List<Player> players = new List<Player>();
            for (int i = 1; i <= x; i++)
            {
                Player p = new Player($"Player{i}", $"an explorer number {i}");
                players.Add(p);
            }

            // Create the bags, if int is even then add a different identifier to alter the sorted container (Sorted no longer required)
            List<Bag> bags = new List<Bag>();
            for (int i = 1; i <= y; i++)
            {
                string[] ids;

                if (i % 2 == 0)
                {
                    ids = new string[] { "bag", $"travel{i}", "otherID" };
                }
                else
                {
                    ids = new string[] { "bag", $"travel{i}" };
                }

                Bag b = new Bag(ids, $"Bag{i}", $"a bag used by explorer {i}");
                bags.Add(b);
            }
            Console.WriteLine("Generated containers:");
            foreach (var p in players)
            {
                Console.WriteLine($"Player: {p.Name} — {p.FullDescription}");
            }
            foreach (var b in bags)
            {
                Console.WriteLine($"Bag: {b.Name} — {b.FullDescription}");
            }
            Console.WriteLine();


            Console.WriteLine();

            Player player1 = new Player("James", "an explorer");

            Item item1 = new Item(new string[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
            Item item2 = new Item(new string[] { "light", "torch" }, "A Torch", "A Torch to light the path");

            player1.Inventory.Put(item1);
            player1.Inventory.Put(item2);

            using (StreamWriter writer = new StreamWriter("TestPlayer.txt"))
            {
                player1.SaveTo(writer);
            }

            Bag bag1 = new Bag(new string[] { "bag", "adventure" }, "Adventure Bag", "A large leather bag");
            bag1.Inventory.Put(new Item(new string[] { "map" }, "Map", "A map of the region"));
            bag1.Inventory.Put(new Item(new string[] { "rope" }, "Rope", "A long sturdy rope"));

            List<IHaveInventory> myContainers = new List<IHaveInventory> { player1, bag1 };

            foreach (IHaveInventory container in myContainers)
            {
                if (container is Player p)
                {
                    Console.WriteLine("Player:");
                    Console.WriteLine(p.FullDescription);
                }
                else if (container is Bag b)
                {
                    Console.WriteLine("Bag:");
                    Console.WriteLine(b.FullDescription);
                }
            }

            using (StreamReader reader = new StreamReader("TestPlayer.txt"))
            {
                player1.LoadFrom(reader);
            }
        }
    }
}
