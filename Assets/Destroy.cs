using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public Text text;
    public GameObject player;
    public GameObject platformPrefab;
    public GameObject springPrefab;

    private List<Vector2> activePlatforms = new List<Vector2>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            HandlePlatform(collision);
        }
        else if (collision.gameObject.name.StartsWith("Spring"))
        {
            HandleSpring(collision);
        }
    }

    private void HandlePlatform(Collider2D collision)
    {
        if (Random.Range(1, 7) == 1) 
        {
            Destroy(collision.gameObject);
            Vector2 spawnPosition = CalculateSpawnPosition();
            Instantiate(springPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Vector2 spawnPosition = CalculateSpawnPosition();
            collision.gameObject.transform.position = spawnPosition;
        }
    }

    private void HandleSpring(Collider2D collision)
    {
        if (Random.Range(1, 7) > 1) 
        {
            Vector2 spawnPosition = CalculateSpawnPosition();
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            Destroy(collision.gameObject);
        }
        else
        {
            Vector2 spawnPosition = CalculateSpawnPosition();
            collision.gameObject.transform.position = spawnPosition;
        }
    }

    private Vector2 CalculateSpawnPosition()
    {
        Vector2 spawnPosition;
        bool isOverlapping;

        do
        {
            
            float xPosition = Random.Range(-4f, 4f); 
            float yPosition = player.transform.position.y + (14 + Random.Range(0.2f, 1.0f)); 

            spawnPosition = new Vector2(xPosition, yPosition);

            
            isOverlapping = CheckOverlap(spawnPosition);
        } while (isOverlapping); 

        return spawnPosition;
    }

    private bool CheckOverlap(Vector2 position)
    {
        foreach (Vector2 existingPosition in activePlatforms)
        {
            if (Vector2.Distance(existingPosition, position) < 1.5f) 
            {
                return true;
            }
        }
        return false;
    }
}
