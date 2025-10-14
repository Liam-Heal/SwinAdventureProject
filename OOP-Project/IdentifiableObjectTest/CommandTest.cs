using NUnit.Framework;
using SwinAdventure;

public class LookCommandTests
{
    private Player _player = null!;
    private Item _gem = null!;
    private Item _coin = null!;
    private Bag _bag = null!;
    private LookCommand _look = null!;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("James", "an explorer");
        _gem = new Item(new[] { "gem", "ruby" }, "red gem", "A bright red gem");
        _coin = new Item(new[] { "coin", "gold" }, "gold coin", "A shiny gold coin");
        _bag = new Bag(new[] { "bag", "small" }, "small bag", "A small cloth bag");
        _look = new LookCommand(new[] { "look" });
    }

    [Test]
    public void TestLookAtMe()
    {
        var result = _look.Execute(_player, new[] { "look", "at", "inventory" });
        Assert.That(result, Does.StartWith("You are"));
        Assert.That(result, Does.Contain("You are carrying:"));
    }

    [Test]
    public void TestLookAtGem()
    {
        _player.Inventory.Put(_gem);
        var result = _look.Execute(_player, new[] { "look", "at", "gem" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void TestLookAtUnknown()
    {
        var result = _look.Execute(_player, new[] { "look", "at", "gem" });
        Assert.That(result, Is.EqualTo("I cannot find the gem"));
    }

    [Test]
    public void TestLookAtGemInMe()
    {
        _player.Inventory.Put(_gem);
        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "inventory" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void TestLookAtGemInBag()
    {
        _bag.Inventory.Put(_gem);
        _player.Inventory.Put(_bag);

        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "bag" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void TestLookAtGemInNoBag()
    {
        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "bag" });
        Assert.That(result, Is.EqualTo("I cannot find the bag"));
    }

    [Test]
    public void TestLookAtNoGemInBag()
    {
        _player.Inventory.Put(_bag); // bag is present but empty of "gem"
        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "bag" });
        Assert.That(result, Is.EqualTo("I cannot find the gem in the small bag"));
    }

    [Test]
    public void TestInvalidLookVariants()
    {
        var r1 = _look.Execute(_player, new[] { "hello", "your", "student", "id" });
        Assert.That(r1, Is.EqualTo("I don't know how to look like that"));

        var r2 = _look.Execute(_player, new[] { "look", "around" });
        Assert.That(r2, Is.EqualTo("I don't know how to look like that"));

        var r3 = _look.Execute(_player, new[] { "look", "under", "gem" });
        Assert.That(r3, Is.EqualTo("What do you want to look at?"));

        var r4 = _look.Execute(_player, new[] { "look", "at", "gem", "under", "bag" });
        Assert.That(r4, Is.EqualTo("What do you want to look in?"));
    }
}
