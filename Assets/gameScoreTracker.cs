using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameScoreTracker : MonoBehaviour
{

    public int player1Score;
    public int player2Score;

    public GameObject troph;

    public bool allDone;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            roundEnd();
        }
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
        //make clock big lerp
        if(GameObject.Find("Player1_Display").GetComponent<Player1Scr>().player1HealthBarFillAmount > 
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().player2HealthBarFillAmount)
        {
            player1Score++;
        } else
        {
            player2Score++;
        }
        Debug.Log("p1 score = " + player1Score);
        Debug.Log("p2 score = " + player2Score);
        if (player1Score >= 3 || player2Score >= 3 || true) //not true later
        {
            StartCoroutine(playerWon());
        } else
        {
            SceneManager.LoadScene("FightingRing");
        }
    }



    IEnumerator playerWon()
    {
        yield return new WaitForSeconds(4f);
        GameObject.Find("Camera Holder").gameObject.SetActive(false);
        Instantiate(troph);
        
        if(player1Score > player2Score)
        {
            GameObject.Find("Player1_Display").GetComponent<Player1Scr>().WON();
            GameObject.Find("Player2").transform.position = new Vector3(8, 0, 0);
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().LOST();
            GameObject.Find("Player2").transform.position = new Vector3(8, 0, 0);
        } else
        {
            GameObject.Find("Player2_Display").GetComponent<Player2Scr>().WON();
            GameObject.Find("Player1").transform.position = new Vector3(-8, 0, 0);
            GameObject.Find("Player1_Display").GetComponent<Player1Scr>().LOST();
            GameObject.Find("Player2").transform.position = new Vector3(8, 0, 0);
        }
        yield return new WaitForSeconds(5f);

        //GameObject.Find("Trophy").GetComponent<Rigidbody>().useGravity = true;
        

    }








}
