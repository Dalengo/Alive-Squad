using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretRocket : MonoBehaviour
{
    public float range;
    private float bestdisttoplayer = 999;
    private GameObject[] objs;
    private Transform target;
    private bool Detected = false;
    private Vector2 Direction;
    public SpriteRenderer sprite;
    public GameObject Gun;
    public GameObject Bullet;
    public float FireRate;
    private float nexTimetoShoot = 0;
    public Transform ShootPos;
    public float ShootSpeed;
    
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
            bestdisttoplayer = Vector2.Distance(transform.position, objs[0].transform.position);
            target = objs[0].transform;
            foreach(GameObject ob in objs)
            {
                if (Vector2.Distance(transform.position, ob.transform.position) < bestdisttoplayer)
                {
                    bestdisttoplayer = Vector2.Distance(transform.position, ob.transform.position);
                    target = ob.transform;
                }
            }
            Direction = (Vector2)target.position - (Vector2)transform.position;
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, range);
            if(rayInfo)
            {
                if(rayInfo.collider.gameObject.tag == "Player" && bestdisttoplayer < range)
                {
                    if(Detected == false)
                    {
                        Detected = true;
                        sprite.color = Color.red;
                    }
                }
                else
                {
                    if(Detected == true)
                    {
                        Detected = false;
                        sprite.color = Color.green;
                    }
                }
            }
            if(Detected)
            {
                Gun.transform.up = Direction;
                Gun.transform.Rotate (0,0,-90);
                if (transform.position.x - target.position.x < 0)
                {
                    Gun.transform.Rotate (-180,0,0);
                }
                if(Time.time > nexTimetoShoot)
                {
                    nexTimetoShoot = Time.time+1/FireRate;
                    shoot();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void shoot()
    {
        GameObject Bulletins = Instantiate(Bullet, ShootPos.position, Quaternion.identity);
        Bulletins.transform.up = Direction;
        Bulletins.transform.Rotate (0,0,-90);
        Bulletins.GetComponent<Rigidbody2D>().AddForce(Direction * ShootSpeed);
    }
}
