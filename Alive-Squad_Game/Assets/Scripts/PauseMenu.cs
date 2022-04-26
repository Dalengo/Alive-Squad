using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    
    public static bool gameIsPaused = false;

    /*void Pause()
    {
        //PlayerMovement.instance.enabled = false; 
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }*/

    /*public void LoadMainMenu()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }*/ 
    // il faut merge avec la version avec le main menu pour que ça marche
}
