using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform Player;
    public GameObject pointA;
    public GameObject pointB;
    public float speed;
    public float agroRange;
    public float AttackRange;

    private bool OutOfBounds = false;

    public PlayerHealth playerHealth;

    private bool PlayerNear;
    private Rigidbody2D rb;
    private Animator animate;
    private Transform currentPoint;

    private bool Stop = false;
    public bool PlayerDead = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        currentPoint = pointB.transform;
        animate.SetBool("IsRunning", true);
    }

    public void StopAll()
    {
        Stop = true;
    }

    void CheckOutOfBounds()
    {
        float patrolPointA = pointA.transform.position.x;
        float patrolPointB = pointB.transform.position.x;

        if (transform.position.x < patrolPointA + 0.5f || transform.position.x > patrolPointB - 0.5f)
        {
            if (!OutOfBounds)
            {
                StopChasingPlayer();
                OutOfBounds = true;
            }
        }

        else if (transform.position.x > patrolPointA + 2.5f && transform.position.x < patrolPointB - 2.5f)
        {
            if (OutOfBounds)
            {
                OutOfBounds = false;
               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, Player.position);

        PlayerDead = playerHealth.isDead;

        CheckOutOfBounds();

        if (!Stop)
        {
            if (!PlayerDead)
            {
                if (playerDistance < agroRange && OutOfBounds != true)
                {
                    PlayerNear = true;

                    if (playerDistance < AttackRange)
                    {
                        StopChasingPlayer();
                        animate.SetBool("IsRunning", false);
                        AttackPlayer();
                    }
                    else
                    {
                        animate.SetBool("Attacking", false);
                        animate.SetBool("IsRunning", true);
                        ChasePlayer();
                    }
                }
                else
                {
                    PlayerNear = false;
                    StopChasingPlayer();
                }
            }
            else if (PlayerDead)
            {
                PlayerNear = false;
            }

            if (PlayerNear == false)
            {
                animate.SetBool("Attacking", false);
                animate.SetBool("IsRunning", true);

                if (currentPoint.position == pointB.transform.position)
                {
                    transform.localScale = new Vector2(4, 4);
                }

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
                {
                    Flip();
                    currentPoint = pointA.transform;
                }

                if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
                {
                    Flip();
                    currentPoint = pointB.transform;
                }

                Vector2 point = currentPoint.position - transform.position;

                if (currentPoint == pointB.transform)
                {
                    rb.velocity = new Vector2(speed, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, 0);
                }
            }
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < Player.position.x)
        {
            rb.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(4, 4);
        }
        else if (transform.position.x > Player.position.x)
        {
            rb.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(-4, 4);
        }
        else if (transform.position.x == Player.position.x)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void StopChasingPlayer()
    {
        rb.velocity = new Vector2(0, 0);
    }

    void AttackPlayer()
    {
        animate.SetBool("Attacking", true);
    }

    private void Flip()
    {
        Vector3 localscale = transform.localScale;

        localscale.x *= -1;
        transform.localScale = localscale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
