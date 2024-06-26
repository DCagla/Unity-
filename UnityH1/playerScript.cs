using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        MovePlayer(movement);
    }

    private void MovePlayer(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Reward"))
        {
            Reward reward = other.gameObject.GetComponent<Reward>();
            if (reward != null && LevelManager.Instance.CanCollectReward(reward.rewardValue))
            {
                //collect the reward if it's the correct order
                LevelManager.Instance.CollectReward(reward.rewardValue);
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Ghost"))
        {
            //end the game if collided with a ghost
            LevelManager.Instance.EndGame();
        }
    }
}

