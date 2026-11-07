using Godot;
using System;

public partial class Finish : Node2D
{
	private Label Cost;

	public override void _Ready()
	{
		Cost = GetNode<Label>("TextureRect/Label/Label");
		Cost.Text = Start.Cost.ToString() + "Кр";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Start.tscn");
	}

	private void _on_again_pressed()
	{
		switch(Main.Pos)
		{
			case "Head" : GetTree().ChangeSceneToFile("res://Head.tscn"); break;
			case "Sendwith" : GetTree().ChangeSceneToFile("res://Sendwith.tscn"); break;
			case "Cartridge" : GetTree().ChangeSceneToFile("res://Cartridge.tscn"); break;
		}
	}
}
