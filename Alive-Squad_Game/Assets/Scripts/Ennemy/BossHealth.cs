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
    private Vector3 velocity = Vector3.zero;
    private int BumpDirection;

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
            transform.GetComponent<Collider2D>().enabled = false;
            currentHealth -= damage;
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibleDelay());
            transform.GetComponent<Collider2D>().enabled = true;
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        while(isInvincible)
        {
            graphics.color = Color.red;
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
            PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
            playerMovement.rb.AddForce(new Vector2(0,200));
        }
    }
}
