using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public string LevelToLoad;
    public void StartLevelOne()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
