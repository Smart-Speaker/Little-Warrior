using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force = 5f;
    public int damage = 1;
    public AudioSource impactsound;

    private float timer;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Rotate the projectile to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Set the velocity of the projectile
        rb.velocity = direction * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 4)
        {
            timer = 0;
            animator.SetTrigger("Explode");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                damage = 0;
            }

            rb.velocity = new Vector2(0, 0);
            animator.SetTrigger("Explode");
        }

        if (other.gameObject.CompareTag("Enviroment"))
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetTrigger("Explode");
        }
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
