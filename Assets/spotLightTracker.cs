using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotLightTracker : MonoBehaviour
{

    public Transform myTarget;
    public Transform offSetThing;
    private Vector3 newPos;
    private float speed;

    private Light myLight;

    private bool changingSize;
    private bool newPosNeeded;
    private bool tracking;

    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        InvokeRepeating("RandomThing", Random.Range(3, 10), Random.Range(7, 12));
    }

    
    void LateUpdate()
    {
        TrackTarget();

    }


    void RandomThing()
    {
        int rand = Random.Range(0, 10);
        if(!changingSize && rand < 5)
        {
            StartCoroutine(changeSizeRandom());
        }
    }

    void TrackTarget()
    {
        //Vector3 newPos = new Vector3(myTarget.position.x + Random.Range(-3, 3), 0, myTarget.position.z + Random.Range(-2, 1));
        if(Mathf.Abs(offSetThing.position.x - myTarget.position.x) > 2.3f || Mathf.Abs(offSetThing.position.z - myTarget.position.z) > 2.3f)
        {
            if (!tracking)
            {
                tracking = true;
                StartCoroutine(trackOffSetThing());
            }
            newPosNeeded = true;
        } else if(!tracking)
        {
            if (newPosNeeded)
            {
                newPosNeeded = false;
                newPos = new Vector3(myTarget.position.x + Random.Range(-2.4f, 2.4f), 0, myTarget.position.z + Random.Range(-2.4f, 2.4f));
                speed = Random.Range(0.0005f, 0.002f);
            }
            if(Mathf.Abs(offSetThing.position.x - newPos.x) > 0.1f && Mathf.Abs(offSetThing.position.z - newPos.z) > 0.1f)
            {
                offSetThing.LookAt(newPos);
                offSetThing.Translate(Vector3.forward * speed);
            } else
            {
                newPosNeeded = true;
            }
        }
        transform.LookAt(offSetThing);
    }

    IEnumerator trackOffSetThing()
    {
        for(int i = 0; i < 4000; i++)
        {
            offSetThing.LookAt(myTarget);
            offSetThing.Translate(Vector3.forward * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        tracking = false;
        
    }

    IEnumerator changeSizeRandom()
    {
        changingSize = true;
        int randSpotAngleTarget = Random.Range(10, 25);
        while(Mathf.Abs(myLight.spotAngle - randSpotAngleTarget) > 0.5f)
        {
            if(myLight.spotAngle > randSpotAngleTarget)
            {
                myLight.spotAngle -= 0.1f;
            } else
            {
                myLight.spotAngle += 0.1f;
            }
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(2.0f);

        //back to normal now
        while (Mathf.Abs(myLight.spotAngle - 15) > 0.5f)
        {
            if (myLight.spotAngle > 15)
            {
                myLight.spotAngle -= 0.1f;
            }
            else
            {
                myLight.spotAngle += 0.1f;
            }
            yield return new WaitForSeconds(0.01f);
        }
        changingSize = false;
    }


}
