using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1And2Manager : MonoBehaviour
{
    //Making this a singleton _____________________________________
    public static Player1And2Manager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    //END Making this a singleton _________________________________


    public enum playerActionType
    {
        GotHit,
        Block,
        RegularAttack,
        SuperAttack,
        MoveBackwards,
        MoveForwards,
        Idle
    }
    public playerActionType player1CurrentAction;
    public playerActionType player1NextAction;

    public playerActionType player2CurrentAction;
    public playerActionType player2NextAction;


    public void controlKeyDown(int whichPlayer, playerActionType player1And2Input)
    {
        //see whether or not this action can be implemented and if so implement it
        //Debug.Log("COMMDAND START : " + player1And2Input.ToString()); //test

        if (whichPlayer == 1)
        {
            //for player1

            //Make sure not in getting hit state
            if (!player1CurrentAction.Equals(playerActionType.GotHit))
            {
                //For Launching Block, Super Attack, Regular Attack, MoveBackward, and MoveForward none of the first three can be active
                if(!player1CurrentAction.Equals(playerActionType.Block) && !player1CurrentAction.Equals(playerActionType.RegularAttack) 
                    && !player1CurrentAction.Equals(playerActionType.SuperAttack))
                {
                    //if the first three from above, then set input action to next action
                    //if(player1And2Input.Equals(playerActionType.Block) || player)



                }
                


            }
        } else
        {
            //for player2


        }


    }

    public void controlKeyUp(int whichPlayer, playerActionType player1And2Input)
    {
        //see what should be done with the particular action and kill it (if it isn't already killed)
        //Debug.Log("COMMDAND STOP : " + player1And2Input.ToString());  //test

        if (whichPlayer == 1)
        {
            //for player1

        }
        else
        {
            //for player2


        }
    }


    //make a function that makes the nextAction into the current action IF it allowed to by priority early, or after the current anim is happening if not
    //maybe this shouldn't be called by update, but whenever a new input is called??? OR NOT..

}
