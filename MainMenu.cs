using Godot;
using System;

public partial class MainMenu : Control
{
	public void OnPlayPressed() {
		GetTree().ChangeScene("res://main.tscn");
	}
}
