using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        //PlayerMovement.instance.enabled = false; 
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void Resume()
    {
        //PlayerMovement.instance.enabled = true; 
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    /*public void LoadMainMenu()
    {
        DontDestroyOnLoadScene.instance.RemoveFromDontDestroyOnLoad();
        Resume();
        SceneManager.LoadScene("MainMenu");
    }*/ 
    // il faut merge avec la version avec le main menu pour que ça marche
}
