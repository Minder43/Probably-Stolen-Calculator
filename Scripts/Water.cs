using Godot;
using System;

public partial class Water : Node2D
{
	private Camera2D camera;
	private float ml_1 , ml_2 , per_1 , per_2 , per_w;
	private TextEdit _1 , _2 , _3 , _4;
	private Label _Text_1 , _Text_2;

	public override void _Ready()
	{
		camera = GetNode<Camera2D>("Camera2D");

		_Text_1 = GetNode<Label>("TextureRect/Label5");
		_Text_2 = GetNode<Label>("TextureRect2/Label5");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://Main.tscn");
	}

	private void _on_right_pressed()
	{
		camera.Position = new Vector2(590,242);
	}

	private void _on_left_pressed()
	{
		camera.Position = new Vector2(197,242);
	}

	private void _on_calculate_1_pressed()
	{
		_1 = GetNode<TextEdit>("TextureRect/Label3/TextEdit");
		_2 = GetNode<TextEdit>("TextureRect/Label3/TextEdit2");
		_3 = GetNode<TextEdit>("TextureRect/Label4/TextEdit");
		_4 = GetNode<TextEdit>("TextureRect/Label4/TextEdit2");

		ml_1 = float.Parse(_1.Text);
		per_1 = float.Parse(_2.Text);
		ml_2 = float.Parse(_3.Text);
		per_2 = float.Parse(_4.Text);

		Calculate_1();
	}

	private void _on_calculate_2_pressed()
	{
		_1 = GetNode<TextEdit>("TextureRect2/Label3/TextEdit");
		_2 = GetNode<TextEdit>("TextureRect2/Label3/TextEdit2");
		_3 = GetNode<TextEdit>("TextureRect2/Label4/TextEdit");
		_4 = GetNode<TextEdit>("TextureRect2/Label4/TextEdit2");
		
		ml_1 = float.Parse(_1.Text);
		per_1 = float.Parse(_2.Text);
		per_2 = float.Parse(_3.Text);
		per_w = float.Parse(_4.Text);

		Calculate_2();
	}

	private void Calculate_1()
	{
		float Cost = (ml_1 * per_1 + ml_2 * per_2) / (ml_1 + ml_2);
		_Text_1.Text = Tr("TOTAL_QUALITY_PERCENT") + $": {(float)Math.Round(Cost , 2)}";
	}

	private void Calculate_2()
	{
		float Cost = ml_1 * (per_w - per_1) / (per_2 - per_w);
		_Text_2.Text = Tr("REQUIRED_AMOUNT_ML") + $": {(float)Math.Round(Cost , 2)}";
	}
}
