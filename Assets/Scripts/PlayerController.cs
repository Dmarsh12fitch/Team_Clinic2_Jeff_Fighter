using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CameraController CameraControllerScript;
    public Animator animator;

    public bool idle = true;
    public bool tiredIdle;
    public bool moveForward;
    public bool moveBackward;
    public bool attack;
    public bool superAttack;
    public bool block;
    public bool damaged;




    public float currentPosition = -6f;
    [SerializeField] float movementRange = 6f;

    [SerializeField] float health = 200f;
    public GameObject healthBar;
    [SerializeField] float baseDmg = 5f;
    [SerializeField] float superAttackDmg = 25f;
    private bool isBlocking;

    [SerializeField] float punchesTillSuperAttackPunch = 7f;
    public GameObject superAttackBar;
    public GameObject ReadyForSuperAttackImage;

    public Enemy badGuyScript;


    // Start is called before the first frame update
    void Start()
    {
        badGuyScript = GameObject.Find("BadGuy").GetComponent<Enemy>();
        CameraControllerScript = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        if(health > 0 && idle)
        {
            getPlayerMovementInputs();
            getplayerhitInputs();
            getPlayerblockInputs();
        }

    }

    void getPlayerMovementInputs()
    {
        if (idle)
        {
            if (Input.GetKeyDown(KeyCode.A) && currentPosition > -movementRange)
            {
                movePlayer(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (currentPosition < badGuyScript.currentPosition - 2)
                {
                    movePlayer(1f);
                }
                else
                {
                    //do something bc you tried to move into the bad-guy's space
                    Debug.Log("Hey! I'm here! (Enemy Speaking)");
                    damagePlayer(5);
                }
            }
        }
    }

    void getplayerhitInputs()
    {
        if (idle) //IF in IDLE ONLY
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (punchesTillSuperAttackPunch <= 0)
                {
                    //super power punch
                    //do animation for super power hit
                    punchesTillSuperAttackPunch = 7f;
                    superAttackBar.gameObject.GetComponent<Image>().fillAmount = 0f;
                    ReadyForSuperAttackImage.gameObject.SetActive(false);
                    punch(superAttackDmg);
                }
                else
                {
                    //regular punch
                    animator.SetBool("attackRegular", true);
                    //do animation for hit
                    punch(baseDmg);
                }
            }
        }
    }

    void getPlayerblockInputs()
    {
        if (idle)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //do block stuff....yeah
                Debug.Log("BLOck de Enemy!!!");
                //do block anim
                //set isBlocking to true while this button is being held down, but back if not
            }
        }
    }







    public void damagePlayer(float damage)
    {
        CameraControllerScript.StartCoroutine(CameraControllerScript.Shake(0.15f, damage));
        if (!isBlocking)
        {
            //activate damage animation
            health -= damage;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
            if(health <= 0)
            {
                //do death stuffs
            }
        }
    }

    void movePlayer(float movementX)
    {
        currentPosition += movementX;
        //transform.position = new Vector3(currentPosition, 0, 0);        //this just teleports, make it slowly move to location
        idle = false;
        if(movementX > 0)
        {
            animator.SetBool("moveForward", true);
        } else
        {
            animator.SetBool("moveBackward", true);

        }
        //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
    }

    void punch(float damage)
    {
        if (currentPosition + 2 == badGuyScript.currentPosition)
        {
            if (damage == superAttackDmg)
            {
                badGuyScript.damageBadGuy(damage);
            }
            else
            {
                punchesTillSuperAttackPunch--;
                superAttackBar.gameObject.GetComponent<Image>().fillAmount = 1 - ((punchesTillSuperAttackPunch) / 7);
                if (superAttackBar.gameObject.GetComponent<Image>().fillAmount == 1)
                {
                    ReadyForSuperAttackImage.gameObject.SetActive(true);
                }
                badGuyScript.damageBadGuy(damage);
            }
        }
    }


    public void physicallyMove(float direction)
    {
        if(direction > 0)
        {
            //move it + OVER TIME
            //after done delete the physical move from before
        } else
        {
            //move it - OVER TIME
        }
    }

    /*
    void kick()
    {
        //VERSION IF ANIM BLENDING
        //do kick animation
        //if enemy is not blocking
            //call enemy's "fall back" function + damage them



        //VERSION IF **NO** ANIM BLENDING
        //do kick animation
        //if enemy is not blocking
            //damage enemy
        //if enemy is in idle
            //call enemy's "fall back" function
    }
    */
}
