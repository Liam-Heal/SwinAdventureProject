using NUnit.Framework;
using SwinAdventure;

namespace SwinAdventureTest
{
    public class PlayerTest
    {
        private Player _player = null!;
        private Item _hat = null!;
        private Item _torch = null!;

        [SetUp]
        public void Setup()
        {
            _player = new Player("James", "an explorer");
            _hat   = new Item(new[] { "silver", "hat" }, "A Silver Hat", "A very shiny silver hat");
            _torch = new Item(new[] { "light", "torch" }, "A Torch", "A Torch to light the path");
            _player.Inventory.Put(_hat);
            _player.Inventory.Put(_torch);
        }

        [Test]
        public void PlayerIsIdentifiable()
        {
            Assert.That(_player.AreYou("me"), Is.True);
            Assert.That(_player.AreYou("inventory"), Is.True);
            Assert.That(_player.AreYou("someone"), Is.False);
        }

        [Test]
        public void PlayerLocatesItself()
        {
            Assert.That(_player.Locate("me"), Is.SameAs(_player));
            Assert.That(_player.Locate("inventory"), Is.SameAs(_player));
        }

        [Test]
        public void PlayerLocatesItems()
        {
            var found = _player.Locate("torch");
            Assert.That(found, Is.SameAs(_torch));            // returns the item
            Assert.That(_player.Inventory.HasItem("torch"));  // still in inventory
        }

        [Test]
        public void PlayerLocatesNothing()
        {
            Assert.That(_player.Locate("club"), Is.Null);
        }

        [Test]
        public void PlayerFullDescriptionIncludesInventory()
        {
            string desc = _player.FullDescription;
            Assert.That(desc, Does.Contain("You are James"));
            Assert.That(desc, Does.Contain("You are carrying:"));
            Assert.That(desc, Does.Contain(_hat.ShortDescription));
            Assert.That(desc, Does.Contain(_torch.ShortDescription));
        }
    }
}
