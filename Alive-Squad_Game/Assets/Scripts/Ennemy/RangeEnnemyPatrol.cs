using UnityEngine;
using System.Collections;

public class RangeEnnemyPatrol : MonoBehaviour
{
    public float speed;
    public float TimeBetweenShoot;
    public float shootSpeed;
    public GameObject bullet;
    public Transform shootPos;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    private int desPoint = 0;
    public int damageOnCollision;
    private Transform target; 
    public float range;
    private float bestdisttoplayer = 999;
    public bool canshoot;
    private int direction;
    private GameObject[] objs;
    private Transform target2;

    void Start()
    {
        target = waypoints[0];
        canshoot = true;
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            if (Vector3.Distance(transform.position, target.position) < 0.3f)
                {
                    desPoint = (desPoint + 1) % waypoints.Length;
                    target = waypoints[desPoint];
                    graphics.flipX = !graphics.flipX;
                }

        objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 0)
        {
            foreach(GameObject ob in objs)
            {
                if (Vector2.Distance(transform.position, ob.transform.position) < bestdisttoplayer)
                {
                    bestdisttoplayer = Vector2.Distance(transform.position, ob.transform.position);
                    target2 = ob.transform;
                }
            }
            if (transform.position.x < target2.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            if (bestdisttoplayer <= range && canshoot)
            {
                StartCoroutine(Shoot());
                bestdisttoplayer = 999;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            //playerHealth.TakeDamage(damageOnCollision);
        }
    }

    IEnumerator Shoot()
    {
        canshoot = false;
        yield return new WaitForSeconds(TimeBetweenShoot);
        GameObject newBullet = Instantiate(bullet, shootPos.position, shootPos.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction, 0f);
        canshoot = true;
    }
}
