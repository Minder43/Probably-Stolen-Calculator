using Godot;
using System;

public partial class Cartridge : Node2D
{
	private string manufacturer = "" , condition1 = "" , condition2 = "" , plazmacolor = "";
	private bool No = false;
	
	private CheckBox _1 , _2 , _3 , _4 , _No;
	private OptionButton _optionButton, _optionButton2, _optionButton3;
	private Label error , errorZ;

	public override void _Ready()
	{
		_1 = GetNode<CheckBox>("TextureRect/Label3/CheckBox");
		_2 = GetNode<CheckBox>("TextureRect/Label3/CheckBox2");
		_3 = GetNode<CheckBox>("TextureRect/Label3/CheckBox3");
		_4 = GetNode<CheckBox>("TextureRect/Label3/CheckBox4");
		_No = GetNode<CheckBox>("TextureRect/CheckBox");

		error = GetNode<Label>("Label6");
		errorZ = GetNode<Label>("Label7");

		_optionButton = GetNode<OptionButton>("TextureRect/Label4/OptionButton");
		_optionButton2 = GetNode<OptionButton>("TextureRect/Label4/OptionButton2");
		_optionButton3 = GetNode<OptionButton>("TextureRect/Label5/OptionButton");
	}

	public override void _Process(double delta)
	{
	}

	private void _on_rate_pressed()
	{
		No = !_No.ButtonPressed;
		if (_1.ButtonPressed && !_2.ButtonPressed && !_3.ButtonPressed && !_4.ButtonPressed) {manufacturer = "WLD";}
		else if (!_1.ButtonPressed && _2.ButtonPressed && !_3.ButtonPressed && !_4.ButtonPressed) {manufacturer = "RNR";}
		else if (!_1.ButtonPressed && !_2.ButtonPressed && _3.ButtonPressed && !_4.ButtonPressed) {manufacturer = "ZHS";}
		else if (!_1.ButtonPressed && !_2.ButtonPressed && !_3.ButtonPressed && _4.ButtonPressed) {manufacturer = "IGN";}
		else if (!_1.ButtonPressed && !_2.ButtonPressed && !_3.ButtonPressed && !_4.ButtonPressed) {if(No){ErrorZ(); return;}}
		else {if(No){Error(); return;}}

		int c1 = _optionButton.GetSelectedId();
		int c2 = _optionButton2.GetSelectedId();
		int p = _optionButton3.GetSelectedId();

		switch(c1)
		{
			case 0: condition1 = "Standart"; break;
			case 1: condition1 = "Light wear"; break;
			case 2: condition1 = "Significant wear"; break;
		}

		switch(c2)
		{
			case 0: condition2 = "Nothing"; break;
			case 1: condition2 = "Cracks"; break;
			case 2: condition2 = "Visible screws"; break;
			case 3: condition2 = "Reinforced body"; break;
		}

		switch(p)
		{
			case 0: plazmacolor = "Standart"; break;
			case 1: plazmacolor = "Low quality"; break;
			case 2: plazmacolor = "High quality"; break;
		}

		Calculate();
	}

	private void Calculate()
	{
		int Cost = 300; 

		if(condition1 == "Light wear") {Cost -= 45;}
		else if(condition1 == "Significant wear") {Cost -= 90;}

		if(condition2 == "Cracks") {Cost -= 150;}
		else if(condition2 == "Visible screws") {Cost -= 30;}
		else if(condition2 == "Reinforced body") {Cost += 60;}

		if(plazmacolor == "Low quality") {Cost -= 150;}
		else if(plazmacolor == "High quality") {Cost += 150;}

		if(manufacturer == "WLD" && No && (plazmacolor == "Low quality" || plazmacolor == "Standart")) Cost += 60;
		if(manufacturer == "ZHS" && No && plazmacolor == "Standart" && (condition2 == "Nothing" || condition2 == "Reinforced body")) Cost += 120;
		if(manufacturer == "RNR" && No && plazmacolor == "Standart" && condition2 != "Cracks") Cost += 120;
		if(manufacturer == "IGN" && No && condition2 == "Nothing" && plazmacolor == "High quality") Cost += 750;

		if(Cost < 90) Cost = 90;

		Start.Cost = Cost;
		GetTree().ChangeSceneToFile("res://Finish.tscn");
	}

	private async void Error()
	{
		error.Visible = true;
		await ToSignal(GetTree().CreateTimer(3.0), "timeout");
		error.Visible = false;
	}

	private async void ErrorZ()
	{
		errorZ.Visible = true;
		await ToSignal(GetTree().CreateTimer(3.0), "timeout");
		errorZ.Visible = false;
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Start.tscn");
	}

	private void _on_option_button_item_selected(int i)
	{
		switch(i)
		{
			case 0: TranslationServer.SetLocale("ru"); break;
			case 1: TranslationServer.SetLocale("en"); break;
		}
	}
}
