using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdPassive : MonoBehaviour
{

    public AudioClip[] passiveCrowdArray;
    private AudioSource au;

    int past = -1;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        au.clip = passiveCrowdArray[Random.Range(0, passiveCrowdArray.Length)];
        au.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!au.isPlaying)
        {
            int rand = Random.Range(0, passiveCrowdArray.Length);
            while (rand != past)
            {
                rand = Random.Range(0, passiveCrowdArray.Length);
            }
            past = rand;
            au.clip = passiveCrowdArray[rand];
            au.Play();
        }
    }

}
