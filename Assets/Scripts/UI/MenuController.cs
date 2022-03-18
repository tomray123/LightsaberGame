using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
	public GameObject MainMenu;

	public GameObject noJoyst;
	public GameObject flJoyst;
	public GameObject clJoyst;

	public GameObject linearFlightCorrection;
	public GameObject angularFlightCorrection;

	public string whichCorrection;

	public Toggle smooth;
	public GameObject smoothOnText;
	public GameObject smoothOffText;

	public Toggle visualEffects;
	public GameObject visualEffectsOnText;
	public GameObject visualEffectsOffText;
	[SerializeField]
	private GameObject visualEffectsPrefab;

	public string whichController;

	private bool isSmooth;

	private bool isGraphicsEnabled;

	private GameSettingsControllerSO gameSettings;

	public GameObject SettingsMenu;

	public GameObject LevelMenu;

	public GameObject UiContainer;


	private void Start()
	{
		gameSettings = Resources.Load<GameSettingsControllerSO>("ScriptableObjects/GameSettingsController");
		whichController = gameSettings.GetControllerType();
		whichCorrection = gameSettings.GetCorrectionType();
		isSmooth = gameSettings.GetSmoothSetting();
		isGraphicsEnabled = gameSettings.GetVisualEffects();

		switch (whichController)
		{
			case "NoJoystick":

				noJoyst.SetActive(true);
				flJoyst.SetActive(false);
				clJoyst.SetActive(false);

				break;

			case "FloatJoystick":

				noJoyst.SetActive(false);
				flJoyst.SetActive(true);
				clJoyst.SetActive(false);

				break;

			case "ClassicJoystick":

				noJoyst.SetActive(false);
				flJoyst.SetActive(false);
				clJoyst.SetActive(true);

				break;
		}

		angularFlightCorrection.SetActive(false);
		linearFlightCorrection.SetActive(true);

		if (whichCorrection == "angular")
		{
			angularFlightCorrection.SetActive(true);
			linearFlightCorrection.SetActive(false);
		}

		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = smooth.colors;

		if (isSmooth)
		{
			smoothOnText.SetActive(true);
			smoothOffText.SetActive(false);
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			smooth.colors = cb;
		}
		else
		{
			smoothOnText.SetActive(false);
			smoothOffText.SetActive(true);
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			smooth.colors = cb;
		}

		if (isGraphicsEnabled)
		{
			visualEffectsOnText.SetActive(true);
			visualEffectsOffText.SetActive(false);
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			visualEffects.colors = cb;
		}
		else
		{
			visualEffectsOnText.SetActive(false);
			visualEffectsOffText.SetActive(true);
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			visualEffects.colors = cb;
		}
	}

	public void ChangeSmooth(Toggle pressed)
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = pressed.colors;

		if (cb.normalColor == disableColor)
		{
			smoothOnText.SetActive(true);
			smoothOffText.SetActive(false);
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			pressed.colors = cb;
			isSmooth = true;
			gameSettings.SetSmoothSetting(true);
		}
		else
		{
			smoothOnText.SetActive(false);
			smoothOffText.SetActive(true);
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			pressed.colors = cb;
			isSmooth = false;
			gameSettings.SetSmoothSetting(false);
		}
	}

	public void ChangeVisualEffects(Toggle pressed)
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = pressed.colors;

		if (cb.normalColor == disableColor)
		{
			visualEffectsOnText.SetActive(true);
			visualEffectsOffText.SetActive(false);
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			pressed.colors = cb;
			gameSettings.SetVisualEffects(true);
			visualEffectsPrefab.SetActive(true);
		}
		else
		{
			visualEffectsOnText.SetActive(false);
			visualEffectsOffText.SetActive(true);
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			pressed.colors = cb;
			gameSettings.SetVisualEffects(false);
			visualEffectsPrefab.SetActive(false);
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

				break;

			case "angular":

				angularFlightCorrection.SetActive(false);
				linearFlightCorrection.SetActive(true);
				whichCorrection = "linear";

				break;
		}

		gameSettings.SetCorrectionType(whichCorrection);
	}

	public void ChangeControllerLeft()
	{
		switch (whichController)
		{
			case "NoJoystick":

				clJoyst.SetActive(true);
				noJoyst.SetActive(false);
				whichController = "ClassicJoystick";

				break;

			case "FloatJoystick":

				noJoyst.SetActive(true);
				flJoyst.SetActive(false);
				whichController = "NoJoystick";

				break;

			case "ClassicJoystick":

				flJoyst.SetActive(true);
				clJoyst.SetActive(false);
				whichController = "FloatJoystick";

				break;
		}

		gameSettings.SetControllerType(whichController);
	}

	public void ChangeControllerRight()
	{
		switch (whichController)
		{
			case "NoJoystick":

				flJoyst.SetActive(true);
				noJoyst.SetActive(false);
				whichController = "FloatJoystick";

				break;

			case "FloatJoystick":

				clJoyst.SetActive(true);
				flJoyst.SetActive(false);
				whichController = "ClassicJoystick";

				break;

			case "ClassicJoystick":

				noJoyst.SetActive(true);
				clJoyst.SetActive(false);
				whichController = "NoJoystick";

				break;
		}

		gameSettings.SetControllerType(whichController);
	}
}
