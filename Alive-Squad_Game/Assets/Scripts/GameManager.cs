using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    private const string playerIdPrefix = "Player";

    [SerializeField]
    NetworkManager networkManager;


    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private static List<Player> _AllPlayers=new List<Player>();

    public static List<Player> AllPlayers
    {
        get => _AllPlayers;
    }

    private static List<Player> _AllPlayersAlive = new List<Player>();

    public static List<Player> AllPlayersAlive
    {
        get => _AllPlayersAlive;
    }

    private static List<Camera> allCams;

    public static GameManager instance;

    [SerializeField]
    private GameObject sceneCamera;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }

    

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
        _AllPlayers.Add(player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    {
        return players[playerId];
    }

    

    
    
    public static void PlayerDesactivated(Player player)
    {
        _AllPlayersAlive.Remove(player);
        Debug.Log(player.name + " a été retiré de _AllPlayerAlive");
        if (_AllPlayersAlive.Count == 0)
        {
           
            GOver();
            
            return;
        }
    }
    public static void PlayerActivated(Player player)
    {
        Debug.Log(player.name+" a été ajouté à _AllPlayerAlive");
        if (player == null)
        {
            Debug.Log("Player Null");
        }
        
        
        _AllPlayersAlive.Add(player);

    }

    public static void GOver()
    {
        Debug.Log("Fin de partie");
        NetworkManager.singleton.ServerChangeScene("Login");
    }
}
