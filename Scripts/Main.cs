using Godot;
using System;

public partial class Main : Node2D
{
	private OptionButton _Language;
	public static string Pos = "";

	public override void _Ready()
	{
		_Language = GetNode<OptionButton>("TextureRect/OptionButton");
		string Language = TranslationServer.GetLocale();
		switch(Language)
		{
			case "ru": _Language.Selected = 0; break;
			case "en": _Language.Selected = 1; break;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_option_button_item_selected(int i)
	{
		switch(i)
		{
			case 0: GetNode<LanguageManager>("/root/LanguageManager").ChangeLanguage("ru"); break;
			case 1: GetNode<LanguageManager>("/root/LanguageManager").ChangeLanguage("en"); break;
		}
	}

	private void _on_appeaiser_pressed()
	{
		GetTree().ChangeSceneToFile("res://Start.tscn");
	}

	private void _on_water_pressed()
	{
		Posit("Water");
	}

	private void _on_wine_pressed()
	{
		Posit("Wine");
	}

	private void Posit(string name)
	{
		GetTree().ChangeSceneToFile($"res://{name}.tscn");
		Pos = name;
	}
}
