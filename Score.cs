using Godot;
using System;

public partial class Score : VBoxContainer
{

	public override void _Ready()
	{
		//child is a label. set its text to "Game Over! You got " + Singleton.score + " points!"
		GetNode<Label>("Label").Text = "Game Over! You got " + Singleton.score + " points!";
	}

}
