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
		GetTree().ChangeSceneToFile("res://Head.tscn");
	}

	private void _on_sendwith_pressed()
	{
		GetTree().ChangeSceneToFile("res://Sendwith.tscn");
	}

	private void _on_cartridge_pressed()
	{
		GetTree().ChangeSceneToFile("res://Cartridge.tscn");
	}

	private void _on_option_button_item_selected(int i)
	{
		switch(i)
		{
			case 0: GetNode<LanguageManager>("/root/LanguageManager").ChangeLanguage("ru"); break;
			case 1: GetNode<LanguageManager>("/root/LanguageManager").ChangeLanguage("en"); break;
		}
	}

}
