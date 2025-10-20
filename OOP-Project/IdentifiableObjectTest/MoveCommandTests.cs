using NUnit.Framework;
using SwinAdventure;
using PathObj = SwinAdventure.Path;  


public class MoveCommandTests
{
    private Player _player = null!;
    private Location _roomA = null!;
    private Location _roomB = null!;
    private MoveCommand _move = null!;

    [SetUp]
    public void SetUp()
    {
        _player = new Player("James", "an explorer");

        _roomA = new Location(new[] { "rooma", "hall" }, "Hall", "A plain hall");
        _roomB = new Location(new[] { "roomb", "kitchen" }, "Kitchen", "A small kitchen");

        // Path from A -> B, identifiable as "north" and "n"
       var northToB = new PathObj(new[] { "north", "n" }, "North Door", "A doorway to the north", _roomB);
        _roomA.AddPath(northToB);

        _player.Location = _roomA;

        _move = new MoveCommand(new[] { "move", "go", "head", "leave" });
    }

    [Test]
    public void MoveNorth_GoesToDestination()
    {
        string result = _move.Execute(_player, new[] { "move", "north" });

        Assert.That(_player.Location, Is.EqualTo(_roomB));
        Assert.That(result.ToLower(), Does.Contain("you move north"));
        Assert.That(result, Does.Contain("Kitchen"));      // new location description included
    }

    [Test]
    public void MoveAliasN_GoesToDestination()
    {
        string result = _move.Execute(_player, new[] { "go-n" }); // hyphen form

        Assert.That(_player.Location, Is.EqualTo(_roomB));
        Assert.That(result.ToLower(), Does.Contain("you move n"));
        Assert.That(result, Does.Contain("Kitchen"));
    }

    [Test]
    public void MoveInvalidDirection_StaysPut()
    {
        string before = _player.Location.Name;

        string result = _move.Execute(_player, new[] { "head", "south" });

        Assert.That(_player.Location.Name, Is.EqualTo(before));
        Assert.That(result, Is.EqualTo("There is no path to 'south'."));
    }
}
