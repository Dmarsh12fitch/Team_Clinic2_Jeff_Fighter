using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class countDown : MonoBehaviour
{
    private Text myText;
    private PlayerSelector P1;
    private PlayerSelector P2;
    private GameObject StartingInText;

    private bool CountDownStarted;
    private int numbercurrent = 5;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        P1 = GameObject.Find("Player1 Selector").GetComponent<PlayerSelector>();
        P2 = GameObject.Find("Player2 Selector").GetComponent<PlayerSelector>();
        StartingInText = GameObject.Find("StartingIn");
        StartingInText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(P1.lockedIn && P2.lockedIn)
        {
            if(timer > 1 && numbercurrent <= 0)
            {
                SceneManager.LoadScene("FightingRing");
            } else if(timer > 1)
            {
                timer = 0;
                CountDownStarted = true;
                StartingInText.gameObject.SetActive(true);
                myText.text = numbercurrent.ToString();
                numbercurrent--;
            }
        } else
        {
            numbercurrent = 5;
            CountDownStarted = false;
            StartingInText.gameObject.SetActive(false);
            myText.text = "VS";
        }
    }
}
