using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; //singleton instance for global access
    public GameObject ghostPrefab;
    public GameObject[] rewardPrefabs; 
    public Transform playerTransform; 
    public Color[] ghostColors = new Color[] { Color.red, Color.yellow, Color.blue };
    public GameObject gameOverSprite; 
    public int currentLevel = 1;
    public int totalLevels = 5;
    private int rewardsToCollect;
    private int totalRewardsCollected; 
    private int smallestRewardValue = 1; //tracks the smallest uncollected reward value

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        ClearScene();
        rewardsToCollect = level;
        smallestRewardValue = 1; //reset smallest reward value at the start of each level
        for (int i = 0; i < level; i++)
        {
            SpawnGhost(i, level);
            SpawnReward(i);
        }
    }

    void ClearScene()
    {
        //simplified clearing method
        foreach (var obj in FindObjectsOfType<GameObject>())
        {
            if (obj.CompareTag("Ghost") || obj.CompareTag("Reward"))
                Destroy(obj);
        }
    }

    void SpawnGhost(int index, int level)
    {
        Vector2 spawnPosition = GetRandomPosition(playerTransform.position, 3f);
        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghost.GetComponent<SpriteRenderer>().color = ghostColors[Random.Range(0, ghostColors.Length)];
        ghost.GetComponent<ghostScript>().isChasing = (index == 0); //first ghost chases the player
    }

    void SpawnReward(int index)
    {
        Vector2 spawnPosition = GetRandomPosition();
        GameObject reward = Instantiate(rewardPrefabs[Random.Range(0, rewardPrefabs.Length)], spawnPosition, Quaternion.identity);
        reward.GetComponent<Reward>().rewardValue = index + 1; //incremental reward values for order
    }

    Vector2 GetRandomPosition(Vector3 basePosition = default(Vector3), float minDistance = 0)
    {
        Vector2 spawnPosition;
        do
        {
            spawnPosition = new Vector2(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
        }
        while (minDistance != 0 && Vector2.Distance(spawnPosition, basePosition) < minDistance);
        return spawnPosition;
    }

    public bool CanCollectReward(int rewardValue)
    {
        return rewardValue == smallestRewardValue;
    }

    public void CollectReward(int rewardValue)
    {
        smallestRewardValue++;
        totalRewardsCollected++;
        if (totalRewardsCollected >= rewardsToCollect)
            AdvanceToNextLevel();
    }

    void AdvanceToNextLevel()
    {
        if (++currentLevel <= totalLevels)
        {
            StartLevel(currentLevel);
        }
        else
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        ClearScene();
        Instantiate(gameOverSprite, new Vector2(0, 0), Quaternion.identity);
        Instantiate(rewardPrefabs[currentLevel-1], new Vector2(0, -2), Quaternion.identity);
        Debug.Log("All levels completed!Game over!");
    }
}

