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
    private GameObject Player1Obj;

    private Animator player1Animator;

    private playerActionType player1CurrentAction = playerActionType.Idle;
    private playerActionType player1NextAction = playerActionType.Idle;

    private float player1HealthBarFillAmount = 0;   //from 0 - 1
    private float player1SuperBarFillAmount = 1;    //from 0 - 1


    //Player2 Variables
    private GameObject Player2Obj;

    private Animator player2Animator;

    private playerActionType player2CurrentAction = playerActionType.Idle;
    private playerActionType player2NextAction = playerActionType.Idle;

    private float player2HealthBarFillAmount = 0;   //from 0 - 1
    private float player2SuperBarFillAmount = 0;    //from 0 - 1


    private void Start()
    {
        Player1Obj = GameObject.Find("Player1");                                //ONLY DO THIS IN THE PLAYER1SCRIPT
        Player2Obj = GameObject.Find("Player2");                                //same here
        player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();
        //player2Animator = GameObject.Find("Player2_Display").GetComponent<Animator>();
    }


















    //is called when a control key is pressed
    public void ControlKeyDown(int whichPlayer, playerActionType player1And2Input)
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
                        TryForceStateChange(1);
                    }
                    else if (player1And2Input.Equals(playerActionType.SuperAttack))
                    {
                        //only makes SuperAttack the next action if super bar is at max
                        if(player1SuperBarFillAmount == 1)
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

                TryForceStateChange(1);
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
                || player1CurrentAction.Equals(playerActionType.BlockSTOP)))        //THIS needs to be edited probably
            {
                //change current to next state, next state to idle
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;

                //say "forced ___ state in"
                Debug.Log("FORCED to : " + player1CurrentAction.ToString());

                //call appropriate state for current!
                if (player1CurrentAction.Equals(playerActionType.MoveBackwards))
                {
                    P1MoveBackwards();
                } else if (player1CurrentAction.Equals(playerActionType.MoveForwards))
                {
                    P1MoveForwards();
                } else if (player1CurrentAction.Equals(playerActionType.BlockSTART))
                {
                    P1BlockSTART();
                } else if (player1CurrentAction.Equals(playerActionType.BlockSTOP))
                {
                    P1BlockSTOP();
                } else if (player1CurrentAction.Equals(playerActionType.RegularAttack))
                {
                    P1RegularAttack();
                } else if (player1CurrentAction.Equals(playerActionType.SuperAttack))
                {
                    P1SuperAttack();
                } else if (player1CurrentAction.Equals(playerActionType.Idle))
                {
                    //Idle
                }
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


    }






    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


        //ARE THESE EVEN ACCURATE??? HOW AM I SUPPOSED TO MAKE THINGS SNAP???

    void P1MoveForwards()
    {
        player1Animator.SetBool("MoveForwardState", true);
        //that should be it bc when you let the key up it should snap to idle
    }

    void P1MoveBackwards()
    {
        //setbool move forwards to true
        //that should be it bc when you let the key up it should snap to idle
    }

    void P1BlockSTART()
    {
        //setbool blockingstart to true     //(should make it do the blocking start anim)
        //wait
        //setbool blocking to true          //(should make it stay in block) (this should be the bool to see if you actually block something or not)
        //set blockingstart to false
    }

    void P1BlockSTOP()
    {
        //setbool blockingstop to true      //(should make it do the blocking stop anim)
        //setbool blocking to false         //(this should be the bool to see if you actually block something or not)^^
        //wait
        //setbool blockingstop to false
        //wait
        //IF blockSTOP is still the current, then call the DefaultStateChange
    }

    void P1RegularAttack()
    {
        //setbool regularAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool regularAttack to false
        //If regularAttack is still the current, then call the DefaultStateChange
    }

    void P1SuperAttack()
    {
        player1Animator.SetBool("SuperAttackState", true);
        //setbool superAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool superAttack to false
        //IF superAttack is still the current, then call the DefaultStateChange
    }

    void p1GotHit()
    {
        
    }

    //Player2 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-




    //Player1 Functions START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void Player1Damaged()
    {

    }


    //Player1 Functions STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    













}
