using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public int rewardValue; // the value of this reward, determining its collectability order

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = LevelManager.Instance;
            if (levelManager != null && levelManager.CanCollectReward(rewardValue))
            {
                //cllect the reward if it's the correct one in the sequence
                levelManager.CollectReward(rewardValue);
                Destroy(gameObject); // remove the reward from the game after it's collected
            }
        }
    }
}
