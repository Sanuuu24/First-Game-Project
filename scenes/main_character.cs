using System;
using Godot;
 
public partial class main_character : CharacterBody2D
{
	public const float Speed = 400.0f;
	public const float JumpVelocity = -600.0f;
 
	private AnimatedSprite2D sprite2d;
	
 
	public override void _Ready()
	{
		sprite2d = GetNode<AnimatedSprite2D>("Sprite2D");
		GD.Print(sprite2d);
	}

	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
 
	
 
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
 
		// Animations
		if (Math.Abs(velocity.X) > 1)
			sprite2d.Animation = "running";
		else
			sprite2d.Animation = "default";
 
		if (!IsOnFloor()) {
			velocity.Y += gravity * (float)delta;
			sprite2d.Animation = "jumping";
		}
 
		if (Input.IsActionJustPressed("lompat") && IsOnFloor())
			velocity.Y = JumpVelocity;
 

		float direction = Input.GetAxis("kiri", "kanan");
		if (direction != 0)
		{
			velocity.X = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 12);
		}
 
		Velocity = velocity;
		MoveAndSlide();
 

		bool isLeft = velocity.X < 0;
		sprite2d.FlipH = isLeft;
	}
}
