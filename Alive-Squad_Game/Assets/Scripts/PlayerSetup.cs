using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;
    Camera sceneCamera;
    private void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
        }
        else
        {
            sceneCamera = Camera.main;
            sceneCamera.gameObject.SetActive(false);

            CmdSetUsername(transform.name, UserAccountManager.LoggedInUserName);
        }
    }


    [Command]
    void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + "has joined !");
            player.username = username;
        }
        
    }


    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();


        GameManager.RegisterPlayer(netId, player);
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        sceneCamera.gameObject.SetActive(true);
        GameManager.UnregisterPlayer(transform.name);
    }
}
