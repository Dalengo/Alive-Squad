using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserAccountManager : MonoBehaviour
{
    public static UserAccountManager instance;
    public static string LoggedInUserName;
    public string lobbySceneName = "Lobby";

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    public void LogIn(Text username)
    {
        LoggedInUserName = username.text;
        Debug.Log("Logged in as : " + LoggedInUserName);
        SceneManager.LoadScene(lobbySceneName);
    }
}  
