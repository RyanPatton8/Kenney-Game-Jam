using Godot;
using System;
using System.Collections.Generic;

public partial class CharacterBody3D : Godot.CharacterBody3D
{
	public const float Speed = 5f;
	public const float JumpVelocity = 4.5f;
	private List<RigidBody3D> bullets = new List<RigidBody3D>();

	[Export] public float MouseSensitivity = 0.1f;
	[Export] public float MoveSpeed = 5.0f;
	[Export] public Node3D neck {get; private set;}
	[Export] public Camera3D camera  {get; private set;}

	[Export] public Marker3D attackPos {get; private set;}

	[Export] public PackedScene bullet {get; private set;}
	[Export] public PackedScene lightning {get; private set;}

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleAttacks();
		Move(delta);
	}

	public void Move(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("moveLeft", "moveRight", "moveForward", "moveBackward");
		Vector3 direction = (neck.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseMotion)
		{
			InputEventMouseMotion mouseMotion = @event as InputEventMouseMotion;
			neck.RotateY(-mouseMotion.Relative.X * MouseSensitivity);
			camera.RotateX(-mouseMotion.Relative.Y * MouseSensitivity);

			Vector3 cameraRot = camera.Rotation;
			cameraRot.X = Mathf.Clamp(cameraRot.X, Mathf.DegToRad(-80f), Mathf.DegToRad(80f));
			camera.Rotation = cameraRot;

		}
	}

	public void HandleAttacks()
	{
		if (Input.IsActionJustPressed("fire"))
		{
			Fire();
		}
		else if (Input.IsActionJustPressed("shock"))
		{
			Shock();
		}
	}

	public void Fire()
	{
		Vector3 aimDirection = (attackPos.GlobalPosition - GlobalPosition).Normalized();
		RigidBody3D instance = (RigidBody3D)bullet.Instantiate();
		GetTree().Root.AddChild(instance);
		bullets.Add(instance);
		instance.GlobalPosition = attackPos.GlobalPosition;
		instance.ApplyImpulse(aimDirection * 30);
	}

	//take mid-point of 2 list items and stretch to match the distance until null
	public void Shock()
	{
		for(int i = 0; i < bullets.Count - 1; i++)
		{
			//Instantiate Lightning
			GD.Print(bullets.Count);
			Area3D instance = (Area3D)lightning.Instantiate();
			GetTree().Root.AddChild(instance);
			//Calculate MidPoint between bullets
			Vector3 midPoint = (bullets[i].GlobalPosition + bullets[i+1].GlobalPosition) / 2;
			//Calculate Direction
			Vector3 direction = (bullets[i+1].GlobalPosition - bullets[i].GlobalPosition).Normalized();
			float distance = bullets[i].GlobalPosition.DistanceTo(bullets[i+1].GlobalPosition);

			instance.GlobalPosition = midPoint;
			CollisionShape3D collisionShape = instance.GetNode<CollisionShape3D>("CollisionShape3D");
			MeshInstance3D mesh = instance.GetNode<MeshInstance3D>("MeshInstance3D");

			collisionShape.Scale = new Vector3(collisionShape.Scale.X, collisionShape.Scale.Y, distance);
			mesh.Scale = collisionShape.Scale;
			instance.LookAt(bullets[i+1].GlobalPosition, Vector3.Up);
		}
	}
}
