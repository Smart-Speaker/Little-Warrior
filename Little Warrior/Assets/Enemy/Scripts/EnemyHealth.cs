using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    private CircleCollider2D circleCollider2D;
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rb;

    public AudioSource DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        rb.mass = 20;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UnityEvent OnDied;
    public UnityEvent OnHit;

    public void TakeDamage(int ammount)
    {
        currentHealth -= ammount;
   
        if (currentHealth <= 0)
        {
            boxCollider2D.enabled = false;
            circleCollider2D.enabled = false;

            DeathSound.Play();

            rb.gravityScale = 0;

            OnDied.Invoke();
        }
        else
        {
            OnHit.Invoke();
        }
    }

    public void Die()
    {
        GameObject parentObject = transform.parent.gameObject;
        Destroy(parentObject);
    }
}
