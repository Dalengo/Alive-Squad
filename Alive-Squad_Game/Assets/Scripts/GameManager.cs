using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerIdPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    private static List<Player> _AllPlayers = GetAllPlayer();

        
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

    private static List<Player> GetAllPlayer()
    {
        List<Player> AllPlayers = new List<Player>(); 
        foreach(KeyValuePair<string, Player> playerGet in players)
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
