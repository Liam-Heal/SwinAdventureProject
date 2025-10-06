using SwinAdventure;

public class IdentifiableTest
{
    private IdentifiableObject id;

    [SetUp]
    public void Setup()
    {

        id = new IdentifiableObject(new string[] { "105923500", "Liam", "Healey", "COS20007" });
    }

    [Test]
    public void TestAreYou()
    {
        Assert.That(id.AreYou("Liam"), Is.True);
    }
    [Test]
    public void TestNotAreYou()
    {
        Assert.That(id.AreYou("John"), Is.False);
    }
    [Test]
    public void TestCaseSensitive()
    {
        Assert.That(id.AreYou("LIAM"), Is.True);
    }

    [Test]
    public void TestFirstId()
    {
        Assert.That(id.FirstId, Is.EqualTo("105923500"));
    }

    [Test]

    public void TestPrivilege()
    {
        id.PrivilegeEscalation("3500");
        Assert.That((id.FirstId), Is.EqualTo("class tuesday afternoon"));
    }
}