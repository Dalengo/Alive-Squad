using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingWindow;

    public GameObject levelsWindow;
	public string LevelToLoad;

    public void StartGame()
    {
        levelsWindow.SetActive(true);
    }

    public void CloseLevelsMenu()
    {
        levelsWindow.SetActive(false);
    }

	public void SettingButton()
    {
        settingWindow.SetActive(true);
    }

    public void CloseSettingWindow()
    {
        settingWindow.SetActive(false);
    }

	public void QuitGame()
    {
        Application.Quit();
    }
}
