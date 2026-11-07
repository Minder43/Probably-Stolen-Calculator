using Godot;
using System;

public partial class Start : Node2D
{
	public static int Cost = 0;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{

	}

	private void _on_head_pressed()
	{
		Posit("Head");
	}

	private void _on_sendwith_pressed()
	{
		Posit("Sendwith");
	}

	private void _on_cartridge_pressed()
	{
		Posit("Cartridge");
	}

	private void Posit(string name)
	{
		GetTree().ChangeSceneToFile($"res://{name}.tscn");
		Main.Pos = name;
	}

	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}

}
