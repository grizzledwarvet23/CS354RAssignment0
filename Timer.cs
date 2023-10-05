using Godot;
using System;

public partial class Timer : Label
{
	public int timerValue = 90;

	//ready
	public override void _Ready()
	{
		timerValue = 90;
		WaitAndChange();
	}
	
	//async function that waits for second, subtracts timer value by 1, and sets text to timer value as "Timer :" + timerValue
	private async void WaitAndChange()
	{
		await ToSignal(GetTree().CreateTimer(1), "timeout");
		timerValue -= 1;
		Text = "Time: " + timerValue;
		if(timerValue <= 0) {
			//preload game over scene and print it out
			// var scene = Preload("GameOver.tscn");
			// GD.Print(scene);
			Singleton.score = TrashBin.score;
			GetTree().ChangeSceneToFile("res://GameOver.tscn");
		} else {
			WaitAndChange();
		}
	}
}
