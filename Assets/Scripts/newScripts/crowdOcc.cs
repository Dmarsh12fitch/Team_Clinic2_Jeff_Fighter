using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdOcc : MonoBehaviour
{
    private AudioSource au;
    public AudioClip[] louderCheers;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        StartCoroutine(firstOne());
    }

    IEnumerator firstOne()
    {
        yield return new WaitForSeconds(2);
        au.PlayOneShot(louderCheers[Random.Range(0, louderCheers.Length)]);
    }

    public void randSound()
    {
        au.PlayOneShot(louderCheers[Random.Range(0, louderCheers.Length)]);
    }



}
