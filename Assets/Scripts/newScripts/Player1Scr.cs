using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;

    public AudioClip[] PunchSoundArray;




    private Animator Player1Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;
    

    private PlayerUIController Player1UIController;

    private Player2Scr Player2Script;

    private string[] statesStrings = { "MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","StunnedState", "IdleState", "DeadState"
                                        ,"GotHitSuperTypeAState","GotHitSuperTypeBState"};

    private float player1HealthBarFillAmount = 1;   //from 0 - 1
    private float player1SuperBarFillAmount = 0;    //from 0 - 1
    public float player1Moving = 0;
    public bool isBlocking;
    public bool immune;
    public float moveDistanceLimit;

    public float regularDamage = 0.02f;
    public float superDamage = 0.08f;


    // Start is called before the first frame update
    void Start()
    {
        Player2Script = GameObject.Find("Player2_Display").GetComponent<Player2Scr>();
        Player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();
        PLAYER1 = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2 = GameObject.Find("Player2").GetComponent<Transform>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
        Player1UIController = GameObject.Find("Player1UI").GetComponent<PlayerUIController>();

        InvokeRepeating("Player1Move", 0.008f, .008f);
    }

    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void P1MoveForwards()
    {
        setAllToFalseBut("MoveForwardState");
        Player1MoveMeSet(1);
    }

    public void P1MoveBackwards()
    {
        setAllToFalseBut("MoveBackwardState");
        Player1MoveMeSet(-1);
    }

    public void P1BlockSTART()
    {
        isBlocking = true;
        setAllToFalseBut("StartBlockState");
    }

    public void P1BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
    }

    public void P1RegularAttack()
    {
        setAllToFalseBut("RegularAttackState");
    }

    public void P1SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
    }

    public void P1GotHitSuperTypeA()
    {
        setAllToFalseBut("GotHitSuperTypeAState");
    }

    public void P1GotHitSuperTypeB()
    {
        setAllToFalseBut("GotHitSuperTypeBState");
    }

    public void P1Stunned()
    {
        setAllToFalseBut("StunnedState");
    }

    public void P1Dies()
    {
        InputManager.Instance.StopFightingInputs = true;
        setAllToFalseBut("DeadState");
    }

    public void P1Idle()
    {
        isBlocking = false;
        setAllToFalseBut("IdleState");
    }

    void setAllToFalseBut(string thisStateString)
    {
        Player1MoveMeSet(0);
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

    public void Player1DetermineSuperHitType()
    {
        immune = true;
        if (transform.position.x - 10 > moveDistanceLimit)
        {
            //inside the range
            P1GotHitSuperTypeA();
            StartCoroutine(Player1FallingBack());
        } else
        {
            //outside the range
            P1GotHitSuperTypeB();
            //call some function to actually launch the character to the edge of the wall and then stay there.
        }
    }

    IEnumerator Player1FallingBack()
    {
        yield return new WaitForSeconds(0.05f);
        player1Moving = -2;
        yield return new WaitForSeconds(0.25f);
        player1Moving = -1;
        yield return new WaitForSeconds(0.15f);
        player1Moving = 0;
    }

    IEnumerator PlayerBackToRingSide()
    {
        yield return new WaitForSeconds(0.05f);
        if(transform.position.x < moveDistanceLimit)
        {

        }
        player1Moving = -2;
        yield return new WaitForSeconds(0.25f); //adjust times
        player1Moving = -1;
        yield return new WaitForSeconds(0.15f);
        player1Moving = 0;
    }


    public void Player1MoveMeSet(float dir)
    {
        player1Moving = dir;
    }

    private void Player1Move()
    {
        if (player1Moving == 1 && PLAYER1.position.x + 6.5f < PLAYER2.position.x)
        {
            PLAYER1.Translate(0.05f, 0, 0);
        }
        else if (player1Moving == -1 && PLAYER1.position.x - 1f > moveDistanceLimit)
        {
            PLAYER1.Translate(-0.05f, 0, 0);
        } else if(player1Moving == -2 /*&& PLAYER1.position.x - 6.5f > moveDistanceLimit*/)
        {
            PLAYER1.Translate(-0.2f, 0, 0);
        }
    }

    public void PlayerSuperAttackHitAttempt()
    {
        if(transform.position.x + 7.5f > PLAYER2.position.x)           //see if the player can hit them because they're on the ground!!!
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(superDamage));
            Player2Script.Player2TakeDamage(superDamage);
        }
        player1SuperBarFillAmount = 0f;
        updateSuperBarDisplay();
    }


    public void PlayerRegularAttackHitAttempt()
    {
        Debug.Log("P1RegAttackHit");
        if(transform.position.x + 7.5f > PLAYER2.position.x)               //see if the player can hit them because they're on the ground!!!
        {
            if(player1SuperBarFillAmount >= 1)
            {
                player1SuperBarFillAmount = 1;
            } else if(!Player2Script.isBlocking && !Player2Script.immune)
            {
                player1SuperBarFillAmount += 0.2f;
            }
            Player2Script.Player2TakeDamage(regularDamage);
            updateSuperBarDisplay();
        }
    }

    public void Player1TakeDamage(float damage)
    {
        if (!immune)
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(damage));
            makeRandomPunchSound();
            if (damage == superDamage)
            {
                if (!isBlocking)
                {
                    Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.GotHit);
                    player1HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    player1HealthBarFillAmount -= damage / 200;
                    //STUNNED SHOULD BE CATEGORIZED AS GOTHIT (make sure that is set when calling it)
                    //trigger the stunned anim in player1 (THIS SCRIPT)
                }
            }
            else
            {
                if (!isBlocking)
                {
                    player1HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    player1HealthBarFillAmount -= damage / 200;
                }
            }
            updateHealthBarDisplay();
            if (player1HealthBarFillAmount <= 0)
            {
                P1Dies();
            }
        }
    }

    public void PlayerHasFinishedAnim(Player1And2Manager.playerActionType type)
    {
        immune = false;
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

    public float GetPlayer1SuperFillAmount()
    {
        return player1SuperBarFillAmount;
    }

    public float GetPlayer1HealthFillAmount()
    {
        return player1HealthBarFillAmount;
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





    void updateHealthBarDisplay()
    {
        //update the health display here
        Player1UIController.PlayerHealthChange(player1HealthBarFillAmount);
    }

    void updateSuperBarDisplay()
    {
        //update the super display here
        Player1UIController.PlayerSuperChange(player1SuperBarFillAmount);
    }





    //SOUNDS !!! SOUNDS !!!

    void makeRandomPunchSound()
    {
        int rand = Random.Range(0, PunchSoundArray.Length);
        gameObject.GetComponent<AudioSource>().clip = PunchSoundArray[rand];
        gameObject.GetComponent<AudioSource>().Play();
    }




}
