using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private int time = 99;

    private Text timer_Text;
    public GameObject p1Win1;
    public GameObject p1Win2;
    public GameObject p2Win1;
    public GameObject p2Win2;
    private gameScoreTracker scoreTracker;

    // Start is called before the first frame update
    void Start()
    {
        timer_Text = GameObject.Find("Timer_Text").GetComponent<Text>();
        scoreTracker = GameObject.Find("GameScoreTracker").GetComponent<gameScoreTracker>();
        InvokeRepeating("UpdateTimerDisplay", 1, 1);

        //p1
        if(scoreTracker.player1Score == 1)
        {
            p1Win1.gameObject.SetActive(true);
            p1Win2.gameObject.SetActive(false);
        } else if(scoreTracker.player2Score >= 2)
        {
            p1Win1.gameObject.SetActive(true);
            p1Win2.gameObject.SetActive(true);
        } else
        {
            p1Win1.gameObject.SetActive(false);
            p1Win2.gameObject.SetActive(false);
        }

        //p2
        if(scoreTracker.player2Score == 1)
        {
            p2Win1.gameObject.SetActive(true);
            p2Win2.gameObject.SetActive(false);
        } else if(scoreTracker.player2Score >= 2)
        {
            p2Win1.gameObject.SetActive(true);
            p2Win2.gameObject.SetActive(true);
        } else
        {
            p2Win1.gameObject.SetActive(false);
            p2Win2.gameObject.SetActive(false);
        }
    }

    void UpdateTimerDisplay()
    {
        if(time - 1 >= 0 && !InputManager.Instance.StopFightingInputs)
        {
            time -= 1;
            timer_Text.text = time.ToString();
        } else
        {
            scoreTracker.roundEnd();
        }
        
    }
}
