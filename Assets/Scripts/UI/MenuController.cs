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

	/*public GameObject player;
	public GameObject joystick;*/

	public static string whichController = "NoJoystick";

	public static bool isSmooth = false;

	public GameObject SettingsMenu;


    private void Start()
    {
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = noJoyst.colors;

		cb.normalColor = activeColor;
		noJoyst.colors = cb;

		cb.normalColor = disableColor;
		flJoyst.colors = cb;
		clJoyst.colors = cb;

		whichController = "NoJoystick";
		isSmooth = false;

		/*joystick.GetComponent<Image>().enabled = false;
		joystick.transform.GetChild(0).GetComponent<Image>().enabled = false;
		joystick.GetComponent<MouseLooker>().enabled = true;
		joystick.GetComponent<JoystickController>().enabled = false;
		joystick.GetComponent<FloatJoystickController>().enabled = false;*/

		cb.selectedColor = disableColor;
		smooth.colors = cb;
		smooth.isOn = false;
		//player.GetComponent<PlayerController>().isSmooth = false;
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

				/*joystick.GetComponent<Image>().enabled = false;
				joystick.transform.GetChild(0).GetComponent<Image>().enabled = false;
				joystick.GetComponent<MouseLooker>().enabled = true;
				joystick.GetComponent<JoystickController>().enabled = false;
				joystick.GetComponent<FloatJoystickController>().enabled = false;*/

				break;

			case "Float Joystick Button":

				noJoyst.colors = cb;
				clJoyst.colors = cb;

				whichController = "FloatJoystick";

				/*joystick.GetComponent<Image>().enabled = true;
				joystick.transform.GetChild(0).GetComponent<Image>().enabled = true;
				joystick.GetComponent<JoystickController>().enabled = false;
				joystick.GetComponent<FloatJoystickController>().enabled = true;
				joystick.GetComponent<MouseLooker>().enabled = false;*/

				break;

			case "Classic Joystick Button":

				flJoyst.colors = cb;
				noJoyst.colors = cb;

				whichController = "ClassicJoystick";

				/*joystick.GetComponent<Image>().enabled = true;
				joystick.transform.GetChild(0).GetComponent<Image>().enabled = true;
				joystick.GetComponent<JoystickController>().enabled = true;
				joystick.GetComponent<FloatJoystickController>().enabled = false;
				joystick.GetComponent<MouseLooker>().enabled = false;*/

				break;
		}
	}

	public void ChangeSmooth(Toggle pressed)
	{
		Color disableColor = new Color(0.5568628f, 0.5568628f, 0.5568628f, 1f);
		Color activeColor = new Color(1f, 1f, 1f, 1f);

		ColorBlock cb = pressed.colors;

		//PlayerController plController = player.GetComponent<PlayerController>();

		if (pressed.isOn)
        {
			cb.normalColor = activeColor;
			cb.selectedColor = activeColor;
			pressed.colors = cb;
			isSmooth = true;
		}
        else
        {
			cb.normalColor = disableColor;
			cb.selectedColor = disableColor;
			pressed.colors = cb;
			isSmooth = false;
		}
	}

}
