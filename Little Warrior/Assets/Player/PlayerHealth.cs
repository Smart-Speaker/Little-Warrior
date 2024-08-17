using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 4; // Maximum health
    public int NumberOfHearts = 4;
    public int currentHealth;
    private Animator animator;
    private SpriteRenderer sr;
    public bool isDead = false;

    public Image[] hearts;
    public Sprite FullHeart;
    public Sprite emptyHeart;

    public LevelChanger levelchanger;
    public Sprite FinalFrame;

    void Start()
    {
        currentHealth = maxHealth; // Initialize health
        animator = GetComponent<Animator>(); // Get the Animator component
        sr = GetComponent<SpriteRenderer>(); // Get the Animator component
    }

    private void Update()
    {
        if (currentHealth > NumberOfHearts)
        {
            currentHealth = NumberOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = FullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < NumberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // Function to apply damage to the player
    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent taking damage if already dead

        currentHealth -= amount;
        animator.SetTrigger("IsHurt"); // Trigger damage animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function called when the player dies
    void Die()
    {
        if (isDead) return; // Prevent double death

        isDead = true; // Mark the player as dead
        animator.SetTrigger("IsDead");

       
    }

    public void OnDeathAnimationComplete()
    {
        animator.speed = 0;
        sr.sprite = FinalFrame;
        levelchanger.FadeToLevel(2);
    }
}
