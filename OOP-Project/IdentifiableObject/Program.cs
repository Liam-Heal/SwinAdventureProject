using System;
using System.IO;
using SwinAdventure;
using System.Collections.Generic;

namespace MainProgram
{
    class MainClass
    {
        public static void Main(string[] args)
        {
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
