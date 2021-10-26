using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void shakeTheCam()
    {
        StartCoroutine(Shake(0.15f));
    }


    public IEnumerator Shake (float timeToShake)
    {

        Vector3 originalPos = transform.localPosition;
        Quaternion originalRot = transform.localRotation;

        float timeElaspled = 0f;

        while(timeElaspled < timeToShake)
        {
            float xShake = Random.Range(-0.04f, 0.04f);
            float yShake = Random.Range(-0.04f, 0.04f);
            float xRot = Random.Range(-5f, 5f);
            float yRot = Random.Range(-5f, 5f);

            transform.localPosition = new Vector3(xShake, yShake, originalPos.z);
            //transform.localRotation = new Quaternion()            //fix the posShake first

            timeElaspled += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = originalPos;
        transform.localRotation = originalRot;


    }


}