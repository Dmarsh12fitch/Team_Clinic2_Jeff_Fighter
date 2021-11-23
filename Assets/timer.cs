using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    private int time = 12;

    private Text timer_Text;

    // Start is called before the first frame update
    void Start()
    {
        timer_Text = GameObject.Find("Timer_Text").GetComponent<Text>();
        InvokeRepeating("UpdateTimerDisplay", 1, 1);
    }

    void UpdateTimerDisplay()
    {
        if(time - 1 >= 0)
        {
            time -= 1;
            timer_Text.text = time.ToString();
        } else
        {
            //end the game
        }
        
    }
}
