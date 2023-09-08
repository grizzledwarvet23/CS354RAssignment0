using Godot;
using System;

public partial class CharacterBody2D : Godot.CharacterBody2D
{
	public const float Speed = 600.0f;
	public const float JumpVelocity = -900.0f;
	private Vector2 armOffset = new Vector2(0, 10);
	private float punchForce = 1000.0f;
	public bool punching;

	private float armPositionX;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	
	private Area2D arm;
	
	private Sprite2D armSprite;

	private bool facingRight;

	private AudioStreamPlayer2D hitSound;
	
	public override void _Ready() {
		punching = false;
		arm = GetNode<Area2D>("Puncher (Area2D)");
		facingRight = true;
		armSprite = arm.GetNode<Sprite2D>("Sprite2D");
		GD.Print(armSprite);
		hitSound = GetNode<AudioStreamPlayer2D>("HitSound");
		//armPositionX = arm.GlobalPosition.X;
	}
	
	public override void _Process(double delta) {
		if(!punching) {
			arm.GlobalPosition = new Vector2(arm.GlobalPosition.X, GlobalPosition.Y + 0);
		}
	}
	
	

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;
		if (Input.IsActionJustPressed("ButtonLeft") && !punching) {
			LaunchFist();
		}
		
		if(velocity.Y > -100) {
			velocity.Y = Mathf.Min(velocity.Y + 10, 5000);
			
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if(facingRight && direction.X < 0 || !facingRight && direction.X > 0) {
				Flip();
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed / 4);
		}

		Velocity = velocity;
		MoveAndSlide();

	
	}

	private void Flip() {
		facingRight = !facingRight;
		//just change this objects scale to -1
		//get this objects sprite and change flip_h
		//GD.Print("flipping");
		Sprite2D thisOne = GetNode<Sprite2D>("Sprite2D");
		thisOne.FlipH = !thisOne.FlipH;
		//flip arm sprite in horizontal direction. access sprite and togle flip_h
		//armSprite.FlipH = !armSprite.FlipH;
		
	}
	
	private async void LaunchFist() {
		punching = true;
		hitSound.Play();
		for(int i = 0; i < 10; i++) {	
			//arm.Position += new Vector2(20, 0);
			//instead, do towards position of rotation
			arm.Position += new Vector2(30, 0).Rotated(arm.Rotation);
			await ToSignal(GetTree().CreateTimer(0.0000001f), "timeout");
		}
		
		for(int i = 0; i < 10; i++) {
			//arm.Position -= new Vector2(20, 0);
			arm.Position -= new Vector2(30, 0).Rotated(arm.Rotation);
			await ToSignal(GetTree().CreateTimer(0.0000001f), "timeout");
		}
		punching = false;
		
	}
}
