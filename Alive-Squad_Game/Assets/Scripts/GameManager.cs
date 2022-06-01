using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private static List<Player> _AllPlayers=new List<Player>();

    public static List<Player> AllPlayers
    {
        get => _AllPlayers;
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

    private static List<Camera> CameraAll()
    {
        List<Camera> allCams= new List<Camera>();
        _AllPlayers=GetAllPlayer();
        foreach (Player player in _AllPlayers)
        {
            Camera camerab = player.camera;

            if (camerab != null)
            {
                allCams.Add(camerab);
            }
            else
            {
                Debug.Log("Not Add");
            }
            
        }
        return allCams;
    }

    public static void RegisterPlayer(string netID, Player player)
    {
        string playerId = playerIdPrefix + netID;
        players.Add(playerId, player);
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

    public void SetSceneCameraActive(bool isActive)
    {
        if (sceneCamera == null)
        {
            return;
        }
        sceneCamera.SetActive(isActive);

        //allCams = CameraAll();
        //sceneCamera.SetActive(false);
        //allCams[1].enabled = true;
    }

    private static List<Player> GetAllPlayer()
    {
        List<Player> AllPlayers = new List<Player>();
        foreach (KeyValuePair<string, Player> playerGet in players)
        {
            AllPlayers.Add(playerGet.Value);
        }
        return AllPlayers;
    }
    
    public static void PlayerDesactivated(Player player)
    {
        _AllPlayers.Remove(player);
        if (_AllPlayers.Count == 0)
        {
            GOver();
            return;
        }
    }
    public static void PlayerActivated(Player player)
    {
        Debug.Log(player.name+" a été ajouté à _AllPlayer");
        if (player == null)
        {
            Debug.Log("Player Null");
        }
        
        _AllPlayers.Add(player);
        
    }

    public static void GOver()
    {
        Debug.Log("Fin de partie");
    }
}
