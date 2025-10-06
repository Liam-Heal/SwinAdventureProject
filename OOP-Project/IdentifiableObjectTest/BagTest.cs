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
        _testFoodBag = new Bag(new string[] { "bag", "food" }, "Food Bag", "A bag of food");
        _testSchoolBag = new Bag(new string[] { "bag", "school" }, "School Bag", "A bag for school");

        _itemTest1 = new Item(new string[] { "pencil", "red" }, "Red Pencil", "Pencil for school");
        _itemTest2 = new Item(new string[] { "apple", "red" }, "Red apple", "Apple for food");

        _testFoodBag.Inventory.Put(_itemTest2);
        _testSchoolBag.Inventory.Put(_itemTest1);
        _testSchoolBag.Inventory.Put(_testFoodBag);
    }

    [Test]
    public void BagLocatesItem()
    {
        Assert.That(_testSchoolBag.Locate("pencil"), Is.EqualTo(_itemTest1));
    }

    [Test]
    public void BagLocatesSelf()
    {
        Assert.That(_testFoodBag.Locate("food"), Is.EqualTo(_testFoodBag));
    }

    [Test]
    public void BagLocatesNothing()
    {
        Assert.That(_testFoodBag.Locate("banana"), Is.Null);
    }

    [Test]
    public void BagFullDescription()
    {
        string desc = _testFoodBag.FullDescription;
        Assert.That(desc, Does.Contain("In this Food Bag you can see:"));
        Assert.That(desc, Does.Contain(_itemTest2.ShortDescription));
    }

    [Test]
    public void BagInBag()
    {
        Assert.That(_testSchoolBag.Locate("food"), Is.EqualTo(_testFoodBag));
        Assert.That(_testSchoolBag.Locate("pencil"), Is.EqualTo(_itemTest1));
        Assert.That(_testSchoolBag.Locate("apple"), Is.Null);
    }

    [Test]
    public void BagInBagWithPrivilegedItem()
    {
        _itemTest2.PrivilegeEscalation("3500");
        Assert.That(_testFoodBag.Locate("class tuesday afternoon"), Is.EqualTo(_itemTest2));
        Assert.That(_testSchoolBag.Locate("class tuesday afternoon"), Is.Null);
    }
}
