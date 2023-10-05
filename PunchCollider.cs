using Godot;
using System;

public partial class PunchCollider : Area2D
{

	bool launching = false;

	[Export]
	public CharacterBody2D characterBody;


	//in process, rotate this object to point towards the mouse
	public override void _Process(double delta) {
		if(!characterBody.punching) {
			Vector2 mousePos = GetGlobalMousePosition();
			Vector2 direction = mousePos - GlobalPosition;
			float angle = direction.Angle();
			Rotation = angle;
		}
	}
	
	//check every frame get overlapping bodies
	public override void _PhysicsProcess(double delta) {
		if(!launching) {
			foreach (var body in GetOverlappingBodies()) {
			
				//check if the body's name contains the word "enemy"
				string name = body.Name;
				GD.Print(name);
				if (name.Contains("Trash") || name.Contains("trash")) {
					RigidBody2D enemy = (RigidBody2D)body;
					Vector2 direction = body.GlobalPosition - GlobalPosition;
					direction = direction.Normalized();
					//create a boolean checking if enemy is grounded
					//print enemy y velocity
					//check magnitude of y velocity of enemy is close to 0
					float enemyYSpeed = Math.Abs(enemy.LinearVelocity.Y);
					if(direction.Y > 0 && enemyYSpeed < 0.3f) {
						if(direction.Y > 0.05) {
							direction = new Vector2(direction.X * 2	, -direction.Y * 1.5f);
						} else {
							direction = new Vector2(direction.X, 0);
						}
					}
					//GD.Print(direction * 500);
					//set enemy velocity to its current self but reduce greatly
					enemy.LinearVelocity = enemy.LinearVelocity / 1.5f;
					enemy.ApplyCentralImpulse(direction * 670);
					SetPunching();
					

				}
			}
		}
	}

	public async void SetPunching() {
		launching = true;
		await ToSignal(GetTree().CreateTimer(0.45f), "timeout");
		launching = false;

	}
}
