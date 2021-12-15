using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private CameraController CameraControllerScript;

    public AudioClip[] PunchSoundArray;

    public AudioClip[] BarkSoundArray;

    string currentState = "IdleState";

    public Animator Player1Animator;
    private Transform PLAYER1;
    private Transform PLAYER2;

    public bool doStunned = false;

    private PlayerUIController Player1UIController;

    private Player2Scr Player2Script;

    private string[] statesStrings = { "MoveForwardState", "MoveBackwardState","RegularAttackState","SuperAttackState"
                                        ,"StartBlockState","EndBlockState","BlockState","StunnedState", "IdleState", "DeadState"
                                        ,"GotHitSuperTypeAState","GotHitSuperTypeBState"};

    public float player1HealthBarFillAmount = 1;   //from 0 - 1
    public float player1SuperBarFillAmount = 0;    //from 0 - 1
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
        setAllToFalseBut("StartBlockState");
        StartCoroutine(JustInCase());
    }

    public void P1BlockSTOP()
    {
        setAllToFalseBut("EndBlockState");
        StartCoroutine(JustInCase());
    }

    public void P1RegularAttack()
    {
        int parameters = Player1Animator.parameterCount;
        AnimatorControllerParameter[] thingie;
        thingie = new AnimatorControllerParameter[parameters];
        foreach (var state in Player1Animator.parameters)
        {
            if (state.type is AnimatorControllerParameterType.Bool)
            {
                if (Player1Animator.GetBool(state.name) == true)
                {
                    currentState = state.name;
                }
            }
        }
        setAllToFalseBut("RegularAttackState");
        Player1Animator.SetTrigger("PunchNormTrig");
    }

    public void P1SuperAttack()
    {
        currentState = "IdleState";
        setAllToFalseBut("SuperAttackState");
        Player1Animator.SetTrigger("PunchSuperTrig");
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
        isBlocking = false;
        doStunned = false;
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




    IEnumerator JustInCase()
    {
        yield return new WaitForSeconds(0.25f);
        
        if(Player1And2Manager.Instance.Player1GetCurrent() == Player1And2Manager.playerActionType.BlockSTOP
            || Player1And2Manager.Instance.Player1GetCurrent() == Player1And2Manager.playerActionType.BlockSTART)
        {
            Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.Idle);
        }
    }




















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
            StartCoroutine(PlayerBackToRingSide());
        }
    }

    IEnumerator Player1FallingBack()
    {
        GameObject.Find("crowd").GetComponent<crowdOcc>().randSound();
        yield return new WaitForSeconds(0.05f);
        player1Moving = -2;
        yield return new WaitForSeconds(0.25f);
        player1Moving = -1;
        yield return new WaitForSeconds(0.15f);
        player1Moving = 0;
    }

    IEnumerator PlayerBackToRingSide()
    {
        GameObject.Find("crowd").GetComponent<crowdOcc>().randSound();
        for (int i = 0; i < 100; i++)
        {
            if (Player1And2Manager.Instance.Player1GetCurrent().Equals(Player1And2Manager.playerActionType.GotHit))
            {
                if (transform.position.x > moveDistanceLimit + 0.5f)
                {
                    player1Moving = -2;
                }
                else
                {
                    player1Moving = 0;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public void Player1MoveMeSet(float dir)
    {
        player1Moving = dir;
    }

    private void Player1Move()
    {
        if (player1Moving == 1 && PLAYER1.position.x + 5.3f < PLAYER2.position.x)
        {
            PLAYER1.Translate(0.1f, 0, 0);
        }
        else if (player1Moving == -1 && PLAYER1.position.x - 1f > moveDistanceLimit)
        {
            PLAYER1.Translate(-0.1f, 0, 0);
        } else if(player1Moving == -2 /*&& PLAYER1.position.x - 6.5f > moveDistanceLimit*/)
        {
            PLAYER1.Translate(-0.2f, 0, 0);
        }
    }

    public void PlayerSuperAttackHitAttempt()
    {
        StartCoroutine(makeRandomBark());
        if (transform.position.x + 6f > PLAYER2.position.x)           //see if the player can hit them because they're on the ground!!!
        {
            GameObject.Find("crowdPassive").GetComponent<CrowdPassive>().changePassiveSound();
            CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(superDamage));
            Player2Script.Player2TakeDamage(superDamage);
        }
        updateSuperBarDisplay();
    }

    public void PlayerRegularAttackHitAttempt()
    {
        Debug.Log("P1RegAttackHit");
        if(transform.position.x + 6f > PLAYER2.position.x)               //see if the player can hit them because they're on the ground!!!
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
                    //player super hits
                    Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.GotHit);
                    player1HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    //player super hits against block
                    doStunned = true;
                    player1HealthBarFillAmount -= damage / 400;
                    Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.GotHit);
                }
            }
            else
            {
                if (!isBlocking)
                {
                    //player regular hit
                    player1HealthBarFillAmount -= damage / 100;
                }
                else
                {
                    //player regular hits against block
                    Player2Script.doStunned = true;
                    player1HealthBarFillAmount -= damage / 400;
                    Player1And2Manager.Instance.Player2SetNextTo(Player1And2Manager.playerActionType.GotHit);
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
        if (currentState.Length != 0)
        {
            setAllToFalseBut(currentState);
        }
        else
        {
            Debug.Log("Ain't got NO state :(");
        }
        if (!(type.Equals(Player1And2Manager.playerActionType.BlockSTOP) && !Player1And2Manager.Instance.Player1GetCurrent().Equals(Player1And2Manager.playerActionType.BlockSTOP)))
        {
            Player1And2Manager.Instance.DefaultStateChange(1);
        }
    }

    public void PlayerFinishedBlockStart()
    {
        isBlocking = true;
        Player1Animator.SetBool("BlockState", true);
        Player1And2Manager.Instance.Player1SetNextTo(Player1And2Manager.playerActionType.Block);
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

    IEnumerator makeRandomBark()
    {
        yield return new WaitForSeconds(0.2f);
        int rand = Random.Range(0, BarkSoundArray.Length);
        gameObject.GetComponent<AudioSource>().clip = BarkSoundArray[rand];
        gameObject.GetComponent<AudioSource>().Play();
    }




    //end game
    public void LOST()
    {
        transform.position = new Vector3(-6, transform.position.x, transform.position.y);
        Player1Animator.SetBool("Lost", true);
    }

    public void WON()
    {
        transform.position = new Vector3(-6, transform.position.x, transform.position.y);
        Player1Animator.SetBool("Won", true);
    }



}
