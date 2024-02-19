using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyJump : MonoBehaviour
{
    public float jumpForce = 5.0f;

    private Rigidbody2D rb;
    private GameManager gameManager;

    [SerializeField]
    private AudioSource flapSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.IsGameActive() && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (rb.velocity.y >= 0f)
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 16.0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -16.0f);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0.0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        flapSound.Play();
    }
}
