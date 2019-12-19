using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;

    public Sprite broken;
    public GameObject explosionPrefab;
    public AudioClip die;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        sr.sprite = broken;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.isDefeated = true;
        AudioSource.PlayClipAtPoint(die, transform.position);
    }
}
