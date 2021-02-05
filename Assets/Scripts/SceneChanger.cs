using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void Lol()
	{
		Debug.Log("Lol Kek");
	}
	public void Exit()
	{
		Application.Quit();
	}
}