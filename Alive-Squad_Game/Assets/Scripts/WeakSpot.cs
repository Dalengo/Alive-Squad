using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(transform.parent.gameObject);

        if(collision.CompareTag("Player"))
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
    
}