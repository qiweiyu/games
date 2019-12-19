using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0f, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        gameManager.AddScore();
        Destroy(gameObject);
    }
}
