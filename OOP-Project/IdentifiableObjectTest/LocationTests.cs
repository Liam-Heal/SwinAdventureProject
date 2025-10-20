using NUnit.Framework;
using SwinAdventure;

[TestFixture]
public class LookCommandLocationTests
{
    private Player _player = null!;
    private Location _lab = null!;
    private Item _gem = null!;
    private LookCommand _look = null!;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("James", "an explorer");
        _look = new LookCommand(new[] { "look" });

        // Location needs ids so it can be addressed as a container
        _lab = new Location(new[] { "location", "here", "lab", "room" }, "Laboratory", "A bright, clean lab.");
        _player.Location = _lab;

        // Put an item into the LOCATION (not the player's inventory)
        _gem = new Item(new[] { "gem", "ruby" }, "red gem", "A bright red gem");
        // NOTE: this assumes Location exposes `public Inventory Inventory { get; }`
        _lab.Inventory.Put(_gem);
    }

    [Test]
    public void Look_SingleWord_ShowsLocationDescription()
    {
        var result = _look.Execute(_player, new[] { "look" });
        // Don't assert the exact whole string (formatting differs). Just check the essentials.
        Assert.That(result.ToLower(), Does.Contain("laboratory"));
        Assert.That(result.ToLower(), Does.Contain("bright"));
    }

    [Test]
    public void LookAt_Item_In_Location_UsingSpaces()
    {
        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "location" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void LookAt_Item_In_Location_UsingHyphens()
    {
        // Hyphen form goes in as a single token; your LookCommand will split on '-'
        var result = _look.Execute(_player, new[] { "look-at-gem-in-location" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void LookAt_Item_In_Here_Alias()
    {
        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "here" });
        Assert.That(result, Is.EqualTo(_gem.FullDescription));
    }

    [Test]
    public void LookAt_MissingItem_In_Location_ShowsError()
    {
        // Remove the gem so it's no longer in the location
        _lab.Inventory.Take("gem");

        var result = _look.Execute(_player, new[] { "look", "at", "gem", "in", "location" });
        // Your LookCommand uses the container's Name in the error: "I cannot find the gem in the Laboratory"
        Assert.That(result, Is.EqualTo("I cannot find the gem in the Laboratory"));
    }
}
