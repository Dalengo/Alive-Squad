using UnityEngine;

public class kamikazeMoove : MonoBehaviour
{
    public float speed;
    private Transform target;
    public int damageOnCollision;
    public int range;
    private GameObject[] objs;
    private float bestdisttoplayer = 999;
    private bool move;

    void Start()
    {
        
    }

    void Update()
    {
    
        objs = GameObject.FindGameObjectsWithTag("Player");
        if (objs.Length > 0)
        {
            foreach(GameObject ob in objs)
            {
                bestdisttoplayer = Vector2.Distance(transform.position, objs[0].transform.position);
                target = objs[0].transform;
                if (Vector2.Distance(transform.position, ob.transform.position) < bestdisttoplayer)
                {
                    bestdisttoplayer = Vector2.Distance(transform.position, ob.transform.position);
                    target = ob.transform;
                }
            }
            if (bestdisttoplayer <= range)
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
                move = true;
            }
            if (move == true)
            {
                Vector3 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
            }
            if (bestdisttoplayer > range*3)
            {
                move = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                Player player = collision.transform.GetComponent<Player>();
                player.RPcTakeDamage(damageOnCollision);
                Destroy(transform.gameObject);
            }
        }
}
