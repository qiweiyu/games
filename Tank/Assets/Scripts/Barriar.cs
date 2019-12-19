using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriar : MonoBehaviour
{
    public AudioClip hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayHit()
    {
        AudioSource.PlayClipAtPoint(hit, transform.position);
    }
}
