using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{

    [SerializeField]
    Text usernameText;

    private Player player;

    public TombRespawn tomb;

    public void Setup(Player playerb)
    {
        usernameText.text = playerb.username;
        player = playerb;
    }

    public void Respawn()
    {
        player.Respawn();
    }

    public void Used()
    {
        tomb.isUsed = true;
    }
}
