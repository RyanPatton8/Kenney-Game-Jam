using Godot;
using System;

public partial class GameManager : Node
{
	private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public override void _EnterTree()
    {
        if (_instance == null)
        {
            _instance = this;
            GD.Print("GameManager instance created.");
        }
        else
        {
            GD.Print("GameManager instance already exists.");
            QueueFree();
        }
    }
	public int score = 0;

	public int GetScore()
	{
		return score;
	}
	public void IncreaseScore()
	{
		score++;
	}

	public void ResetScore()
	{
		score = 0;
	}
}
