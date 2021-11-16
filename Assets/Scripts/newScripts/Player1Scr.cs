using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;


    private Animator Player1Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;



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
        Player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();
        PLAYER1 = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2 = GameObject.Find("Player2").GetComponent<Transform>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        if (player1Moving == 1 && PLAYER1.position.x + 6 < PLAYER2.position.x)
        {
            PLAYER1.Translate(0.07f, 0, 0);                                     //this is gitching..make it move move in slowly
        } else if(player1Moving == -1 && PLAYER1.position.x - 6 > -25)
        {
            PLAYER1.Translate(-0.07f, 0, 0);
        }
    }

    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void P1MoveForwards()
    {
        setAllToFalseBut("MoveForwardState");
        Player1Animator.SetBool("MoveForwardState", true);
        Player1MoveMeSet(1);
        //that should be it bc when you let the key up it should snap to idle
    }

    public void P1MoveBackwards()
    {
        setAllToFalseBut("MoveBackwardState");
        Player1Animator.SetBool("MoveBackwardState", true);
        Player1MoveMeSet(-1);
        //setbool move forwards to true
        //that should be it bc when you let the key up it should snap to idle
    }

    public void P1BlockSTART()
    {
        isBlocking = true;
        setAllToFalseBut("StartBlockState");
        Player1Animator.SetBool("StartBlockState", true);
        //setbool blockingstart to true     //(should make it do the blocking start anim)
        //wait
        //setbool blocking to true          //(should make it stay in block) (this should be the bool to see if you actually block something or not)
        //set blockingstart to false
    }

    public void P1BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        Player1Animator.SetBool("EndBlockState", true);
        //setbool blockingstop to true      //(should make it do the blocking stop anim)
        //setbool blocking to false         //(this should be the bool to see if you actually block something or not)^^
        //wait
        //setbool blockingstop to false
        //wait
        //IF blockSTOP is still the current, then call the DefaultStateChange
    }

    public void P1RegularAttack()
    {
        setAllToFalseBut("RegularAttackState");
        Player1Animator.SetBool("RegularAttackState", true);
        //setbool regularAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool regularAttack to false
        //If regularAttack is still the current, then call the DefaultStateChange
    }

    public void P1SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
        Player1Animator.SetBool("SuperAttackState", true);
        //setbool superAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool superAttack to false
        //IF superAttack is still the current, then call the DefaultStateChange
    }

    public void P1GotHit()
    {
        setAllToFalseBut("GotHitState");
        //MORE
    }

    public void P1Idle()
    {
        isBlocking = false;
        setAllToFalseBut("IdleState");
        Player1Animator.SetBool("IdleState", true);
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
        }
    }

    //Player1 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-































    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Player1MoveMeSet(float dir)
    {
        player1Moving = dir;
    }


    public void Player1SuperAttackHitAttempt()
    {
        if(transform.position.x + 7 > PLAYER2.position.x)      //distance to hit
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(15));
            player1SuperBarFillAmount = 0f;
            updateSuperBarDisplay();
            //call to damage function of the other player
        }
    }


    public void Player1RegularAttackHitAttempt()
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
            updateSuperBarDisplay();
            //call the damage function of the other player
        }
    }

    public void Player1HasFinishedAnim()
    {
        Player1And2Manager.Instance.DefaultStateChange(1);
    }

    public void PlayerFinishedBlockStart()
    {
        Player1Animator.SetBool("BlockState", true);
        Player1Animator.SetBool("StartBlockState", false);
    }

    public void PlayerFinishedBlockStop()
    {
        Player1And2Manager.Instance.DefaultStateChange(1);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    

    public void Player1TakeDamage(float damage)
    {
        if (!isBlocking)
        {
            Player1TakeDamage(damage);
            updateHealthBarDisplay();
            P1GotHit();                     //make sure to add this anim!
        }
    }


    void updateHealthBarDisplay()
    {
        //update the health display here
    }

    void updateSuperBarDisplay()
    {
        //update the super display here
    }

    //take damage function
    //take damage if not blocking
    //if you take damage force got hit animation

}
