using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int currentHealth;

    public bool isInvincible = false;
    public float InvincibilityFlashDelay = 0.15f;
    public float InvincibilityTimeAfterHit = 3f;

    public SpriteRenderer graphics;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(transform.parent.parent.gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        if(!isInvincible)
        {
            currentHealth -= damage;
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibleDelay());
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while(isInvincible)
        {
            graphics.color = new Color(1f,1f,1f,0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }
    public IEnumerator HandleInvincibleDelay()
    {
        yield return new WaitForSeconds(InvincibilityTimeAfterHit);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TakeDamage(1);
            Player player = collision.transform.GetComponent<Player>(); //need to edit or add isinvicible
            if (currentHealth>0)
            {
                StartCoroutine(HandleInvincibleDelay2(player));
            }
        }
    }

    public IEnumerator HandleInvincibleDelay2(Player player)
    {
        player.isInvincible = true;
        yield return new WaitForSeconds(1);
        player.isInvincible = false;
    }
}
