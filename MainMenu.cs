using Godot;
using System;

public partial class MainMenu : Control
{

	public int score;

	//create start and process:
	public override void _Ready()
	{
		//connect the buttons to the functions:
		// GetNode<Button>("Start").Connect("pressed", this, nameof(OnPlayPressed));
		// GetNode<Button>("Quit").Connect("pressed", this, nameof(OnQuitPressed));
		simultaneousScene = ResourceLoader.Load<PackedScene>("res://Scenes/main.tscn");
	}




	[Export]
	public PackedScene simultaneousScene;
	public void OnPlayPressed() {
		
		GetTree().ChangeSceneToFile("res://main.tscn");
		//get rid of this scene:
		
	}

	public void OnToMenuPressed() {
		GetTree().ChangeSceneToFile("res://MainMenu.tscn");
	}
}
