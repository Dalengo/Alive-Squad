using UnityEngine;

public class Player_UI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public GameObject respawnMenu;

    void Start()
    {
        PauseMenu.isOn = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            respawnMenu.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            respawnMenu.SetActive(false);
        }
    }
    
    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        PauseMenu.isOn = pauseMenu.activeSelf;
    }
}
  