using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    GameObject[] componentsToActiveOnlocal;

    [SerializeField]
    GameObject[] componentsToActive;

    [SerializeField]
    Behaviour PlayerMovement;

    [SerializeField]
    SpriteRenderer spriteRenderer;


    private void Set()
    {
        if (isLocalPlayer)    
        {
            foreach(GameObject component in componentsToActiveOnlocal)
            {
                component.SetActive(true);
            }
            PlayerMovement.enabled = true;
        }
        foreach (GameObject component in componentsToActive)
        {
            component.SetActive(true);
        }

        spriteRenderer.enabled = true;
        string username = UserAccountManager.LoggedInUsername;
        if (hasAuthority) CmdSetUsername(transform.name, username);
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
        GameManager.PlayerActivated(player);
        if (!GameManager.instance.isMenu)
        {
            Set();
        }
    }

    private void OnDisable()
    {
        //GameManager.instance.SetSceneCameraActive(true);
        GameManager.PlayerDesactivated(GetComponent<Player>());
        GameManager.UnregisterPlayer(transform.name);
    }
}
