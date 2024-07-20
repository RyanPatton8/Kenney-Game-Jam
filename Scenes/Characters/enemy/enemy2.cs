using Godot;
using System;

public partial class enemy2 : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public const float AttackRange = 10.0f

	[Export] public Area3D searchArea {get; private set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	

	public enum State{ 
		IDLE
		AttackRangeSTRUGGLE
		DIE
		
 	}

	public State CurrentState;

	public override void _Ready()
	{
		CurrentState = State.IDLE;
		target = getParent().GetNode<Node3D>("character");
		anim = GetNode<AnimationPlayer>("AnimationPlayer");

		searchArea.BodyEntered += MoveToward;
	}
	
	
	public override void _Process(double delta)
	{

	}

	private void Updateidle(){
		if (position.DistanceTo(target.position)> AttackRange)
		{
			CurrentState = State.Attack;
		}
	}

	public void UpdateAttack(double delta)
	{
		Vector3 t = target.position;
		t.y = position.Y;
		Vector3 direction = position.DirectionTo(t);
	}
	
	
		public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		Velocity = velocity;
		
		MoveAndSlide();
	}
	
	private void MoveToward(Body body)
	{
		body.GlobalPosition
	}
}
