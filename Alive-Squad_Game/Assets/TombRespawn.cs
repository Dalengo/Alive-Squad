using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TombRespawn : MonoBehaviour
{

    public bool isInRange;
   
    

    
    void Awake()
    {
       
       
    }

    
    void Update()
    {
        if(isInRange && Input.GetKeyDown(KeyCode.F))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            interactUI.enabled = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInRange = false;
    }
}
