using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BoutonLevel : NetworkBehaviour
{
    [SerializeField]
    string levelToload;

    public void StartLevel()
    {
        NetworkManager.singleton.ServerChangeScene(levelToload);
    }
}
