using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameScoreTracker : MonoBehaviour
{

    public int player1Score;
    public int player2Score;

    public GameObject troph;
    public GameObject wonText;
    public GameObject musAK;

    private AudioSource au;
    public AudioClip endOfRound;

    public bool allDone;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (allDone)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main Menu");
                Destroy(gameObject);
            }
        }
    }

    public void roundEnd()
    {
        //call end round sounds!!!
        au.clip = endOfRound;
        au.Play();
        //make clock big lerp
        if(GameObject.Find("Player1_Display").GetComponent<Player1Scr>().player1HealthBarFillAmount > 
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().player2HealthBarFillAmount)
        {
            player1Score++;
        } else
        {
            player2Score++;
        }
        Debug.Log("Player1 Score = " + player1Score);
        Debug.Log("Player2 Score = " + player2Score);
        StartCoroutine(newRound());
    }

    IEnumerator newRound()
    {
        yield return new WaitForSeconds(4f);
        if (player1Score >= 3 || player2Score >= 3)
        {
            StartCoroutine(playerWon());
        }
        else
        {
            SceneManager.LoadScene("FightingRing");
        }
    }



    IEnumerator playerWon()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Camera Holder").gameObject.SetActive(false);
        Instantiate(troph);
        Instantiate(musAK);
        if(player1Score > player2Score)
        {
            GameObject.Find("Player1_Display").GetComponent<Player1Scr>().WON();
            GameObject.Find("Player1").transform.position = new Vector3(-8, 0, 0);
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().LOST();
            GameObject.Find("Player2").transform.position = new Vector3(8, 0, 0);
        } else
        {
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().WON();
            GameObject.Find("Player1").transform.position = new Vector3(-8, 0, 0);
            GameObject.Find("Player1_Display").GetComponent<Player1Scr>().LOST();
            GameObject.Find("Player2").transform.position = new Vector3(8, 0, 0);
        }
        yield return new WaitForSeconds(4f);
        var i = Instantiate(wonText);
        if (player1Score > player2Score)
        {
            GameObject.Find("Ptext").GetComponent<Text>().text = "Player 1 Won!";
        } else
        {
            GameObject.Find("Ptext").GetComponent<Text>().text = "Player 2 Won!";
        }
        allDone = true;    

    }








}
