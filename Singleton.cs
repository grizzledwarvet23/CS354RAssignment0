using Godot;
using System;

public partial class Singleton : Node
{

	public static int score = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(score);
	}

	
}
