using System.Collections;
using UnityEngine;

public class EnnemyBullet : MonoBehaviour
{

    public float dieTime;
    public int damage;
    // Start is called before the first frame update
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        die();
    }
    void Start()
    {
        StartCoroutine(CountDownTimer());
        //transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = col.transform.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(damage);
        }
        die();
    }


    void die()
    {
        Destroy(gameObject);
    }
}
