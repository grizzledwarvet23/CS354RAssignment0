using Godot;
using System;

public partial class TrashBin : Area2D
{
	public static int score = 0;

	[Export]
	public string color;
	public AudioStreamPlayer2D scoreSound;

	//ready
	public override void _Ready() {
		scoreSound = GetNode<AudioStreamPlayer2D>("ScoreSound");
	}

	//since this is an area2D, just check for overlapping bodies. print hey if there is a body whose name contains "Trash"
	public override void _PhysicsProcess(double delta) {
		foreach (var body in GetOverlappingBodies()) {
			string name = body.Name;
			if (name.Contains("Trash")) {
				//delete the trash
				//GD.Print("hey");
				if(name.Contains(color)) {
					score++;
					scoreSound.Play();
				}
				body.QueueFree();
			}
		}
	}
}
