using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float defendTimeVal = 3;
    private bool isDefended = true;

    private SpriteRenderer sr;
    public Sprite[] tankSprites;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;
    public AudioClip drive;
    public AudioClip idle;
    public AudioSource moveAudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        moveAudio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeated)
        {
            return;
        }
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                defendEffectPrefab.SetActive(false);
                isDefended = false;
            }
        }
        if (timeVal >= 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
        Move();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }

    private void Move()
    {
        bool isMove = false;
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprites[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
            isMove = true;
        }
        else if (v > 0)
        {
            sr.sprite = tankSprites[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
            isMove = true;
        }
        else
        {
            float h = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
            if (h < 0)
            {
                sr.sprite = tankSprites[3];
                bulletEulerAngles = new Vector3(0, 0, -270);
                isMove = true;
            }
            else if (h > 0)
            {
                sr.sprite = tankSprites[1];
                bulletEulerAngles = new Vector3(0, 0, -90);
                isMove = true;
            }
        }
        if (isMove)
        {
            moveAudio.clip = drive;
        }
        else
        {
            moveAudio.clip = idle;
        }
        if (!moveAudio.isPlaying)
        {
            moveAudio.Play();
        }
    }

    private void Die()
    {
        if (isDefended)
        {
            return;
        }
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        PlayerManager.Instance.isDead = true;
    }
}
