using System;
using System.IO;
using SwinAdventure;

namespace MainProgram
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Player _testPlayer = new Player("James", "an explorer");

            Item item1 = new Item(new string[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
            Item item2 = new Item(new string[] { "light", "torch" }, "A Torch", "A Torch to light the path");

            //Put items into player's inventory
            _testPlayer.Inventory.Put(item1);
            _testPlayer.Inventory.Put(item2);

            if (_testPlayer.Locate("torch") != null)
            {
                Console.WriteLine("The object torch exists!");
                Console.WriteLine(_testPlayer.Inventory.HasItem("torch"));
            }
            else
            {
                Console.WriteLine("The object torch does not exist");
            }

            //Save to file
            using (StreamWriter writer = new StreamWriter("TestPlayer.txt"))
            {
                _testPlayer.SaveTo(writer);
            }

            //Load (display) from file
            using (StreamReader reader = new StreamReader("TestPlayer.txt"))
            {
                _testPlayer.LoadFrom(reader);
            }
        }
    }
}
