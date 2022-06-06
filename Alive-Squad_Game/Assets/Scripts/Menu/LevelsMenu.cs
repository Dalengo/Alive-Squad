using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class LevelsMenu : NetworkBehaviour
{
    public void StartLevel(string LevelToLoad)
    {
        NetworkManager.singleton.ServerChangeScene("Lobby");
    }
}
