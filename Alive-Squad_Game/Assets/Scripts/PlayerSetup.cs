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

    public bool isactive=false;

    private void Start()
    {
        if (!GameManager.instance.isMenu)
        {
            Debug.Log("Enable");
            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            Player player = GetComponent<Player>();
            GameManager.RegisterPlayer(netId, player);
            Debug.Log("vérification " + player.transform.name);
            GameManager.PlayerActivated(player);
            


            this.Set();
        }
       
       
        
    }

    private void Set()
    {
        Debug.Log("Set");
        if (isLocalPlayer)    
        {
            foreach(GameObject component in componentsToActiveOnlocal)
            {
                component.SetActive(true);
            }
            PlayerMovement.enabled = true;
        }
        else
        {
            Debug.Log("NOPE");
        }
        foreach (GameObject component in componentsToActive)
        {
            component.SetActive(true);
        }

        spriteRenderer.enabled = true;
        Debug.Log("Ok jusque là");
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
        
        Debug.Log("Start");
        Debug.Log(GetComponent<Player>().transform.name);
        base.OnStartClient();

        GetComponent<Player>().transform.name = "Player" + (GetComponent<Player>().GetComponent<NetworkIdentity>().netId.ToString());
        Debug.Log(GameManager.AllPlayers.Count);
        Debug.Log(GameManager.players.Count);
        Debug.Log(GameManager.AllPlayersAlive.Count);
        Debug.Log("---------------------------------------------------");
        if(isLocalPlayer)
        {
            Debug.Log("TRUUUUUUUUUUE");
        }
    }

    private void OnDisable()
    {
        if (!GameManager.instance.isMenu)
        {
            Debug.Log("Disable");
            Debug.Log(GetComponent<Player>().transform.name);
            GameManager.PlayerDesactivated(GetComponent<Player>());
            GameManager.UnregisterPlayer(GetComponent<Player>().transform.name);
            Debug.Log("players count : " + GameManager.players.Count);
            Debug.Log("_AllPlayers count : " + GameManager.AllPlayers.Count);
            Debug.Log("_AllPlayersAlive count : " + GameManager.AllPlayersAlive.Count);
            Debug.Log("----------------------------------------------");
        }
            

    }
}
