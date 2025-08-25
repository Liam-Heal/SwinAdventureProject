using SwinAdventure;

public class VerificationTests
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
    public void TestRemoveIdentifierTrue()
    {
        Assert.That(sword.AreYou("bronze"), Is.True);

        bool removed = sword.RemoveIdentifier("bronze");

        Assert.That(removed, Is.True);
        Assert.That(sword.AreYou("bronze"), Is.False);
    }

    [Test]
    public void TestRemoveIdentifierFalse()
    {
        bool removed = sword.RemoveIdentifier("blade");
        Assert.That(removed, Is.False);
    }

    // Test and add duplicate identfiers blcked 
    [Test]
    public void TestAddDuplicateBlocked()
    {
        int before = sword.GetIdentifierCount();

        // FirstId should be sword
        Assert.That(sword.FirstId, Is.EqualTo("sword"));

        // Try to add a duplicate of FirstId
        sword.AddIdentifier("SWORD");

        int after = sword.GetIdentifierCount();

        // Check count is the same
        Assert.That(after, Is.EqualTo(before));
    }
}
