using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class RespawnButton : NetworkBehaviour
{

    [SerializeField]
    Text usernameText;

    public Player player;

    public TombRespawn tomb;

    

    public void Setup(Player playerb)
    {
        usernameText.text = playerb.username;
        player = playerb;
        if (!player.isDead)
        {
            this.GetComponent<Button>().interactable = false;
        }
    }


    public void Respawn()
    {
        Debug.Log("Activation bouton");
        player.SetCanRespawn(true, tomb.transform.position);
        
    }

    public void Used()
    {
        tomb.isUsed = true;
    }
}
