using Godot;
using System;

public partial class bullet : RigidBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += Hit;
	}

	public void Hit(Node body)
	{
		if (body.IsInGroup("player"))
			return;
		CallDeferred(nameof(Stick), body);
	}
	public void Stick(Node otherBody)
	{
		Vector3 GlobalPosition = this.GlobalTransform.Origin;
		Basis GlobalBasis = this.GlobalTransform.Basis;

		this.Freeze = true;
		this.GetParent().RemoveChild(this);
		otherBody.AddChild(this);

		this.GlobalTransform = new Transform3D(GlobalBasis, GlobalPosition);
		CollisionLayer = 0;
		CollisionMask = 0;
	}
}
