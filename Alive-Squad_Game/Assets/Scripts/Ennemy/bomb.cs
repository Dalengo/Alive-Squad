using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public Animator explosion;
    public float RangeToDetect;
    public float RangeToDamage;
    public int damage;
    public float ExplosionFlashDelay = 0.15f;
    public float ExplosionDelay = 3f;
    public SpriteRenderer graphics;
    private float bestdisttoplayer = 999;
    private GameObject[] objs;
    private Transform target2;
    private bool clignote;
    private int die = 0;
    public AudioClip playlist;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 0)
        {
            bestdisttoplayer = 999;
            foreach(GameObject ob in objs)
            {
                if (Vector2.Distance(transform.position, ob.transform.position) < bestdisttoplayer)
                {
                    bestdisttoplayer = Vector2.Distance(transform.position, ob.transform.position);
                    target2 = ob.transform;
                }
            }
        }
        if (bestdisttoplayer < RangeToDetect)
        {
            die += 1;
            if (die == 1)
            {
                Die();
            }
        }
    }

    void Die()
    {
        clignote = true;
        StartCoroutine(InvincibilityFlash());
        StartCoroutine(HandleInvincibleDelay());
        audioSource.clip = playlist;
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(ExplosionDelay/3);
        audioSource.Play();
        yield return new WaitForSeconds(ExplosionDelay/3*2);
        if (bestdisttoplayer <= RangeToDamage)
        {
            Player player = target2.GetComponent<Player>();
            player.RPcTakeDamage(damage);
        }
        yield return new WaitForSeconds(0.40f);
        if (bestdisttoplayer <= RangeToDamage)
        {
            Player player = target2.GetComponent<Player>();
            player.RPcTakeDamage(damage);
        }
        yield return new WaitForSeconds(0.40f);
        if (bestdisttoplayer <= RangeToDamage)
        {
            Player player = target2.GetComponent<Player>();
            player.RPcTakeDamage(damage);
        }
        Destroy(gameObject);
    }
    IEnumerator InvincibilityFlash()
    {
        while(clignote)
        {
            graphics.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(ExplosionFlashDelay);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(ExplosionFlashDelay);
        }
    }

    public IEnumerator HandleInvincibleDelay()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        transform.localScale = new Vector3(RangeToDamage,RangeToDamage,RangeToDamage);
        explosion.SetBool("Explosion", true);
        clignote = false;
    }
}
