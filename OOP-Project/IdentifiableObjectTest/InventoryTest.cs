using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventure
{
    public class InventoryTest
    {
        private Inventory _inventory = null!;
        private Item _sword = null!;
        private Item _apple = null!;

        [SetUp]
        public void SetUp()
        {
            _inventory = new Inventory();

            _sword = new Item(
                new string[] { "sword", "bronze" },
                "bronze sword",
                "A short sword cast from bronze"
            );

            _apple = new Item(
                new string[] { "apple", "fruit" },
                "apple",
                "A shiny red apple"
            );

            // Put both in for most tests; individual tests can re-shape as needed
            _inventory.Put(_sword);
            _inventory.Put(_apple);
        }

        [Test]
        public void TestFindItem()
        {
            Assert.That(_inventory.HasItem("sword"), Is.True);
            // case-insensitive
            Assert.That(_inventory.HasItem("BRONZE"), Is.True);  
            Assert.That(_inventory.HasItem("fruit"), Is.True);
        }

        [Test]
        public void TestNoItemFind()
        {
            Assert.That(_inventory.HasItem("shield"), Is.False);
        }

        [Test]
        public void TestFetchItem()
        {
            var fetched = _inventory.Fetch("sword");
            // same object instance
            Assert.That(fetched, Is.SameAs(_sword));     
            // fetch does NOT remove      
            Assert.That(_inventory.HasItem("sword"), Is.True); 
        }

        [Test]
        public void TestTakeItem()
        {
            var taken = _inventory.Take("apple");
            // removed item returned
            Assert.That(taken, Is.SameAs(_apple));         
            // removed from inventory    
            Assert.That(_inventory.HasItem("apple"), Is.False); 
        }

        [Test]
        public void TestRemoveItem()
        {
            // Inventory contains _sword and _apple from SetUp
            bool removed = _inventory.RemoveItem(_sword);

            Assert.That(removed, Is.True);

            // Sword should no longer be present
            Assert.That(_inventory.HasItem("sword"), Is.False);

            // Apple should still be there, ensure that only sword was removed.
            Assert.That(_inventory.HasItem("apple"), Is.True);

            // Remove something that doesnt exist, test edge case to ensure no issues.
            Item dummy = new Item(new string[] { "shield" }, "shield", "a sturdy shield");
            Assert.That(_inventory.RemoveItem(dummy), Is.False);
        }


        [Test]
        public void TestItemList()
        {
            // Build the exact expected list based on current inventory order (_sword then _apple)
            string expected =
                "\t" + _sword.ShortDescription + "\n" +
                "\t" + _apple.ShortDescription + "\n";

            Assert.That(_inventory.ItemList, Is.EqualTo(expected));
        }
    }
}
