using SwinAdventure;

public class ItemTest
{
    private Item sword;

    [SetUp]
    public void Setup()
    {
        sword = new Item(
            new string[] { "sword", "bronze" },
            "bronze sword",
            "A short sword cast from bronze"
        );
    }

    [Test]
    public void TestItemIsIdentifiable()
    {
        Assert.That(sword.AreYou("sword"), Is.True);
        Assert.That(sword.AreYou("bronze"), Is.True);
        Assert.That(sword.AreYou("SWORD"), Is.True);  
        Assert.That(sword.AreYou("blade"), Is.False);
    }

    [Test]
    public void TestShortDescription()
    {
        Assert.That(sword.ShortDescription, Is.EqualTo("a bronze sword (sword)"));
    }

    [Test]
    public void TestLongDescription()
    {
        Assert.That(sword.LongDescription, Is.EqualTo("A short sword cast from bronze"));
    }

    [Test]
    public void TestPrivilegeEscalation()
    {
        sword.PrivilegeEscalation("3500");
        Assert.That(sword.FirstId, Is.EqualTo("Class tuesday afternoon"));
    }
}
