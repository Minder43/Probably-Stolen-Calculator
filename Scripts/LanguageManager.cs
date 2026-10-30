using Godot;
using System;

public partial class LanguageManager : Node
{
	public override void _Ready()
	{
		TranslationServer.SetLocale("en");
	}

	public void ChangeLanguage(string langCode)
    {
        TranslationServer.SetLocale(langCode);
    }
}
