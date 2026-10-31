using Godot;
using System;

public partial class Head : Node2D
{
	private string material = "", state = "", interfaceType = "";
	private bool armor = false, blade = false;
	
	private OptionButton _optionButton, _optionButton2, _optionButton3;
	private CheckBox _checkBox, _checkBox2;

	public override void _Ready()
	{
		_optionButton = GetNode<OptionButton>("TextureRect/Label3/OptionButton");
		_optionButton2 = GetNode<OptionButton>("TextureRect/Label4/OptionButton");
		_optionButton3 = GetNode<OptionButton>("TextureRect/Label5/OptionButton");

		_checkBox = GetNode<CheckBox>("TextureRect/Label6/CheckBox");
		_checkBox2 = GetNode<CheckBox>("TextureRect/Label6/CheckBox2");
	}

	public override void _Process(double delta)
	{
	}

	private void _on_count_pressed()
	{
		int m = _optionButton.GetSelectedId();
		int s = _optionButton2.GetSelectedId();
		int i = _optionButton3.GetSelectedId();

		armor = _checkBox.ButtonPressed; 
		blade = _checkBox2.ButtonPressed;

		switch(m)
		{
			case 0: 
				material = "Aluminum alloy"; 
				break;
			case 1: 
				material = "Carbon alloy"; 
				break;
			case 2: 
				material = "Gold coating"; 
				break;
		}

		switch(s)
		{
			case 0: 
				state = "No features"; 
				break;
			case 1: 
				state = "Wear"; 
				break;
			case 2: 
				state = "Damage"; 
				break;
		}

		switch(i)
		{
			case 0: 
				interfaceType = "EMG"; 
				break;
			case 1: 
				interfaceType = "EMG+"; 
				break;
			case 2: 
				interfaceType = "Neural"; 
				break;
			case 3: 
				interfaceType = "Not specified"; 
				break;
		}

		Calculate();
	}

	private void Calculate()
	{
		int Cost = 100;

		if(material == "Gold coating") {Cost += 200;}
		else if(material == "Carbon alloy") {Cost += 50;}

		if(state ==  "Wear") {Cost -= 35;}
		else if(state == "Damage") {Cost -= 85;}

		if(interfaceType == "Not specified") {Cost -= 60;}
		else if(interfaceType == "Neural") {Cost += 120;}
		else if(interfaceType == "EMG+") {Cost += 45;}

		if(armor == true) Cost += 50;
		if(blade == true) Cost += 80;

		if(material == "Carbon alloy" && (interfaceType == "Neural" || interfaceType == "EMG+") && state ==  "No features" && armor == true && blade == true) Cost += 100;
		if(material == "Gold coating" && interfaceType == "Neural" && state ==  "No features") Cost += 200;

		if(Cost < 50) Cost = 50;

		Start.Cost = Cost;
		GetTree().ChangeSceneToFile("res://Finish.tscn");
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Start.tscn");
	}
}