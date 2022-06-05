using UnityEngine;
using Mirror;

public class WeakSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (transform.parent.parent.gameObject != null)
            {
                //NetworkServer.Destroy(transform.parent.parent.gameObject);
                Destroy(transform.parent.parent.gameObject);
            }
            
        }
    }
}