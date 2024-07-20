using Godot;
using System;

public partial class enemy2 : CharacterBody3D
{
	public const float Speed = 5.0f;
	
	public const float JumpVelocity = 4.5f;

	public const float AttackRange = 10.0f;

	public AnimationPlayer anim;
	
	public Node3D target;

	public float gravity = -9.85f;
	
	public Vector3 velocity;

	

	public enum State{ 
		IDLE,
		Attack,
		Struggle,
		DIE
		
 	}

	public State CurrentState;

	public override void _Ready()
	{
		CurrentState = State.IDLE;
		target = GetParent().GetNode<Node3D>("character");
		anim = GetNode<AnimationPlayer>("AnimationPlayer");

		// searchArea.BodyEntered += MoveToward;
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		switch(CurrentState){
			case State.IDLE:
			Updateidle();
			break;
			case State.Attack:
			UpdateAttack(delta);
			break;

		}
	}

	private void Updateidle(){
		if (Position.DistanceTo(target.Position)> AttackRange)
		{
			CurrentState = State.Attack;
		}
	}

	public void UpdateAttack(double delta)
	{
		Vector3 t = target.Position;
		t.Y = Position.Y;
		Vector3 direction = Position.DirectionTo(t);
		velocity = Velocity;
		velocity.X = direction.X * Speed;
		velocity.Y = direction.Y * Speed;

		Velocity = velocity;
		MoveAndSlide();
	}
	
	

}
