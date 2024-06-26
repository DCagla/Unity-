using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject ghostPrefab; 
    public float speed = 5.0f; 
    private float boundaryLeft = -7.5f; 
    private float boundaryRight = 7.5f; 

    void Start()
    {
        CreateGhost();
    }

    void Update()
    {
        float movement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + movement, boundaryLeft, boundaryRight),
            transform.position.y
        );
    }

    void CreateGhost()
    {
        float spawnX = Random.Range(boundaryLeft, boundaryRight);
        Vector2 spawnPosition = new Vector2(spawnX, transform.position.y + 5); 
        Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
    }
}
