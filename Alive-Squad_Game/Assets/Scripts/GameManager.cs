using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private const string playerIdPrefix = "Player";
    
    public static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static GameManager instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
    }

    public static void RegisterPlayer(string netId, Player player)
    {
        string playerId = playerIdPrefix + netId;
        players.Add(playerId,player);
        player.transform.name = playerId;
    }

    public static void UnregisterPlayer(string playerId)
    {
        players.Remove(playerId);
    }

    public static Player GetPlayer(string playerId)
    {
        try
        {
            return players[playerId];
        }
        catch (KeyNotFoundException)
        {
            Debug.LogError("Objet pas dans le dictionnaire");
            throw new System.Exception("fe");
        }

    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }
}
