using UnityEngine;
using System.Collections;

public class RangeEnnemyPatrol : MonoBehaviour
{
    public float speed;
    public float TimeBetweenShoot;
    public float shootSpeed;
    private bool canshoot;
    public GameObject bullet;
    public Transform shootPos;
    public Transform[] waypoints;
    public SpriteRenderer graphics;
    private Transform target;
    private int desPoint = 0;
    public int damageOnCollision;

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
        if(canshoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

    IEnumerator Shoot()
    {
        canshoot = false;
        yield return new WaitForSeconds(TimeBetweenShoot);
        GameObject newBullet = Instantiate(bullet, shootPos.position, shootPos.rotation);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-shootSpeed, 0f);
        
        canshoot = true;
    }
}
