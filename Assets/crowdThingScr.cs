using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdThingScr : MonoBehaviour
{
    private AudioSource au;
    public AudioClip booClip;
    public AudioClip yayClip;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (au.time > au.clip.length - 1) //3.5f
        {
            au.time = 0.25f;
            int rand = Random.Range(0, 3);
            if(rand == 0)
            {
                au.clip = booClip;
            } else
            {
                au.clip = yayClip;
            }
            au.Play();
        }
    }
}
