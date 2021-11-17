using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;
    

    private Animator Player1Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;

    private Player2Scr Player2Script;

    private string[] statesStrings = { "MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","GotHitState", "IdleState"};

    public float player1HealthBarFillAmount = 0;   //from 0 - 1
    public float player1SuperBarFillAmount = 0;    //from 0 - 1
    public float player1Moving = 0;
    public bool isBlocking;

    public float regularDamage = 5;
    public float superDamage = 15;


    // Start is called before the first frame update
    void Start()
    {
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
        Player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();
        PLAYER1 = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2 = GameObject.Find("Player2").GetComponent<Transform>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
        InvokeRepeating("Player1Move", 0.008f, .008f);
    }

    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void P1MoveForwards()
    {
        setAllToFalseBut("MoveForwardState");
        //Player1Animator.SetBool("MoveForwardState", true);
        Player1MoveMeSet(1);
    }

    public void P1MoveBackwards()
    {
        setAllToFalseBut("MoveBackwardState");
        //Player1Animator.SetBool("MoveBackwardState", true);
        Player1MoveMeSet(-1);
    }

    public void P1BlockSTART()
    {
        
        isBlocking = true;
        setAllToFalseBut("StartBlockState");
        //Player1Animator.SetBool("StartBlockState", true);
    }

    public void P1BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        //Player1Animator.SetBool("EndBlockState", true);
    }

    public void P1RegularAttack()
    {
        setAllToFalseBut("RegularAttackState");
        //Player1Animator.SetBool("RegularAttackState", true);
    }

    public void P1SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
        //Player1Animator.SetBool("SuperAttackState", true);
    }

    public void P1GotHit()
    {
        setAllToFalseBut("GotHitState");
        //Player1Animator.SetBool("GotHitState", true);
    }

    public void P1Idle()
    {
        isBlocking = false;
        setAllToFalseBut("IdleState");
        //Player1Animator.SetBool("IdleState", true);
    }

    void setAllToFalseBut(string thisStateString)
    {
        Player1MoveMeSet(0);                                 //revisit
        foreach (string compareString in statesStrings)
        {
            if (!thisStateString.Equals(compareString))
            {
                Player1Animator.SetBool(compareString, false);
            }
            else
            {
                Player1Animator.SetBool(compareString, true);
            }
        }
    }

    //Player1 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-































    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Player1MoveMeSet(float dir)
    {
        player1Moving = dir;
    }


    public void PlayerSuperAttackHitAttempt()
    {
        if(transform.position.x + 7 > PLAYER2.position.x)      //distance to hit
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(15));
            //call to damage function of the other player
        }
        player1SuperBarFillAmount = 0f;
        updateSuperBarDisplay();
    }


    public void PlayerRegularAttackHitAttempt()
    {
        if(transform.position.x + 7 > PLAYER2.position.x)       //distance to hit
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(15));
            if(player1SuperBarFillAmount >= 1)
            {
                player1SuperBarFillAmount = 1;
            } else
            {
                player1SuperBarFillAmount += 0.2f;
            }
            Player2Script.Player2TakeDamage(regularDamage);
            updateSuperBarDisplay();
        }
    }

    public void PlayerHasFinishedAnim(Player1And2Manager.playerActionType type)
    {
        if (!(type.Equals(Player1And2Manager.playerActionType.BlockSTOP) && !Player1And2Manager.Instance.Player1GetCurrent().Equals(Player1And2Manager.playerActionType.BlockSTOP)))
        {
            Player1And2Manager.Instance.DefaultStateChange(1);
        }
    }

    public void PlayerFinishedBlockStart()
    {
        Player1Animator.SetBool("BlockState", true);
        Player1Animator.SetBool("StartBlockState", false);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void Player1TakeDamage(float damage)
    {
        if (!isBlocking)
        {
            //Player1TakeDamage(damage);                //NOT RECURSIVE, JUSt edits tthe fillamount
            Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.GotHit);
            P1GotHit();
        } else
        {
            //Player1TakeDamage(damage / 2);
            Player2Script.P2GotHit();
        }
        updateHealthBarDisplay();
    }


    void updateHealthBarDisplay()
    {
        //update the health display here
    }

    void updateSuperBarDisplay()
    {
        //update the super display here
    }

    private void Player1Move()
    {
        if (player1Moving == 1 && PLAYER1.position.x + 6 < PLAYER2.position.x)
        {
            PLAYER1.Translate(0.05f, 0, 0);                                     //this is gitching..make it move move in slowly
        } else if(player1Moving == -1 && PLAYER1.position.x - 6 > -25)
        {
            PLAYER1.Translate(-0.05f, 0, 0);
        }
    }

}
