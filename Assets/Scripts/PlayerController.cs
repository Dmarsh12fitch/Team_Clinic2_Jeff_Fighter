using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float currentPosition = -4f;
    [SerializeField] float movementRange = 4f;

    [SerializeField] float health = 200f;
    public GameObject healthBar;
    [SerializeField] float baseDmg = 5f;
    [SerializeField] float superAttackDmg = 25f;

    [SerializeField] float punchesTillSuperAttackPunch = 7f;
    public GameObject superAttackBar;
    public GameObject ReadyForSuperAttackImage;

    public Enemy badGuyScript;


    // Start is called before the first frame update
    void Start()
    {
        badGuyScript = GameObject.Find("BadGuy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //inputs
        if(health > 0)  //AND ONLY while in IDLE!!!
        {
            getPlayerMovementInputs();
            getplayerhitInputs();
            getPlayerblockInputs();
        }








    }

    void getPlayerMovementInputs()
    {
        if (true)   //IF in IDLE ONLY
        {
            if (Input.GetKeyDown(KeyCode.A) && currentPosition > -movementRange)
            {
                if (currentPosition - 1 != badGuyScript.currentPosition)
                {
                    movePlayer(-1);
                }
                else
                {
                    //do something bc you tried to move into the bad-guy's space
                    Debug.Log("Hey! I'm here! (Enemy Speaking)");
                    damagePlayer(5);
                }

            }
            else if (Input.GetKeyDown(KeyCode.D) && currentPosition < movementRange)
            {
                if (currentPosition + 1 != badGuyScript.currentPosition)
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
        if (true) //IF in IDLE ONLY
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
                    //do animation for hit
                    punch(baseDmg);
                }
            }
        }
    }

    void getPlayerblockInputs()
    {
        if (true)       //IF in Idle ONLY
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //do block stuff....yeah
            }
        }
    }







    public void damagePlayer(float damage)
    {
        if (true)   //IF PLAYER NOT IN BLOCK STATE
        {
            //activate damage animation
            health -= damage;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
        }
    }

    void movePlayer(float movementX)
    {
        currentPosition += movementX;
        transform.position = new Vector3(currentPosition, 0, 0);
        //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
    }

    void punch(float damage)
    {
        if (currentPosition + 1 == badGuyScript.currentPosition)
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

}