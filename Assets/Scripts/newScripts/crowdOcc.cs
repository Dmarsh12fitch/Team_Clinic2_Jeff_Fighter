using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdOcc : MonoBehaviour
{
    private AudioSource au;
    public AudioClip BooSound;
    public AudioClip YaySound;
     
    //float i









    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            doCheer();
        }
    }

    public void randSound()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                doBoo();
                break;
            case 1:
                doCheer();
                break;
            case 2:
                break;
            default:
                break;
        }

        }


    public void doBoo()
    {
        au.PlayOneShot(BooSound);
    }

    public void doCheer()
    {
        au.PlayOneShot(YaySound);
    }



}
