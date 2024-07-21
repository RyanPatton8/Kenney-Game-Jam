using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node3D
{
	private Node3D _player;
	private Timer _spawnTimer;
	private Queue<Vector3> _positionHistory = new Queue<Vector3>();
	private const float RecordInterval = 0.1f; // Record position every 0.1 seconds
	private const float SpawnInterval = 5.0f;
	private const int HistoryLength = (int)(SpawnInterval / RecordInterval);

	// Hardcoded path to the player node
	private const string PlayerPath = "Player"; // Adjust this path to match your scene structure

	// Hardcoded path to the child scene
	private const string ChildScenePath = "res://Scenes/Characters/enemy/enemy2.tscn"; // Adjust this path to your enemy scene file

	public override void _Ready()
	{
		// Get the player node using the hardcoded path
		_player = GetNode<Node3D>(PlayerPath);
		if (_player == null)
		{
			GD.PrintErr("Player node not found at path: " + PlayerPath);
			return;
		}

		// Set up position recording timer
		var recordTimer = new Timer();
		recordTimer.WaitTime = RecordInterval;
		recordTimer.Timeout += RecordPlayerPosition;
		AddChild(recordTimer);
		recordTimer.Start();

		// Set up spawn timer
		_spawnTimer = new Timer();
		_spawnTimer.WaitTime = SpawnInterval;
		_spawnTimer.Timeout += SpawnChildAtOldPosition;
		AddChild(_spawnTimer);
		_spawnTimer.Start();
	}

	private void RecordPlayerPosition()
	{
		_positionHistory.Enqueue(_player.GlobalPosition);
		if (_positionHistory.Count > HistoryLength)
		{
			_positionHistory.Dequeue();
		}
	}

	private void SpawnChildAtOldPosition()
	{
		if (_positionHistory.Count == HistoryLength)
		{
			Vector3 oldPosition = _positionHistory.Peek();
			var childScene = ResourceLoader.Load<PackedScene>(ChildScenePath);
			if (childScene == null)
			{
				GD.PrintErr("Child scene not found at path: " + ChildScenePath);
				return;
			}
			
			var child = childScene.Instantiate<Node3D>();
			GetTree().Root.AddChild(child);
			child.GlobalPosition = oldPosition;
		}
	}
}
