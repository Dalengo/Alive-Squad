using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            string username = UserAccountManager.LoggedInUsername;
            CmdSetUsername(transform.name, username);
        }
        GetComponent<Player>().Setup();
    }

    [Command]
    void CmdSetUsername(string playerID,string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + " has joined");
            player.username = username;
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        GameManager.RegisterPlayer(netId,player);
    }

    private void OnDisable()
    {
        GameManager.instance.SetSceneCameraActive(true);
        GameManager.UnregisterPlayer(transform.name);
    }
}
