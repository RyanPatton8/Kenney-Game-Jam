using Godot;
using System;

public partial class Spawner : Node
{
    [Export] public PackedScene enemy { get; private set; }
    [Export] public Timer spawnTimer { get; private set; }
    [Export] public Marker3D[] spawnPoints;

    private RandomNumberGenerator rng;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize(); // Ensures different results each run
        spawnTimer.Timeout += SpawnEnemy;
    }

    public void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            GD.PrintErr("No spawn points available.");
            return;
        }

        int spawnIndex = rng.RandiRange(0, spawnPoints.Length - 1);
        Godot.CharacterBody3D instance = (Godot.CharacterBody3D)enemy.Instantiate();
        GetTree().Root.AddChild(instance);
        instance.GlobalPosition = spawnPoints[spawnIndex].GlobalPosition;
    }
}

