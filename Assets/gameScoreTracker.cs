using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameScoreTracker : MonoBehaviour
{

    public int player1Score;
    public int player2Score;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(player1Score == 1)
        {

        } else if(player1Score == 2)
        {

        }

        if(player2Score == 1)
        {

        } else if(player2Score == 2)
        {

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
        if (player1Score >= 3 || player2Score >= 3)
        {
            //load the last scene (champ)
            Debug.Log("LOAD OTHER SCENE DEPENDING ON WHO WINS");
        } else
        {
            SceneManager.LoadScene("FightingRing");
        }
    }


}
