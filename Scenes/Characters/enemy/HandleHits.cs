using Godot;
using System;

public partial class HandleHits : Node
{
	[Export] public Area3D hurtBox {get; private set;}
	// Called when the node enters the scene tree for the first time.
	

        public override void _Ready()
    {
        hurtBox.AreaEntered += TakeHit;
    }

    public void TakeHit(Area3D area)
    {
		GameManager gameManager = GameManager.Instance;
		gameManager.IncreaseScore();
        this.GetParent().QueueFree();
    }
}
