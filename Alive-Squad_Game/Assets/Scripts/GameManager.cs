using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;



public class GameManager : NetworkBehaviour
{
    private const string playerIdPrefix = "Player";


    public bool isMenu=false;

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
       
        NetworkManager.singleton.ServerChangeScene("MainMenuScene");
        
    }

    public void Win()
    {
        foreach (Player player in AllPlayers)
        {
            player.GetComponentInChildren<Player_UI>().Win_Panel.SetActive(true);
        }
        Wait();
       
    }

    public void Wait()
    {
        StartCoroutine(ExampleCoroutine());
    }


    public static IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        NetworkManager.singleton.ServerChangeScene("MainMenuScene");

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
