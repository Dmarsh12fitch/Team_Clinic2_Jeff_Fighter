using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Player1Scr Player1Script;
    private Player2Scr Player2Script;


    // Start is called before the first frame update
    void Start()
    {
        Player1Script = GameObject.Find("Player1_Display").GetComponent<Player1Scr>();
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shake (float damage)
    {
        float intensity;
        float timeToShake;
        if(Player1Script.regularDamage == damage)
        {
            intensity = 0.08f;
            timeToShake = 0.15f;
        } else
        {
            intensity = 0.5f;
            timeToShake = 0.4f;
        }

        Vector3 originalPos = transform.localPosition;
        Quaternion originalRot = transform.localRotation;

        float timeElaspled = 0f;

        while(timeElaspled < timeToShake)
        {
            float xShake = Random.Range(-intensity, intensity);
            float yShake = Random.Range(-intensity, intensity);
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
