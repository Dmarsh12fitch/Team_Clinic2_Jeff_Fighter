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
    private playerActionType player1LastAction = playerActionType.Idle;
    private float BackupReset1 = 10;

    //Player2 Variables
    private Player2Scr Player2Script;

    private playerActionType player2CurrentAction = playerActionType.Idle;
    private playerActionType player2NextAction = playerActionType.Idle;
    private playerActionType player2LastAction = playerActionType.Idle;
    private float BackupReset2 = 10;

    private void Start()
    {
        Player1Script = GameObject.Find("Player1_Display").GetComponent<Player1Scr>();
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
    }


    private void Update()
    {
        if(player1CurrentAction == player1LastAction && player1CurrentAction != playerActionType.Idle)
        {
            BackupReset1 -= Time.deltaTime;
            if(BackupReset1 < 0)
            {
                BackupReset1 = 0;
                Debug.Log("RESET P1!");
            }
            //timer ++
            //player1NextAction = playerActionType.Idle;
            //DefaultStateChange(1);
        }
        if(player2CurrentAction == player2LastAction && player2CurrentAction != playerActionType.Idle)
        {
            BackupReset2 -= Time.deltaTime;
            if (BackupReset2 < 0)
            {
                BackupReset2 = 0;
                Debug.Log("RESET P2!");
            }
            //timer ++
            //player2NextAction = playerActionType.Idle;
            //DefaultStateChange(2);
        }
    }




    //is called when a control key is pressed
    public void ControlKeyDown(int whichPlayer, playerActionType player1And2Input)
    {
        //see whether or not this action can be implemented and if so implement it
        //Debug.Log("COMMDAND START : " + player1And2Input.ToString()); //test

        if (whichPlayer == 1)
        {
            //for player1

            //if not about to get hit nor about to block
            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.BlockSTART))
            {
                //it can be set but not run (for all of them at least) before the end of any animation
                if (player1And2Input.Equals(playerActionType.SuperAttack))
                {
                    if (Player1Script.GetPlayer1SuperFillAmount() >= 1)
                    {
                        Player1Script.player1SuperBarFillAmount = 0f;
                        Debug.Log("Super");
                        player1NextAction = player1And2Input;
                        TryForceStateChange(1);
                    }
                }
                else if (player1And2Input == playerActionType.BlockSTART && (player1CurrentAction == playerActionType.MoveBackwards
                    || player1CurrentAction == playerActionType.MoveForwards))
                {
                    Debug.Log("NO");

                    
                } else
                {
                    player1NextAction = player1And2Input;
                    TryForceStateChange(1);
                }
            }
        } else
        {
            //for player2

            //if not about to get hit nor about to block
            if (!player2NextAction.Equals(playerActionType.GotHit) && !player2CurrentAction.Equals(playerActionType.BlockSTART))
            {
                //it can be set but not run (for all of them at least) before the end of any animation
                if (player1And2Input.Equals(playerActionType.SuperAttack))
                {
                    if (Player2Script.GetPlayer2SuperFillAmount() >= 1)
                    {
                        Player2Script.player2SuperBarFillAmount = 0f;
                        player2NextAction = player1And2Input;
                        TryForceStateChange(2);
                    }
                }
                else if (player1And2Input == playerActionType.BlockSTART && (player2CurrentAction == playerActionType.MoveBackwards
                    || player2CurrentAction == playerActionType.MoveForwards))
                {

                    Debug.Log("NO");


                } else
                {

                    player2NextAction = player1And2Input;
                    TryForceStateChange(2);
                }                
            }
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

            if (!player1NextAction.Equals(playerActionType.GotHit) && !player1CurrentAction.Equals(playerActionType.GotHit) 
                && !player1CurrentAction.Equals(playerActionType.SuperAttack) && !player1CurrentAction.Equals(playerActionType.RegularAttack) 
                && !player1CurrentAction.Equals(playerActionType.BlockSTART) && !player1CurrentAction.Equals(playerActionType.Block))
            {
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
            if (player1CurrentAction.Equals(playerActionType.Block) && player1And2Input.Equals(playerActionType.BlockSTOP))
            {
                player1NextAction = playerActionType.BlockSTOP;
                DefaultStateChange(1);
            }
        }
        else
        {
            //for player2


            if (!player2NextAction.Equals(playerActionType.GotHit) && !player2CurrentAction.Equals(playerActionType.GotHit)
                && !player2CurrentAction.Equals(playerActionType.SuperAttack) && !player2CurrentAction.Equals(playerActionType.RegularAttack) 
                && !player2CurrentAction.Equals(playerActionType.BlockSTART) && !player2CurrentAction.Equals(playerActionType.Block))
            {
                if (player1And2Input.Equals(playerActionType.BlockSTOP))
                {
                    //when stopping block you need to do blockstop
                    player2NextAction = playerActionType.BlockSTOP;
                }
                else
                {
                    player2NextAction = playerActionType.Idle;
                }
                DefaultStateChange(2);
            }
            if (player2CurrentAction.Equals(playerActionType.Block) && player1And2Input.Equals(playerActionType.BlockSTOP))
            {
                player2NextAction = playerActionType.BlockSTOP;
                DefaultStateChange(2);
            }
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
                CallTheCurrentState(1);

                //if the current state can be replaced (and it isn't GotHit)
            }
            else if ((!player1CurrentAction.Equals(playerActionType.GotHit))
                && !(player1CurrentAction.Equals(playerActionType.RegularAttack) || player1CurrentAction.Equals(playerActionType.SuperAttack) 
                || player1CurrentAction.Equals(playerActionType.Block) 
                || player1CurrentAction.Equals(playerActionType.BlockSTART)))
            {
                //change current to next state, next state to idle
                //Debug.Log("PUN CH");
                player1CurrentAction = player1NextAction;
                player1NextAction = playerActionType.Idle;
                CallTheCurrentState(1);
            }
        }
        else
        {
            //for player2

            //if got hit is next action, stop the current action right away and start gothit
            if (player2NextAction.Equals(playerActionType.GotHit))
            {
                //change current to next state, next state to idle
                player2CurrentAction = player2NextAction;
                player2NextAction = playerActionType.Idle;
                CallTheCurrentState(2);

                //if the current state can be replaced (and it isn't GotHit)
            }
            else if ((!player2CurrentAction.Equals(playerActionType.GotHit))
                && !(player2CurrentAction.Equals(playerActionType.RegularAttack) || player2CurrentAction.Equals(playerActionType.SuperAttack)
                || player2CurrentAction.Equals(playerActionType.Block) || player2CurrentAction.Equals(playerActionType.BlockSTART)))
            {
                //change current to next state, next state to idle
                player2CurrentAction = player2NextAction;
                player2NextAction = playerActionType.Idle;
                CallTheCurrentState(2);
            }
        }
    }

    //this is called once the current animation ends and calls the next animation
    public void DefaultStateChange(float whichPlayer)
    {


        if (whichPlayer == 1)
        {
            //for Player1
            player1CurrentAction = player1NextAction;
            player1NextAction = playerActionType.Idle;
            CallTheCurrentState(1);


        } else
        {
            //for Player2
            player2CurrentAction = player2NextAction;
            player2NextAction = playerActionType.Idle;
            CallTheCurrentState(2);
        }
    }

    void CallTheCurrentState(int whichPlayer)
    {
        if (whichPlayer == 1)
        {
            //for Player1


            //call appropriate state for current!
            if (player1CurrentAction.Equals(playerActionType.GotHit))
            {
                if (Player1Script.doStunned)
                {
                    Player1Script.P1Stunned();
                } else if(!(Player1Script.GetPlayer1HealthFillAmount() <= 0))
                {
                    Player1Script.Player1DetermineSuperHitType();
                } else
                {
                    Player1Script.P1Dies();
                }
                
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
            
            //call appropriate state for current!
            if (player2CurrentAction.Equals(playerActionType.GotHit))
            {
                if (Player2Script.doStunned)
                {
                    Debug.Log("reee2");
                    Player2Script.P2Stunned();
                } else if(!(Player2Script.GetPlayer2HealthFillAmount() <= 0))
                {
                    Player2Script.Player2DetermineSuperHitType();
                } else
                {
                    Player2Script.P2Dies();
                }
                
            }
            else if (player2CurrentAction.Equals(playerActionType.MoveBackwards))
            {
                Player2Script.P2MoveBackwards();
            }
            else if (player2CurrentAction.Equals(playerActionType.MoveForwards))
            {
                Player2Script.P2MoveForwards();
            }
            else if (player2CurrentAction.Equals(playerActionType.BlockSTART))
            {
                Player2Script.P2BlockSTART();
            }
            else if (player2CurrentAction.Equals(playerActionType.BlockSTOP))
            {
                Player2Script.P2BlockSTOP();
            }
            else if (player2CurrentAction.Equals(playerActionType.RegularAttack))
            {
                Player2Script.P2RegularAttack();
            }
            else if (player2CurrentAction.Equals(playerActionType.SuperAttack))
            {
                Player2Script.P2SuperAttack();
            }
            else if (player2CurrentAction.Equals(playerActionType.Idle))
            {
                Player2Script.P2Idle();
            }
        }
    }






    //---Next/current get/sets---
    public void Player1SetNextTo(playerActionType setTo)
    {
        if(setTo == playerActionType.Block)
        {
            player1CurrentAction = setTo;
        } else if(setTo == playerActionType.GotHit)
        {
            Debug.Log("P1 should be stunned");
            player1NextAction = setTo;
            DefaultStateChange(1);
        } else
        {
            player1NextAction = setTo;
            TryForceStateChange(1);
        }
    }

    public playerActionType Player1GetCurrent()
    {
        return player1CurrentAction;
    }

    public void Player2SetNextTo(playerActionType setTo)
    {
        if(setTo == playerActionType.Block)
        {
            player2CurrentAction = setTo;
        } else if(setTo == playerActionType.GotHit)
        {
            Debug.Log("P2 should be stunned");
            player2NextAction = setTo;
            DefaultStateChange(2);
        } else
        {
            player2NextAction = setTo;
            TryForceStateChange(2);
        }
    }

    public playerActionType Player2GetCurrent()
    {
        return player2CurrentAction;
    }
}
