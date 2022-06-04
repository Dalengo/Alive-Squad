using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TombRespawn : MonoBehaviour
{

    public bool isInRange;
    public bool isUsed=false;




    void Update()
    {
        if (isUsed)
        {
            
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
                collision.GetComponent<Player>().interactUI.enabled = true;
                collision.GetComponent<Player>().GetComponentInChildren<Player_UI>().respawnMenu.SetActive(true);
                collision.GetComponent<Player>().GetComponentInChildren<Player_UI>().respawnMenu.GetComponentInChildren<RespawnButton>().tomb=GetComponent<TombRespawn>();
                
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
