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
    private Player1Scr Player1Script;

    private playerActionType player1CurrentAction = playerActionType.Idle;
    private playerActionType player1NextAction = playerActionType.Idle;




    //Player2 Variables
    private Player2Scr Player2Script;

    private Animator player2Animator;

    private playerActionType player2CurrentAction = playerActionType.Idle;
    private playerActionType player2NextAction = playerActionType.Idle;

    private float player2HealthBarFillAmount = 0;   //from 0 - 1
    private float player2SuperBarFillAmount = 0;    //from 0 - 1


    private void Start()
    {
        Player1Script = GameObject.Find("Player1_Display").GetComponent<Player1Scr>();
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
    }













    //is called when a control key is pressed
    public void ControlKeyDown(int whichPlayer, playerActionType player1And2Input)
    {
        //see whether or not this action can be implemented and if so implement it
        //Debug.Log("COMMDAND START : " + player1And2Input.ToString()); //test

        if (whichPlayer == 1)
        {
            //for player1

            //if not about to get hit or being hit
            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.GotHit))
            {
                //if neither attack or superattack
                if (!(player1CurrentAction.Equals(playerActionType.RegularAttack) || player1CurrentAction.Equals(playerActionType.SuperAttack)
                    || player1CurrentAction.Equals(playerActionType.BlockSTART) || player1CurrentAction.Equals(playerActionType.Block)
                    || player1CurrentAction.Equals(playerActionType.BlockSTOP)))
                {
                    //testing
                    if (player1CurrentAction.Equals(playerActionType.RegularAttack))
                    {
                        Debug.Log("ATTTTTTACK ERRRRRORR");
                    }




                    if (player1And2Input.Equals(playerActionType.SuperAttack))
                    {
                        if (Player1Script.player1SuperBarFillAmount >= 1)
                        {
                            player1NextAction = player1And2Input;
                            TryForceStateChange(1);
                        }
                    } else
                    {
                        player1NextAction = player1And2Input;
                        TryForceStateChange(1);
                    }
                }



                /*
                //For Launching Block, Super Attack, Regular Attack, MoveBackward, and MoveForward none of the first three can be active
                if (!player1CurrentAction.Equals(playerActionType.Block) && !player1CurrentAction.Equals(playerActionType.RegularAttack)
                    && !player1CurrentAction.Equals(playerActionType.SuperAttack))
                {
                    //set input action to next action
                    if (player1And2Input.Equals(playerActionType.Block))
                    {
                        //when doing block you have to start blockstart
                        player1NextAction = playerActionType.BlockSTART;
                        TryForceStateChange(1);
                    }
                    else if (player1And2Input.Equals(playerActionType.SuperAttack))
                    {
                        //only makes SuperAttack the next action if super bar is at max
                        if(Player1Script.player1SuperBarFillAmount == 1)
                        {
                            player1NextAction = player1And2Input;
                            TryForceStateChange(1);
                        }
                    } else
                    {
                        player1NextAction = player1And2Input;
                        TryForceStateChange(1);
                    }
                }*/
            }
        } else
        {
            //for player2



        }


    }













    //is called when a control key is released
    public void ControlKeyUp(int whichPlayer, playerActionType player1And2Input)
    {
        //see what should be done with the particular action and kill it (if it isn't already killed)
        //Debug.Log("COMMDAND STOP : " + player1And2Input.ToString());  //test

        if (whichPlayer == 1)
        {
            //for player1

            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.SuperAttack)
                && !player1CurrentAction.Equals(playerActionType.RegularAttack) && !player1CurrentAction.Equals(playerActionType.BlockSTART) && !player1CurrentAction.Equals(playerActionType.Block)
                    && !player1CurrentAction.Equals(playerActionType.BlockSTOP))
            {

                //testing
                if (player1CurrentAction.Equals(playerActionType.RegularAttack))
                {
                    Debug.Log("ATTACK ERROR UPPP");
                }

                if (player1And2Input.Equals(playerActionType.BlockSTOP))
                {
                    //when stopping block you need to do blockstop
                    player1NextAction = playerActionType.BlockSTOP;
                }
                else
                {
                    player1NextAction = playerActionType.Idle;
                }
                //temp
                DefaultStateChange(1);
                //TryForceStateChange(1);
            }
            if (player1CurrentAction.Equals(playerActionType.BlockSTART) && player1And2Input.Equals(playerActionType.BlockSTOP))
            {
                player1NextAction = playerActionType.BlockSTOP;
                DefaultStateChange(1);
            }
        }
        else
        {
            //for player2


        }

    }


    //is called when it is requested to force the state to change right now
    public void TryForceStateChange(float whichPlayer)
    {


        if (whichPlayer == 1)
        {
            //for player1

            //if got hit is next action, stop the current action right away and start gothit
            if (player1NextAction.Equals(playerActionType.GotHit))
            {
                //change current to next state, next state to idle
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;
                callTheCurrentState(1);




                //if the current state can be replaced (and it isn't GotHit)
            }
            else if ((!player1CurrentAction.Equals(playerActionType.GotHit))
                && !(player1CurrentAction.Equals(playerActionType.RegularAttack) || player1CurrentAction.Equals(playerActionType.SuperAttack)
                || player1CurrentAction.Equals(playerActionType.Block) || player1CurrentAction.Equals(playerActionType.BlockSTART)
                || player1CurrentAction.Equals(playerActionType.BlockSTOP)))        //THIS needs to be edited probably
            {
                //change current to next state, next state to idle
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;

                //say "forced ___ state in"
                //Debug.Log("FORCED to : " + player1CurrentAction.ToString());

                callTheCurrentState(1);
            }
        }
        else
        {
            //for player2




        }



    }



    //this is called once the current animation ends and calls the next animation
    public void DefaultStateChange(float whichPlayer)
    {


        if (whichPlayer == 1)
        {
            //for Player1
            Debug.Log("Current : " + player1CurrentAction.ToString());
            player1CurrentAction = player1NextAction;
            Debug.Log("After : " + player1CurrentAction.ToString());
            player1NextAction = playerActionType.Idle;
            //Debug.Log("end?");
            callTheCurrentState(1);


        } else
        {
            //for Player2


        }






        //do more than this obviously


    }


    void callTheCurrentState(int whichPlayer)
    {
        if (whichPlayer == 1)
        {
            //for Player1


            //call appropriate state for current!
            if (player1CurrentAction.Equals(playerActionType.GotHit))
            {
                Player1Script.P1GotHit();
            }
            else if (player1CurrentAction.Equals(playerActionType.MoveBackwards))
            {
                Player1Script.P1MoveBackwards();
            }
            else if (player1CurrentAction.Equals(playerActionType.MoveForwards))
            {
                Player1Script.P1MoveForwards();
            }
            else if (player1CurrentAction.Equals(playerActionType.BlockSTART))
            {
                Player1Script.P1BlockSTART();
            }
            else if (player1CurrentAction.Equals(playerActionType.BlockSTOP))
            {
                Player1Script.P1BlockSTOP();
            }
            else if (player1CurrentAction.Equals(playerActionType.RegularAttack))
            {
                Player1Script.P1RegularAttack();
            }
            else if (player1CurrentAction.Equals(playerActionType.SuperAttack))
            {
                Player1Script.P1SuperAttack();
            }
            else if (player1CurrentAction.Equals(playerActionType.Idle))
            {
                Player1Script.P1Idle();
            }


        } else
        {
            //for Player2


        }


    }








}
