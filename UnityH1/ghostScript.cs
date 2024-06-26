using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostScript : MonoBehaviour
{
    public float chaseSpeed = 1.0f; //speed of the ghost when chasing the player
    public float wanderSpeed = 2.0f; //speed when wandering randomly
    public bool isChasing = false; //determines if the ghost is currently chasing the player
    public Transform playerTransform; //reference to the player's transform for chasing

    private Rigidbody2D rb;
    private Vector2 wanderDirection; //current direction for wandering
    private float changeDirectionTime = 3f; //time interval to change wandering direction
    private float directionTimer; // timer to count down to next dirction change

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        ChangeWanderDirection(); //initialize wandering direction
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer(); //chase the player if set to chase
        }
        else
        {
            WanderRandomly();
        }
    }

    void FixedUpdate()
    {
         if (!isChasing)
        {
            rb.velocity = wanderDirection * wanderSpeed;
        }
    }

    private void ChasePlayer()
    {
        //calculate the direction to the player and set velocity
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * chaseSpeed;
        }
    }

    private void WanderRandomly()
    {
        //change direction at intervls defined by changeDirectionTime
        directionTimer -= Time.deltaTime;
        if (directionTimer <= 0)
        {
            ChangeWanderDirection();
        }
    }

    private void ChangeWanderDirection()
    {
        //choose a new random direction for wandering
        wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        directionTimer = changeDirectionTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            ChangeWanderDirection(); //change direction upon hitting a wall
        }
        else if (other.CompareTag("Player"))
        {
            LevelManager.Instance.EndGame(); //end game if a ghost catches the player
        }
    }

    private void ClampPosition()
    {
        // ensure ghosts stay within designated game area
        float clampedX = Mathf.Clamp(transform.position.x, -10f, 10f);
        float clampedY = Mathf.Clamp(transform.position.y, -4f, 4f);
        transform.position = new Vector2(clampedX, clampedY);
    }
}
