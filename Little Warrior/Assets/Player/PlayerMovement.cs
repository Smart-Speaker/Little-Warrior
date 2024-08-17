using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CoinManager coinManager;
    public CharacterController2D controller;
    public Animator animator;
    public GameObject origin;
    private AudioSource[] sfx;

    public GameObject attackArea = default;
    private PolygonCollider2D polygonCollider;
    public LevelChanger levelChanger;

    public float runSpeed = 40f;
    float horizontalMove = 0f;

    bool jump = false;
    bool crouch = false;
    bool attacking = false;

    private Vector2 spawnPosition;

    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private void Start()
    {
        sfx = origin.GetComponents<AudioSource>();
        spawnPosition = transform.position;
        polygonCollider = attackArea.GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelChanger.FadeToLevel(0);
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayAudioSource(3);
            animator.SetBool("IsJumping", true);
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            animator.SetBool("IsCrouching", true);
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            animator.SetBool("IsCrouching", false);
            crouch = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!attacking)
            {
                attack();
                attacking = true;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("IsAttacking", false);
            attacking = false;
            DisableCollider();
        }


    }

    public void AttackingEnd()
    {
        animator.SetBool("IsAttacking", false);
           DisableCollider();
        attacking = false;
    }

    public void EnableCollider()
    {
        if (polygonCollider != null)
        {
            polygonCollider.enabled = true;
            Debug.Log("PolygonCollider2D enabled.");
        }
    }

    public void DisableCollider()
    {
        if (polygonCollider != null)
        {
            polygonCollider.enabled = false;
            Debug.Log("PolygonCollider2D disabled.");
        }
    }

    private void FixedUpdate()
    {
        // Move Character

        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        PlayAudioSource(1);
        print("Landed");
    }

    public void PlayAudioSource(int index)
    {
        if (index >= 0 && index < sfx.Length)
        {
            sfx[index].Play();
        }
        else
        {
            Debug.LogError("Invalid AudioSource index!");
        }
    }

    private void attack()
    {
        attacking = true;
        animator.SetBool("IsAttacking", true);
        PlayAudioSource(0);
        
        EnableCollider();
    }

    public void OnCrouching(bool isCrouching)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            PlayAudioSource(2);
            coinManager.AddCoins(1);
        }

        if (other.gameObject.CompareTag("KillBox"))
        {
            transform.position = spawnPosition;
        }
    }
}
