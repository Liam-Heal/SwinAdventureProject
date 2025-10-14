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

            List<Player> players = new List<Player>();
            for (int i = 1; i <= x; i++)
            {
                Player p = new Player($"Player{i}", $"an explorer number {i}");
                players.Add(p);
            }

            List<Bag> bags = new List<Bag>();
            for (int i = 1; i <= y; i++)
            {
                string[] ids;
                if (i % 2 == 0)
                {
                    ids = new string[] { "bag", $"travel{i}", "otherid" };
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

            // Put the bag into the player's inventory so you can "look-at-…-in-bag"
            player1.Inventory.Put(bag1);

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

            // ------------ simple REPL to test LookCommand (hyphens or spaces) ------------
            var look = new LookCommand(new[] { "look" });
            Console.WriteLine();
            Console.WriteLine("Type look commands (hyphens OR spaces). Examples:");
            Console.WriteLine("  look at inventory");
            Console.WriteLine("  look-at-torch");
            Console.WriteLine("  look at map in bag");
            Console.WriteLine("  look-at-map-in-bag");
            Console.WriteLine("Type 'exit' to quit.");
            while (true)
            {
                Console.Write("> ");
                string? line = Console.ReadLine();
                if (line == null) break;
                line = line.Trim();
                if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;
                if (line.Length == 0) continue;

                // Pass as a single token if user typed hyphens; otherwise split by spaces.
                string[] argsForLook = line.Contains("-")
                    ? new[] { line }
                    : line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                string result = look.Execute(player1, argsForLook);
                Console.WriteLine(result);
            }
        }
    }
}
