using Godot;
using System;

public partial class Cartridge : Node2D
{
	private string manufacturer = "" , condition = "" , plazmacolor = "" , condit = "";
	private bool No = false , cracks = false , visible_screws = false , reinforced_body = false;
	
	private CheckBox _1 , _2 , _3 , _4 , _No , _checkBox , _checkBox2 , _checkBox3;
	private OptionButton _optionButton, _optionButton3;
	private Label error , errorZ;

	public override void _Ready()
	{
		_1 = GetNode<CheckBox>("TextureRect/Label3/CheckBox");
		_2 = GetNode<CheckBox>("TextureRect/Label3/CheckBox2");
		_3 = GetNode<CheckBox>("TextureRect/Label3/CheckBox3");
		_4 = GetNode<CheckBox>("TextureRect/Label3/CheckBox4");
		_No = GetNode<CheckBox>("TextureRect/CheckBox");

		_checkBox = GetNode<CheckBox>("TextureRect/Label4/CheckBox");
		_checkBox2 = GetNode<CheckBox>("TextureRect/Label4/CheckBox2");
		_checkBox3 = GetNode<CheckBox>("TextureRect/Label4/CheckBox3");

		error = GetNode<Label>("Label6");
		errorZ = GetNode<Label>("Label7");

		_optionButton = GetNode<OptionButton>("TextureRect/Label4/OptionButton");
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

		int c = _optionButton.GetSelectedId();
		int p = _optionButton3.GetSelectedId();

		cracks = _checkBox.ButtonPressed;
		visible_screws = _checkBox2.ButtonPressed;
		reinforced_body = _checkBox3.ButtonPressed;

		if(!cracks && !visible_screws && !reinforced_body) condit = "Nothing";

		switch(c)
		{
			case 0: condition = "Standart"; break;
			case 1: condition = "Light wear"; break;
			case 2: condition = "Significant wear"; break;
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

		if(condition == "Light wear") {Cost -= 45;}
		else if(condition == "Significant wear") {Cost -= 90;}

		if(cracks ) {Cost -= 150;}
		else if(visible_screws) {Cost -= 30;}
		else if(reinforced_body) {Cost += 60;}

		if(plazmacolor == "Low quality") {Cost -= 150;}
		else if(plazmacolor == "High quality") {Cost += 150;}

		if(manufacturer == "WLD" && No && (plazmacolor == "Low quality" || plazmacolor == "Standart")) Cost += 60;
		if(manufacturer == "ZHS" && No && plazmacolor == "Standart" && (condit == "Nothing" || reinforced_body)) Cost += 120;
		if(manufacturer == "RNR" && No && plazmacolor == "Standart" && !cracks) Cost += 120;
		if(manufacturer == "IGN" && No && condit == "Nothing" && plazmacolor == "High quality") Cost += 750;

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
