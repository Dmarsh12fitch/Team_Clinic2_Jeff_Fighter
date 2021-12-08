using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotLightTracker : MonoBehaviour
{

    public Transform myTarget;
    public Transform offSetThing;
    private Vector3 newPos;
    private float speed = 10f;

    private Light myLight;

    private float timerTracker = 3f;

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
        if((Mathf.Abs(offSetThing.position.x - myTarget.position.x) > 2.8f || Mathf.Abs(offSetThing.position.z - myTarget.position.z) > 2.8f) || tracking)
        {
            tracking = true;
            speed = 10f;
            if((Mathf.Abs(offSetThing.position.x - myTarget.position.x) > 0.1f || Mathf.Abs(offSetThing.position.z - myTarget.position.z) > 0.1f))
            {
                offSetThing.LookAt(myTarget);
                offSetThing.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            timerTracker -= Time.deltaTime;
            if(timerTracker <= 0)
            {
                tracking = false;
                timerTracker = 3;
                newPosNeeded = true;
            }
        } else if(!tracking)
        {
            if (newPosNeeded)
            {
                newPosNeeded = false;
                newPos = new Vector3(myTarget.position.x + Random.Range(-3f, 3f), 0, myTarget.position.z + Random.Range(-3f, 3f));
                //speed = Random.Range(0.01f, 0.03f);
                speed = 1f;
            }
            if(Mathf.Abs(offSetThing.position.x - newPos.x) > 0.1f && Mathf.Abs(offSetThing.position.z - newPos.z) > 0.1f)
            {
                offSetThing.LookAt(newPos);
                offSetThing.Translate(Vector3.forward * speed * Time.deltaTime);
            } else
            {
                newPosNeeded = true;
            }
        }
        transform.LookAt(offSetThing);
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
