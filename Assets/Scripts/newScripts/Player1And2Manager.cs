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
        BlockSTART,
        BlockSTOP,
        RegularAttack,
        SuperAttack,
        MoveBackwards,
        MoveForwards,
        Idle
    }


    //Player1 Variables
    private playerActionType player1CurrentAction = playerActionType.Idle;
    private playerActionType player1NextAction = playerActionType.Idle;

    private float player1HealthBarFillAmount = 0;   //from 0 - 1
    private float player1SuperBarFillAmount = 0;    //from 0 - 1


    //Player2 Variables
    private playerActionType player2CurrentAction = playerActionType.Idle;
    private playerActionType player2NextAction = playerActionType.Idle;

    private float player2HealthBarFillAmount = 0;   //from 0 - 1
    private float player2SuperBarFillAmount = 0;    //from 0 - 1





















    //is called when a control key is pressed
    public void controlKeyDown(int whichPlayer, playerActionType player1And2Input)
    {
        //see whether or not this action can be implemented and if so implement it
        //Debug.Log("COMMDAND START : " + player1And2Input.ToString()); //test

        if (whichPlayer == 1)
        {
            //for player1

            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.GotHit))
            {
                //For Launching Block, Super Attack, Regular Attack, MoveBackward, and MoveForward none of the first three can be active
                if (!player1CurrentAction.Equals(playerActionType.Block) && !player1CurrentAction.Equals(playerActionType.RegularAttack)
                    && !player1CurrentAction.Equals(playerActionType.SuperAttack))
                {
                    //set input action to next action
                    if (player1And2Input.Equals(playerActionType.Block))
                    {
                        //when doing block you have to start blockstart
                        player1NextAction = playerActionType.BlockSTART;
                        tryForceStateChange(1);
                    }
                    else if (player1And2Input.Equals(playerActionType.SuperAttack))
                    {
                        //only makes SuperAttack the next action if super bar is at max
                        if(player1SuperBarFillAmount == 1)
                        {
                            player1NextAction = player1And2Input;
                            tryForceStateChange(1);
                        }
                    } else
                    {
                        player1NextAction = player1And2Input;
                        tryForceStateChange(1);
                    }
                    
                    

                    /*
                    //IF Block, RegularAttack, or SuperAttack, launch right now
                    if (player1And2Input.Equals(playerActionType.Block) || player1And2Input.Equals(playerActionType.RegularAttack)
                        || (player1And2Input.Equals(playerActionType.SuperAttack) && player1SuperBarFillAmount == 1))
                    {
                        //call the function that makes next action current action, switch animation, and makes next Idle
                    }
                    */
                }
            }
        } else
        {
            //for player2



        }


    }













    //is called when a control key is released
    public void controlKeyUp(int whichPlayer, playerActionType player1And2Input)
    {
        //see what should be done with the particular action and kill it (if it isn't already killed)
        //Debug.Log("COMMDAND STOP : " + player1And2Input.ToString());  //test

        if (whichPlayer == 1)
        {
            //for player1

            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.GotHit))
            {
                if (player1And2Input.Equals(playerActionType.Block))
                {
                    //when stopping block you need to do blockstop
                    player1NextAction = playerActionType.BlockSTOP;
                }
                else
                {
                    player1NextAction = playerActionType.Idle;
                }

                tryForceStateChange(1);
            }
        }
        else
        {
            //for player2


        }

    }


    //is called when it is requested to force the state to change right now
    void tryForceStateChange(float whichPlayer)
    {


        if(whichPlayer == 1)
        {
            //for player1

            //if got hit is next action, stop the current action right away and start gothit
            if (player1NextAction.Equals(playerActionType.GotHit))
            {
                //change current to next state, next state to idle
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;
                p1GotHit();




                //if the current state can be replaced (and it isn't GotHit)
            }
            else if ((!player1CurrentAction.Equals(playerActionType.GotHit))
                && !(player1CurrentAction.Equals(playerActionType.RegularAttack) || player1CurrentAction.Equals(playerActionType.SuperAttack)
                || player1CurrentAction.Equals(playerActionType.Block) || player1CurrentAction.Equals(playerActionType.BlockSTART)
                || player1CurrentAction.Equals(playerActionType.BlockSTOP)))
            {
                //change current to next state, next state to idle
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;

                //say "forced ___ state in"
                Debug.Log("FORCED to : " + player1CurrentAction.ToString());

                //call appropriate state for current!
                if (player1CurrentAction.Equals(playerActionType.MoveBackwards))
                {
                    p1MoveBackwards();
                } else if (player1CurrentAction.Equals(playerActionType.MoveForwards))
                {
                    p1MoveForwards();
                } else if (player1CurrentAction.Equals(playerActionType.BlockSTART))
                {
                    p1BlockSTART();
                } else if (player1CurrentAction.Equals(playerActionType.BlockSTOP))
                {
                    p1BlockSTOP();
                } else if (player1CurrentAction.Equals(playerActionType.RegularAttack))
                {
                    p1RegularAttack();
                } else if (player1CurrentAction.Equals(playerActionType.SuperAttack))
                {
                    p1SuperAttack();
                }
            }
        }
        else
        {
            //for player2




        }



    }



    //this is called once the current animation ends and calls the next animation
    void defaultStateChange(float whichPlayer)
    {
        

    }






    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-



        
    void p1MoveForwards()
    {
        new WaitForSeconds(2.0f);

    }

    void p1MoveBackwards()
    {

    }

    void p1BlockSTART()
    {

    }

    void p1BlockSTOP()
    {

    }

    void p1RegularAttack()
    {

    }

    void p1SuperAttack()
    {

    }

    void p1GotHit()
    {

    }

    //Player2 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

}
