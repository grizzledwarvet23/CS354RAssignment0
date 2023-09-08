using Godot;
using System;

public partial class UpdateScore : Label
{

	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//this has a label. change it to the score, TrashBin.score
		Text = "Score: " + TrashBin.score;
	}
}
