using UnityEngine;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    void Start()
    {
        PauseMenu.gameIsPaused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.gameIsPaused = pauseMenu.activeSelf;
    }
}
