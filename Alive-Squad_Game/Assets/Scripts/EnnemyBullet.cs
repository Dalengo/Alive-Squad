using System.Collections;
using UnityEngine;

public class EnnemyBullet : MonoBehaviour
{

    public float dieTime, damage;
     public int speed;
     public GameObject target;
    // Start is called before the first frame update
    IEnumerator CountDownTimer()
    {
        yield return new WaitForSeconds(dieTime);
        die();
    }
    void Start()
    {
        StartCoroutine(CountDownTimer());
        transform.position = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        die();
    }


    void die()
    {
        Destroy(gameObject);
    }
}
