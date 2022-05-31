using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private static List<Player> _AllPlayers;

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
        Debug.Log("player+"+_AllPlayers.Count);
        foreach (Player player in _AllPlayers)
        {
            Camera camerab = player.camera;

            if (camerab != null)
            {
                allCams.Add(camerab);
                Debug.Log("Add");
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
        Debug.Log(playerId + " is added");
        Debug.Log(players.Count + " is Count");

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

        allCams = CameraAll();
        sceneCamera.SetActive(false);
        allCams[1].enabled = true;
        Debug.Log(allCams.Count);
    }

    private static List<Player> GetAllPlayer()
    {
        List<Player> AllPlayers = new List<Player>();
        Debug.Log("players1 "+players.Count);
        foreach (KeyValuePair<string, Player> playerGet in players)
        {
            AllPlayers.Add(playerGet.Value);
        }
        return AllPlayers;
    }
    public static List<Player> AllPlayers
    {
        get=> _AllPlayers;
    }
    public void PlayerDesactivated(Player player)
    {
        _AllPlayers.Remove(player);
    }
    public void PlayerActivated(Player player)
    {
        _AllPlayers.Add(player);
    }
}
