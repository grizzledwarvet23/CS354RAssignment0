using Godot;
using System;

public partial class TrashSpawner : Node2D
{

	[Export]
	private PackedScene prefabScene;
	
	private bool canSpawn = true;

	public static int trashCount = 0;

	private float spawnDelay = 2.5f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		prefabScene = ResourceLoader.Load<PackedScene>("res://trash.tscn");
		//SpawnObject();
	}

	//process. spawn object every 2 seconds
	public override void _Process(double delta) {
		//GD.Print(TrashBin.score);
		if(canSpawn && trashCount < 30) {
			canSpawn = false;
			SpawnObject();
			SetCanSpawn();
		}
	}

	//async function SetCanSpawn that sets it to true after x seconds
	public async void SetCanSpawn() {
		await ToSignal(GetTree().CreateTimer(spawnDelay), "timeout");
		spawnDelay = spawnDelay - 0.1f;
		spawnDelay = Mathf.Max(spawnDelay, 1);
		canSpawn = true;
	}

	public void SpawnObject() {
		if (prefabScene != null)
        {
			//instantiate prefab scene to the world scene as parent
			//for(int i = 0; i < 5; i++) {
				Node2D prefabInstance = (Node2D)prefabScene.Instantiate();
				//randomly set position from 0 to 500, y is yVal
				//randomly generate 4 numbers. select between spawning at the following for x ranges:
				//157-1013, 1686-2543, 3488-4345, 5361-6113:
				int x = (int)GD.RandRange(0, 4);
				float yVal = -200;
				if(x == 0) {
					prefabInstance.Position = new Vector2((float)GD.RandRange(157, 1013), yVal);
				} else if(x == 1) {
					prefabInstance.Position = new Vector2((float)GD.RandRange(1686, 2543), yVal);
				} else if(x == 2) {
					prefabInstance.Position = new Vector2((float)GD.RandRange(3488, 4345), yVal);
				} else {
					prefabInstance.Position = new Vector2((float)GD.RandRange(5361, 6113), yVal);
				}

				//prefabInstance.Position = new Vector2((float)GD.RandRange(0, 500), yVal);
				//add to world scene as child
				AddChild(prefabInstance);
				//randomly give the color of prefabInstance to either red, green, or blue
				int color = (int)GD.RandRange(0, 3);
				if(color == 0) {
					prefabInstance.GetNode<Sprite2D>("Sprite2D").SelfModulate = new Color(1, 0, 0);
					prefabInstance.Name = "Trash Red ";
				} else if(color == 1) {
					prefabInstance.GetNode<Sprite2D>("Sprite2D").SelfModulate = new Color(0, 1, 0);
					prefabInstance.Name = "Trash Green ";
				} else {
					prefabInstance.GetNode<Sprite2D>("Sprite2D").SelfModulate = new Color(0, 0, 1);
					prefabInstance.Name = "Trash Blue ";
					//hi
				}
				//prefabInstance.Name = "Trash instance " + i;d
				//YOsss
			//}
			trashCount++;
        }
        else
        {
            GD.Print("Prefab not loaded.");
        }
	}


	
}
