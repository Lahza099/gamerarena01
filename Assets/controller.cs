using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float moveInput;
    private float speed = 10f;

    private bool isStarted = false;

    private float topScore = 0.0f;

    public Text scoreText;
    public Text startText;

    private int playerLives = 3; 
    public Image[] hearts; 

    private Transform lastPlatform; 

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector3.zero;

        UpdateHeartsUI(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isStarted == false)
        {
            isStarted = true;
            startText.gameObject.SetActive(false);
            rb2d.gravityScale = 5f;
        }

        if (isStarted == true)
        {
            if (moveInput < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (rb2d.velocity.y > 0 && transform.position.y > topScore)
            {
                topScore = transform.position.y;
            }

            scoreText.text = "Score: " + Mathf.Round(topScore).ToString();

            
            if (transform.position.y + 30 < topScore)
            {
                Debug.Log("Player Fell!");
                PlayerFell(); 
            }
        }
    }

    void FixedUpdate()
    {
        if (isStarted == true)
        {
            moveInput = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(moveInput * speed, rb2d.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            lastPlatform = collision.transform; 
            Debug.Log("Last Platform Updated: " + lastPlatform.position);
        }
    }

    void PlayerFell()
    {
        playerLives--; 
        UpdateHeartsUI(); 

        if (playerLives <= 0)
        {
            Debug.Log("Game Over - Going to Menu");
            SceneManager.LoadScene("Menu"); 
        }
        else if (lastPlatform != null)
        {
            
            transform.position = new Vector3(lastPlatform.position.x, lastPlatform.position.y + 1, transform.position.z);
            rb2d.velocity = Vector3.zero; 
        }
        else
        {
            Debug.LogWarning("No Last Platform Found!");
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerLives)
            {
                hearts[i].enabled = true; 
            }
            else
            {
                hearts[i].enabled = false; 
            }
        }
    }
}
