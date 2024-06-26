using System.Collections;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject laserPrefab;
    public Transform firePoint;
    public GameObject ghostPrefab;
    public int numberOfGhosts = 7;
    private int ghostsDestroyed = 0;

    /*private void Start()
    {
        StartSpawningGhosts();
    }*/

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);
    }

    private void FireLaser()
    {
        Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
    }

    public void StartSpawningGhosts()
    {
        StartCoroutine(SpawnGhosts());
    }

    private IEnumerator SpawnGhosts()
    {
        for (int i = 0; i < numberOfGhosts; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(5f, 10f), 0);
            Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void GhostDestroyed()
    {
        ghostsDestroyed++;
        if (ghostsDestroyed >= numberOfGhosts)
        {
            Debug.Log("You Win!");
            QuitGame();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
