using Godot;
using System;

public partial class Sendwith : Node2D
{
	private bool fake = false , damage = false , darve = false , ribwith = false , sesame = false , meat = false , cucumber = false;
	private int month = 0 , year = 0 , place = 0;

	private CheckBox _Check , _Check2 , _Check3 , _Check4 , _Check5 , _Check6 ;
	private SpinBox _Spin , _Spin2;
	private TextEdit _Text;

	public override void _Ready()
	{
		_Check = GetNode<CheckBox>("TextureRect/CheckBox");
		_Check2 = GetNode<CheckBox>("TextureRect/Label7/CheckBox");
		_Check3 = GetNode<CheckBox>("TextureRect/Label7/CheckBox2");
		_Check4 = GetNode<CheckBox>("TextureRect/Label7/CheckBox3");
		_Check5 = GetNode<CheckBox>("TextureRect/Label8/CheckBox");
		_Check6 = GetNode<CheckBox>("TextureRect/Label8/CheckBox2");

		_Spin = GetNode<SpinBox>("TextureRect/Label4/HBoxContainer/SpinBox");
		_Spin2 = GetNode<SpinBox>("TextureRect/Label4/HBoxContainer/SpinBox2");

		_Text = GetNode<TextEdit>("TextureRect/Label5/SpinBox");
	}

	public override void _Process(double delta)
	{

	}

	private void _on_rate_pressed()
	{
		damage = _Check.ButtonPressed;
		darve = _Check5.ButtonPressed;
		ribwith = _Check6.ButtonPressed;
		sesame = _Check2.ButtonPressed;
		cucumber = _Check3.ButtonPressed;
		meat = _Check4.ButtonPressed;

		month = (int)_Spin.Value;
		year = (int)_Spin2.Value;
		place = int.Parse(_Text.Text);

		Calculate();
	}

	private void Calculate()
	{
		int Par = 0;
		int[] hidh = {1 , 2 , 5 , 8 , 21 , 34};
		int[] _short = {7 , 15 , 18 , 26 , 31};
		int Cost = 150;
		fake = damage; 

		if((year >= 425 && year <= 429)) {Cost += 80; Par = 1;}
		else if((year >= 432 && year <= 438)) {Cost += 0; Par = 2;}
		else fake = true;

		if((month == 1 && year == 425) || (month == 12 && year == 438)) Cost += 50;

		if(place <= 35 && Array.Exists(hidh, x => x == place)) {Cost += 50;}
		else if(place <= 35 && Array.Exists(_short, x => x == place)) {Cost -= 50;}
		else fake = true;

		if(sesame && cucumber) {Cost += 300;}
		else if((sesame == false && cucumber) || (cucumber == false && sesame)) {fake = true;}

		if(meat) Cost += 800;

		if((darve || ribwith) && Par != 1) {fake = true;}

		if(fake) Cost = 40;
		if(Cost < 40) Cost = 40;

		Start.Cost = Cost;
		GetTree().ChangeSceneToFile("res://Finish.tscn");
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Start.tscn");
	}
}
