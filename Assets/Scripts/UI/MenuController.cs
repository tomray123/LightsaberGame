using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public GameObject MainMenu;

	public Button noJoyst;
	public Button flJoyst;
	public Button clJoyst;

	public Toggle smooth;

	public static string whichController;

	public static bool isSmooth;

	public GameObject SettingsMenu;


    private void Start()
    {
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = noJoyst.colors;

		whichController = PlayerPrefs.GetString("ControllerType", "NoJoystick");
		isSmooth = Convert.ToBoolean(PlayerPrefs.GetInt("SmoothSetting", 0));

		switch (whichController)
		{
			case "NoJoystick":

				cb.normalColor = activeColor;
				noJoyst.colors = cb;

				cb.normalColor = disableColor;
				flJoyst.colors = cb;
				clJoyst.colors = cb;

				break;

			case "FloatJoystick":

				cb.normalColor = activeColor;
				flJoyst.colors = cb;

				cb.normalColor = disableColor;
				noJoyst.colors = cb;
				clJoyst.colors = cb;

				break;

			case "ClassicJoystick":

				cb.normalColor = activeColor;
				clJoyst.colors = cb;

				cb.normalColor = disableColor;
				noJoyst.colors = cb;
				flJoyst.colors = cb;

				break;
		}

		if (isSmooth)
		{
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			smooth.colors = cb;
		}
		else
		{
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			smooth.colors = cb;
		}
	}

    public void OpenSettings()
	{
		MainMenu.SetActive(false);
		SettingsMenu.SetActive(true);
	}

	public void CloseSettings()
	{
		MainMenu.SetActive(true);
		SettingsMenu.SetActive(false);
	}

	public void ChangeController(Button pressed)
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = pressed.colors;

		cb.normalColor = activeColor;
		pressed.colors = cb;

		cb.normalColor = disableColor;
		

		switch (pressed.name)
        {
			case "No Joystick Button":

				flJoyst.colors = cb;
				clJoyst.colors = cb;

				whichController = "NoJoystick";
				PlayerPrefs.SetString("ControllerType", "NoJoystick");

				break;

			case "Float Joystick Button":

				noJoyst.colors = cb;
				clJoyst.colors = cb;

				whichController = "FloatJoystick";
				PlayerPrefs.SetString("ControllerType", "FloatJoystick");

				break;

			case "Classic Joystick Button":

				flJoyst.colors = cb;
				noJoyst.colors = cb;

				whichController = "ClassicJoystick";
				PlayerPrefs.SetString("ControllerType", "ClassicJoystick");

				break;
		}
	}

	public void ChangeSmooth(Toggle pressed)
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = pressed.colors;

		if (pressed.isOn)
        {
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			pressed.colors = cb;
			isSmooth = true;
			PlayerPrefs.SetInt("SmoothSetting", 1);
		}
        else
        {
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			pressed.colors = cb;
			isSmooth = false;
			PlayerPrefs.SetInt("SmoothSetting", 0);
		}
	}

}
