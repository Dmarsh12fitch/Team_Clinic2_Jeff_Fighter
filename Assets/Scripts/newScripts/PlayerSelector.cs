using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public Transform PlayerDisplay;
    private AudioSource au;

    public AudioClip[] selectedSound;
    public float RotToStopOn;
    private float selectionWaitToRotate = 0.5f;
    public bool lockedIn;
    public int player;

    public GameObject PlayerREADY;


    // Start is called before the first frame update
    void Start()
    {
        PlayerREADY.gameObject.SetActive(false);
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
        
        if(selectionWaitToRotate > 0)
        {
            selectionWaitToRotate -= Time.deltaTime;
        } else
        {
            if (!lockedIn)
            {
                rotateMe();
            } else
            {
                if(Mathf.Abs(PlayerDisplay.rotation.eulerAngles.y - RotToStopOn) < 0.5f)
                {
                    if(PlayerDisplay.rotation.eulerAngles.y != RotToStopOn) { PlayerDisplay.rotation = Quaternion.Euler(0, RotToStopOn, 0); }
                } else
                {
                    rotateMe();
                }
            }
        }
    }

    void getInputs()
    {
        if(player == 1 && Input.GetKeyDown(KeyCode.Space))
        {
            switchLockedIn();
        }
        else if(player == 2 && (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.RightControl)))
        {
            switchLockedIn();
        }
    }

    void switchLockedIn()
    {
        bool setTo = !lockedIn;
        if (Mathf.Abs(PlayerDisplay.rotation.eulerAngles.y - RotToStopOn) < 0.25f) { selectionWaitToRotate = 2f; }
        lockedIn = setTo;
        if (lockedIn)
        {
            au.clip = selectedSound[0];
            au.Play();
        }
        PlayerREADY.gameObject.SetActive(setTo);
    }


    void cycleSelection()
    {
        selectionWaitToRotate = 2f;
        //more obviously
    }

    void rotateMe()
    {
        PlayerDisplay.Rotate(0, 0.03f, 0);
    }

}
