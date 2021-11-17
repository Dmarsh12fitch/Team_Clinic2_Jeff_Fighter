using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;


    private Animator Player2Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;

    private string[] statesStrings = { "MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","GotHitState", "IdleState"};

    public float player2HealthBarFillAmount = 0;   //from 0 - 1
    public float player2SuperBarFillAmount = 1;    //from 0 - 1
    public float player2Moving = 0;
    public bool isBlocking;

    public float regularDamage = 5;
    public float superDamage = 15;





    // Start is called before the first frame update
    void Start()
    {
        Player2Animator = GameObject.Find("Player2_Display").GetComponent<Animator>();
        PLAYER1 = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2 = GameObject.Find("Player2").GetComponent<Transform>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
        InvokeRepeating("Player2Move", 0.008f, .008f);                                                //do this later
    }



    //Player2 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void P2MoveForwards()
    {
        setAllToFalseBut("MoveForwardState");
        //Player1Animator.SetBool("MoveForwardState", true);
        Player2MoveMeSet(-1);
    }

    public void P2MoveBackwards()
    {
        setAllToFalseBut("MoveBackwardState");
        //Player1Animator.SetBool("MoveBackwardState", true);
        Player2MoveMeSet(1);
    }

    public void P2BlockSTART()
    {

        isBlocking = true;
        setAllToFalseBut("StartBlockState");
        //Player1Animator.SetBool("StartBlockState", true);
    }

    public void P2BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        //Player1Animator.SetBool("EndBlockState", true);
    }

    public void P2RegularAttack()
    {
        setAllToFalseBut("RegularAttackState");
        //Player1Animator.SetBool("RegularAttackState", true);
    }

    public void P2SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
        //Player1Animator.SetBool("SuperAttackState", true);
    }

    public void P2GotHit()
    {
        setAllToFalseBut("GotHitState");
        //Player1Animator.SetBool("GotHitState", true);
    }

    public void P2Idle()
    {
        isBlocking = false;
        setAllToFalseBut("IdleState");
        //Player2Animator.SetBool("IdleState", true);
    }

    void setAllToFalseBut(string thisStateString)
    {
        Player2MoveMeSet(0);
        foreach (string compareString in statesStrings)
        {
            if (!thisStateString.Equals(compareString))
            {
                Player2Animator.SetBool(compareString, false);
            } else
            {
                Player2Animator.SetBool(compareString, true);
            }
        }
    }

    //Player2 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-





    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Player2MoveMeSet(float dir)
    {
        player2Moving = dir;
    }

    private void Player2Move()
    {
        if (player2Moving == 1 && PLAYER2.position.x + 6 < 25)
        {
            PLAYER2.Translate(0.05f, 0, 0);
        } else if(player2Moving == -1 && PLAYER2.position.x - 6 > PLAYER2.position.x)
        {
            PLAYER2.Translate(-0.05f, 0, 0);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





}
