using Godot;
using System;

public partial class death_screen : Control
{
	[Export] public Button button {get; private set;}
	[Export] public Label label {get; private set;}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager gameManager = GameManager.Instance;
		button.ButtonDown += ReturnToMenu;
		label.Text = "Score: " + gameManager.GetScore(); 
	}

	private void ReturnToMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Mainmenu/Menu.tscn");
	}
}
