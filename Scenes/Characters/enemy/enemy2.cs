using Godot;

public partial class Mob : CharacterBody3D
{
	// Don't forget to rebuild the project so the editor knows about the new export variable.

	// Minimum speed of the mob in meters per second
	[Export]
	public int MinSpeed { get; set; } = 10;
	// Maximum speed of the mob in meters per second
	[Export]
	public int MaxSpeed { get; set; } = 18;

	public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
	}
}
