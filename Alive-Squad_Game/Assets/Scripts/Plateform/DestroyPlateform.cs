using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlateform : MonoBehaviour
{
    public float TimeToDestoy;
    public SpriteRenderer[] graphics;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            StartCoroutine(Flash());
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(TimeToDestoy);
        Destroy(gameObject);
    }

    IEnumerator Flash()
    {
        while(true)
        {
            foreach(SpriteRenderer s in graphics)
            {
                s.color = new Color(1f,1f,1f,0);
            }
            yield return new WaitForSeconds(0.3f);
            foreach(SpriteRenderer s in graphics)
            {
                s.color = new Color(1f,1f,1f,1f);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
