using System;
using NUnit.Framework;
using SwinAdventure;

public class BagTests
{
    private Bag _testFoodBag;
    private Bag _testSchoolBag;
    private Item _itemTest1;
    private Item _itemTest2;

    [SetUp]
    public void SetUp()
    {
        // Construct new bag using the bag class, identifiers, short desc, full desc
        _testFoodBag = new Bag(new string[] { "bag", "food" }, "Food Bag", "A bag of food");
        _testSchoolBag = new Bag(new string[] { "bag", "school" }, "School Bag", "A bag for school");

        _itemTest1 = new Item(new string[] { "pencil", "red" }, "Red Pencil", "Pencil for school");
        _itemTest2 = new Item(new string[] { "apple", "red" }, "Red apple", "Apple for food");

        // Puts the constructed objects into the bags invetories.
        _testFoodBag.Inventory.Put(_itemTest2);
        _testSchoolBag.Inventory.Put(_itemTest1);
        _testSchoolBag.Inventory.Put(_testFoodBag);
    }

    [Test]
    // Search for an identifier "pencil", seeing if it matches an identifier for the test item.
    public void BagLocatesItem()
    {
        Assert.That(_testSchoolBag.Locate("pencil"), Is.EqualTo(_itemTest1));
    }

    [Test]
    // See if the bag can locate itself, that is search for its own matching identifier within itself.
    public void BagLocatesSelf()
    {
        Assert.That(_testFoodBag.Locate("food"), Is.EqualTo(_testFoodBag));
    }

    [Test]
    // Search for something that doesn't exist as an identifier within the bag, assert that it returns nothing (It isn't there.)
    public void BagLocatesNothing()
    {
        Assert.That(_testFoodBag.Locate("banana"), Is.Null);
    }

    [Test]
    // Looking at the full descriptiong made, assert that it matches the full description override in Bags.cs
    public void BagFullDescription()
    {
        string desc = _testFoodBag.FullDescription;
        Assert.That(desc, Does.Contain("In this Food Bag you can see:"));
        Assert.That(desc, Does.Contain(_itemTest2.ShortDescription));
    }

    [Test]
    // See if the bag can locate another bag within itself
    public void BagInBag()
    {
        Assert.That(_testSchoolBag.Locate("food"), Is.EqualTo(_testFoodBag));
        Assert.That(_testSchoolBag.Locate("pencil"), Is.EqualTo(_itemTest1));
        Assert.That(_testSchoolBag.Locate("apple"), Is.Null);
    }

    [Test]
    // Using the privilege escalation previously set up, give it/change an identifier to "class tuesday afternoon"
    public void BagInBagWithPrivilegedItem()
    {
        _itemTest2.PrivilegeEscalation("3500");
        Assert.That(_testFoodBag.Locate("class tuesday afternoon"), Is.EqualTo(_itemTest2));
        Assert.That(_testSchoolBag.Locate("class tuesday afternoon"), Is.Null);
    }
}
