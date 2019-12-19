using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float timeValChange = 4;
    private float v = -1;
    private float h;

    private SpriteRenderer sr;
    public Sprite[] tankSprites;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletEulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeVal >= 3f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Attack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }

    private void Move()
    {
        if (timeValChange >= 4)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 2 && num <= 4)
            {
                h = 1;
                v = 0;
            }
            else
            {
                h = -1;
                v = 0;

            }
            timeValChange = 0;
        }
        else
        {
            timeValChange += Time.fixedDeltaTime;

        }
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (h < 0)
            {
                sr.sprite = tankSprites[3];
                bulletEulerAngles = new Vector3(0, 0, -270);
            }
            else if (h > 0)
            {
                sr.sprite = tankSprites[1];
                bulletEulerAngles = new Vector3(0, 0, -90);
            }
        }
    }

    private void Die()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            timeValChange = 4;
        }
    }
}
