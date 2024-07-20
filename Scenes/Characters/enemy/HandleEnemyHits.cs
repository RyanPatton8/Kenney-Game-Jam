using Godot;
using System;

public partial class HandleEnemyHits : Node
{
	[Export] public Area3D hurtBox {get; private set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hurtBox.AreaEntered += TakeHit;
		hitBox.AreaEntered += GiveHit;
		attackCooldown.Timeout += HitAgain;
	}

	public void TakeHit(Area3D area)
	{
		this.GetParent().QueueFree();	
	}
}
