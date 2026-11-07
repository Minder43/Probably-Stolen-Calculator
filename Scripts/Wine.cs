using Godot;
using System;

public partial class Wine : Node2D
{
	private OptionButton _1 , _2 , _3 , _4;
	private string berry_1 , berry_2 , yeast , water , Winer;
	private Label Text , Text2;
	private int b1 , b2 , y , w;

	public override void _Ready()
	{
		_1 = GetNode<OptionButton>("TextureRect/Label3/OptionButton");
		_2 = GetNode<OptionButton>("TextureRect/Label3/OptionButton2");
		_3 = GetNode<OptionButton>("TextureRect/Label4/OptionButton");
		_4 = GetNode<OptionButton>("TextureRect/Label5/OptionButton");

		Text = GetNode<Label>("TextureRect/Label6");
		Text2 = GetNode<Label>("TextureRect/Label7");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}

	private void _on_rate_pressed()
	{
		b1 = _1.GetSelectedId();
		b2 = _2.GetSelectedId();
		y = _3.GetSelectedId();
		w = _4.GetSelectedId();

		Calculate();
	}

	private void Calculate()
	{
		int Cost = 0;
		int Day = 4; 

		switch(b1)
		{
			case 0: berry_1 = "No"; break;
			case 1: berry_1 = "Small"; Cost += 2; break;
			case 2: berry_1 = "Normal"; Cost += 1; break;
			case 3: berry_1 = "Unripe"; break;
			case 4: berry_1 = "Overripe"; break;
			case 5: berry_1 = "Mold"; Cost -= 1; break;
		}

		switch(b2)
		{
			case 0: berry_2 = "No"; break;
			case 1: berry_2 = "Small"; Cost += 2; break;
			case 2: berry_2 = "Normal"; Cost += 1; break;
			case 3: berry_2 = "Unripe"; break;
			case 4: berry_2 = "Overripe"; break;
			case 5: berry_2 = "Mold"; Cost -= 1; break;
		}		

		switch(w)
		{
			case 0: water = "Pure"; Cost += 1; break;
			case 1: water = "Quality"; Cost += 1; break;
			case 2: water = "Standard"; break;
			case 3: water = "Ghostly"; Cost -= 1; break;
			case 4: water = "Technical"; Cost -= 2; break;
			case 5: water = "Sewage"; Cost -= 3; break;
		}

		switch(y)
		{
			case 0: yeast = "No"; break;
			case 1: yeast = "Yeast"; Cost += 1; Day = 2; break;
			case 2: yeast = "Super yeast"; Cost += 1; Day = 1; break;
			case 3: yeast = "Synthetic yeast"; Cost = -100; Day = 2; break;
		}

		if(berry_1 == "No" && berry_2 == "No") Cost = -50;

		if(Cost > -10) Text.Text = Tr("RIPENING_DAYS") + $": {Day}";

		switch(Cost)
		{
			case > 5: Winer = Tr("SPECIAL_VINTAGE"); break;
			case 5: Winer = Tr("AGED"); break;
			case 4 or 3: Winer = Tr("MASS_PRODUCTION"); break;
			case 2: Winer = Tr("HOMEMADE"); break;
			case 1: Winer = Tr("SWILL"); break;
			case < 1 and > - 50: Winer = Tr("TO_THE_TRASH"); break;
			case -50: Winer = Tr("MISSING_INGREDIENTS"); break;
			case < -50: Winer = Tr("SYNTHETIC_WINE"); break;
		}

		Text2.Text = Tr("FINAL_WINE") + $": {Winer}";
	}
}
