﻿using System;
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

	public GameObject linearFlightCorrection;
	public GameObject angularFlightCorrection;

	public string whichCorrection;

	public Toggle smooth;

	public string whichController;

	public bool isSmooth;

	public GameObject SettingsMenu;

	public GameObject LevelMenu;

	public GameObject UiContainer;


	private void Start()
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = noJoyst.colors;

		whichController = PlayerPrefs.GetString("ControllerType", "NoJoystick");
		whichCorrection = PlayerPrefs.GetString("CorrectionType", "linear");
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

		angularFlightCorrection.SetActive(false);
		linearFlightCorrection.SetActive(true);

		if (whichCorrection == "angular")
		{
			angularFlightCorrection.SetActive(true);
			linearFlightCorrection.SetActive(false);
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

		if (cb.normalColor == disableColor)
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
	public void ChangeFlightCorrection()
	{
		switch (whichCorrection)
		{
			case "linear":

				angularFlightCorrection.SetActive(true);
				linearFlightCorrection.SetActive(false);
				whichCorrection = "angular";
				PlayerPrefs.SetString("CorrectionType", whichCorrection);

				break;

			case "angular":

				angularFlightCorrection.SetActive(false);
				linearFlightCorrection.SetActive(true);
				whichCorrection = "linear";
				PlayerPrefs.SetString("CorrectionType", whichCorrection);

				break;
		}
	}

}
