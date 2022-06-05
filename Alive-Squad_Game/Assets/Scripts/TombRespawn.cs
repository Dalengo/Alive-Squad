using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TombRespawn : MonoBehaviour
{

    public bool isInRange;
    public bool isUsed=false;
    private Player player;




    void Update()
    {
        if (isUsed)
        {
            Debug.Log("D�sactivation");
            
            player.interactUI.enabled = false;
            player.GetComponentInChildren<Player_UI>().respawnMenu.SetActive(false);
            this.enabled = false;
        }
        if(isInRange && Input.GetKeyDown(KeyCode.F))
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUsed)
        {
            if (collision.CompareTag("Player"))
            {
                isInRange = true;
                player = collision.GetComponent<Player>();
                player.interactUI.enabled = true;
                player.GetComponentInChildren<Player_UI>().respawnMenu.SetActive(true);
                foreach(RespawnButton respawnbutton in player.GetComponentInChildren<Player_UI>().respawnMenu.GetComponentsInChildren<RespawnButton>())
                {
                    respawnbutton.tomb = this;
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            collision.GetComponent<Player>().interactUI.enabled = false;
            collision.GetComponent<Player>().GetComponentInChildren<Player_UI>().respawnMenu.SetActive(false);
        }
        
    }
}
