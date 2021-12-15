using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;

    public AudioClip[] PunchSoundArray;

    public AudioClip[] BarkSoundArray;

    string currentState = "IdleState";

    public bool doStunned = false;

    public Animator Player2Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;

    private Player1Scr Player1Script;

    private PlayerUIController Player2UIController;

    private string[] statesStrings = {"MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","StunnedState", "IdleState", "DeadState"
                                        ,"GotHitSuperTypeAState","GotHitSuperTypeBState"};

    public float player2HealthBarFillAmount = 1;   //from 0 - 1
    public float player2SuperBarFillAmount = 0;    //from 0 - 1
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
        setAllToFalseBut("StartBlockState");
        StartCoroutine(JustInCase());
    }

    public void P2BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        StartCoroutine(JustInCase());
    }

    public void P2RegularAttack()
    {
        /*
        int parameters = Player2Animator.parameterCount;
        AnimatorControllerParameter[] thingie;
        thingie = new AnimatorControllerParameter[parameters];
        foreach (var state in Player2Animator.parameters)
        {
            if (state.type is AnimatorControllerParameterType.Bool)
            {
                if (Player2Animator.GetBool(state.name) == true)
                {
                    currentState = state.name;
                }
            }
        }
        */
        setAllToFalseBut("RegularAttackState");
        Player2Animator.SetTrigger("PunchNormTrig");
    }

    public void P2SuperAttack()
    {
        setAllToFalseBut("SuperAttackState");
        Player2Animator.SetTrigger("PunchSuperTrig");
        /*
        int parameters = Player2Animator.parameterCount;
        AnimatorControllerParameter[] thingie;
        thingie = new AnimatorControllerParameter[parameters];
        foreach (var state in Player2Animator.parameters)
        {
            if (state.type is AnimatorControllerParameterType.Bool)
            {
                if (Player2Animator.GetBool(state.name) == true && state.name == "SuperAttack")
                {
                    setAllToFalseBut("IdleState");
                    Player2Animator.ResetTrigger("PunchSuperTrig");
                }
                else
                {
                    setAllToFalseBut("SuperAttackState");
                    Player2Animator.SetTrigger("PunchSuperTrig");
                }
            }
        }*/

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
        isBlocking = false;
        doStunned = false;
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
        if (Player1And2Manager.Instance.Player2GetCurrent() == Player1And2Manager.playerActionType.BlockSTOP
            || Player1And2Manager.Instance.Player2GetCurrent() == Player1And2Manager.playerActionType.BlockSTART)
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
            StartCoroutine(PlayerBackToRingSide());
        }
    }

    public void Player2MoveMeSet(float dir)
    {
        player2Moving = dir;
    }

    IEnumerator Player2FallingBack()
    {
        GameObject.Find("crowd").GetComponent<crowdOcc>().randSound();
        yield return new WaitForSeconds(0.05f);
        player2Moving = 2;
        yield return new WaitForSeconds(0.25f);
        player2Moving = 1;
        yield return new WaitForSeconds(0.15f);
        player2Moving = 0;
    }

    IEnumerator PlayerBackToRingSide()
    {
        GameObject.Find("crowd").GetComponent<crowdOcc>().randSound();
        for (int i = 0; i < 100; i++)
        {
            if (Player1And2Manager.Instance.Player2GetCurrent().Equals(Player1And2Manager.playerActionType.GotHit))
            {
                if (transform.position.x < moveDistanceLimit)
                {
                    player2Moving = 2;
                }
                else
                {
                    player2Moving = 0;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }


    private void Player2Move()
    {
        if (player2Moving == 1 && PLAYER2.position.x + 1f < moveDistanceLimit)
        {
            PLAYER2.Translate(0.1f, 0, 0);
        } else if(player2Moving == -1 && PLAYER2.position.x - 5.3f > PLAYER1.position.x)
        {
            PLAYER2.Translate(-0.1f, 0, 0);
        } else if(player2Moving == 2 /*&& PLAYER2.position.x + 6.5f < moveDistanceLimit*/)
        {
            PLAYER2.Translate(0.2f, 0, 0);
        }
    }

    public void PlayerSuperAttackHitAttempt()
    {
        StartCoroutine(makeRandomBark());
        if (transform.position.x - 6f < PLAYER1.position.x)      //see if the player can hit them because they're on the ground!!!
        {
            GameObject.Find("crowdPassive").GetComponent<CrowdPassive>().changePassiveSound();
            //call to damage function of the other player
            Player1Script.Player1TakeDamage(superDamage);
        }
        player2SuperBarFillAmount = 0f;
        updateSuperBarDisplay();
    }

    public void PlayerRegularAttackHitAttempt()
    {
        Debug.Log("P2RegAttackHit");
        if (transform.position.x - 6f < PLAYER1.position.x)       //see if the player can hit them because they're on the ground!!!
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
                    //player super hits
                    Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.GotHit);
                    player2HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    //player super hits against block
                    doStunned = true;
                    player2HealthBarFillAmount -= damage / 400;
                    Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.GotHit);
                }
            }
            else
            {
                if (!isBlocking)
                {
                    //player regular hit
                    player2HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    //player regular hits against block
                    Player1Script.doStunned = true;
                    player2HealthBarFillAmount -= damage / 400;
                    Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.GotHit);
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
        if (currentState.Length != 0)
        {
            setAllToFalseBut(currentState);
        }
        else
        {
            Debug.Log("Ain't got NO state :(");
        }
        if (!(type.Equals(Player1And2Manager.playerActionType.BlockSTOP) && !Player1And2Manager.Instance.Player2GetCurrent().Equals(Player1And2Manager.playerActionType.BlockSTOP)))
        {
            Player1And2Manager.Instance.DefaultStateChange(2);
        }
    }

    public void PlayerFinishedBlockStart()
    {
        isBlocking = true;
        
        Player2Animator.SetBool("BlockState", true);
        Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.Block);
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


    IEnumerator makeRandomBark()
    {
        yield return new WaitForSeconds(0.2f);
        int rand = Random.Range(0, BarkSoundArray.Length);
        gameObject.GetComponent<AudioSource>().clip = BarkSoundArray[rand];
        gameObject.GetComponent<AudioSource>().Play();
    }



    public void LOST()
    {
        Player2Animator.SetBool("Lost", true);
    }

    public void WON()
    {
        Player2Animator.SetBool("Won", true);
    }




}
