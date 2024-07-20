using Godot;
using System;

public partial class ui : CanvasLayer
{
	[Export] public Label scoreLabel {get; private set;}
	[Export] public Label ammoLabel {get; private set;}
	[Export] public Label healthLabel {get; private set;}
	[Export] public CharacterBody3D player {get; private set;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager gameManager = GameManager.Instance;
		GameManager.Instance.Connect(nameof(GameManager.ScoreUpdated), new Callable(this, nameof(UpdateScoreLabel)));
		UpdateScoreLabel();
        
	}

	public override void _PhysicsProcess(double delta)
	{
		ammoLabel.Text = "Ammo: " + (10 - player.bulletCount);
		healthLabel.Text = "Health: " + player.health;
	}

	public void UpdateScoreLabel()
	{
		GameManager gameManager = GameManager.Instance;
		scoreLabel.Text = "Score: " + gameManager.GetScore(); 
	}
}
