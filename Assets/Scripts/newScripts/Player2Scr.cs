using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;

    public AudioClip[] PunchSoundArray;





    private Animator Player2Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;

    private Player1Scr Player1Script;

    private PlayerUIController Player2UIController;

    private string[] statesStrings = { "MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","StunnedState", "IdleState", "DeadState"
                                        ,"GotHitSuperTypeAState","GotHitSuperTypeBState"};

    private float player2HealthBarFillAmount = 1;   //from 0 - 1
    private float player2SuperBarFillAmount = 0;    //from 0 - 1
    public float player2Moving = 0;
    public bool isBlocking;
    public bool immune;

    public float moveDistanceLimit;

    public float regularDamage = 0.02f;
    public float superDamage = 0.08f;





    // Start is called before the first frame update
    void Start()
    {
        Player1Script = GameObject.Find("Player1_Display").GetComponent<Player1Scr>();
        Player2Animator = GameObject.Find("Player2_Display").GetComponent<Animator>();
        PLAYER1 = GameObject.Find("Player1").GetComponent<Transform>();
        PLAYER2 = GameObject.Find("Player2").GetComponent<Transform>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
        Player2UIController = GameObject.Find("Player2UI").GetComponent<PlayerUIController>();

        InvokeRepeating("Player2Move", 0.008f, .008f);                                                //do this later
    }



    //Player2 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

    public void P2MoveForwards()
    {
        setAllToFalseBut("MoveForwardState");
        Player2MoveMeSet(-1);
    }

    public void P2MoveBackwards()
    {
        setAllToFalseBut("MoveBackwardState");
        Player2MoveMeSet(1);
    }

    public void P2BlockSTART()
    {
        isBlocking = true;
        setAllToFalseBut("StartBlockState");
    }

    public void P2BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        StartCoroutine(JustInCase());
    }

    public void P2RegularAttack()
    {
        setAllToFalseBut("RegularAttackState");
    }

    public void P2SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
    }

    public void P2GotHitSuperTypeA()
    {
        setAllToFalseBut("GotHitSuperTypeAState");
    }

    public void P2GotHitSuperTypeB()
    {
        setAllToFalseBut("GotHitSuperTypeBState");
    }

    public void P2Stunned()
    {
        setAllToFalseBut("StunnedState");
    }

    public void P2Dies()
    {
        InputManager.Instance.StopFightingInputs = true;
        setAllToFalseBut("DeadState");
    }

    public void P2Idle()
    {
        isBlocking = false;
        setAllToFalseBut("IdleState");
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










    IEnumerator JustInCase()
    {
        yield return new WaitForSeconds(0.25f);
        if (Player1And2Manager.Instance.Player2GetCurrent() == Player1And2Manager.playerActionType.BlockSTOP)
        {
            Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.Idle);
        }
    }




    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void Player2DetermineSuperHitType()
    {
        immune = true;
        if (transform.position.x + 10 < moveDistanceLimit)
        {
            //inside the range
            P2GotHitSuperTypeA();
            StartCoroutine(Player2FallingBack());
        }
        else
        {
            //outside the range
            P2GotHitSuperTypeB();
            //call some function to actually launch the character to the edge of the wall and then stay there.
        }
    }

    public void Player2MoveMeSet(float dir)
    {
        player2Moving = dir;
    }

    IEnumerator Player2FallingBack()
    {
        yield return new WaitForSeconds(0.05f);
        player2Moving = 2;
        yield return new WaitForSeconds(0.25f);
        player2Moving = 1;
        yield return new WaitForSeconds(0.15f);
        player2Moving = 0;
    }

    private void Player2Move()
    {
        if (player2Moving == 1 && PLAYER2.position.x + 1f < moveDistanceLimit)
        {
            PLAYER2.Translate(0.05f, 0, 0);
        } else if(player2Moving == -1 && PLAYER2.position.x - 6.5f > PLAYER1.position.x)
        {
            PLAYER2.Translate(-0.05f, 0, 0);
        } else if(player2Moving == 2 /*&& PLAYER2.position.x + 6.5f < moveDistanceLimit*/)
        {
            PLAYER2.Translate(0.2f, 0, 0);
        }
    }

    public void PlayerSuperAttackHitAttempt()
    {
        if (transform.position.x - 7.5f < PLAYER1.position.x)      //see if the player can hit them because they're on the ground!!!
        {
            //call to damage function of the other player
            Player1Script.Player1TakeDamage(superDamage);
        }
        player2SuperBarFillAmount = 0f;
        updateSuperBarDisplay();
    }

    public void PlayerRegularAttackHitAttempt()
    {
        Debug.Log("P2RegAttackHit");
        if (transform.position.x - 7.5f < PLAYER1.position.x)       //see if the player can hit them because they're on the ground!!!
        {
            if (player2SuperBarFillAmount >= 1)
            {
                player2SuperBarFillAmount = 1;
            }
            else if(!Player1Script.isBlocking && !Player1Script.immune)
            {
                player2SuperBarFillAmount += 0.2f;
            }
            Player1Script.Player1TakeDamage(regularDamage);
            updateSuperBarDisplay();
        }
    }

    public void Player2TakeDamage(float damage)
    {
        if (!immune)
        {
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(damage));
            makeRandomPunchSound();
            if (damage == superDamage)
            {
                if (!isBlocking)
                {
                    Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.GotHit);
                    player2HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    player2HealthBarFillAmount -= damage / 200;
                    //STUNNED SHOULD BE CATEGORIZED AS GOTHIT (make sure that is set when calling it)
                    //trigger the stunned anim in player2 (THIS SCRIPT!)
                }
            }
            else
            {
                if (!isBlocking)
                {
                    player2HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    Debug.Log("Got here");
                    player2HealthBarFillAmount -= damage / 200;
                }
            }
            updateHealthBarDisplay();
            if (player2HealthBarFillAmount <= 0)
            {
                P2Dies();
            }
        }
    }

    public void PlayerHasFinishedAnim(Player1And2Manager.playerActionType type)
    {
        immune = false;
        if (!(type.Equals(Player1And2Manager.playerActionType.BlockSTOP) && !Player1And2Manager.Instance.Player2GetCurrent().Equals(Player1And2Manager.playerActionType.BlockSTOP)))
        {
            Player1And2Manager.Instance.DefaultStateChange(2);
        }
    }

    public void PlayerFinishedBlockStart()
    {
        Player2Animator.SetBool("BlockState", true);
        Player2Animator.SetBool("StartBlockState", false);
    }

    public float GetPlayer2SuperFillAmount()
    {
        return player2SuperBarFillAmount;
    }

    public float GetPlayer2HealthFillAmount()
    {
        return player2HealthBarFillAmount;
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



    void updateHealthBarDisplay()
    {
        //update the health display here
        Player2UIController.PlayerHealthChange(player2HealthBarFillAmount);
    }

    void updateSuperBarDisplay()
    {
        //update the super display here
        Player2UIController.PlayerSuperChange(player2SuperBarFillAmount);
    }








    //SOUNDS !!! SOUNDS !!!

    void makeRandomPunchSound()
    {
        int rand = Random.Range(0, PunchSoundArray.Length);
        gameObject.GetComponent<AudioSource>().clip = PunchSoundArray[rand];
        gameObject.GetComponent<AudioSource>().Play();
    }




}
