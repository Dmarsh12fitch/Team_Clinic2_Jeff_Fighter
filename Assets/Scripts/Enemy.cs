using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float currentPosition = 4f;
    [SerializeField] float movementRange = 4f;

    [SerializeField] float health = 200f;
    public GameObject healthBar;
    [SerializeField] float baseDmg = 5f;
    [SerializeField] float specialAttackDmg = 20f;

    public PlayerController playerScript;


    public enum enemyAction
    {
        MoveLeft,
        MoveRight,
        Block,
        RegularAttack,
        SuperAttack
    }
    private enemyAction newEnemyActionNode;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnEnemyAttackNode()
    {
        //gameobject go = instantiate(prefab enemyattack node, at right location + rotation)
        //set the enemyActionToDo inside the script of go to newEnemyActionNode
    }

    void excecuteEnemyAttackNode(enemyAction actionToDo)
    {
        //perhaps do the other job here? or not...
    }


    void makeNewEnemyActionNode()
    {
        if(currentPosition - 1 != playerScript.currentPosition)
        {
            //must be far away
            int weightedRandAction = Random.Range(0, 25);     //move Left (0-10), block (11-15), regular attack (16-20), super attack (21-23), move Right (24+25)
            if(0 <= weightedRandAction && weightedRandAction <= 10)
            {
                newEnemyActionNode = enemyAction.MoveLeft;
            } else if(11 <= weightedRandAction && weightedRandAction <= 15)
            {
                newEnemyActionNode = enemyAction.Block;
            } else if(16 <= weightedRandAction && weightedRandAction <= 20)
            {
                newEnemyActionNode = enemyAction.RegularAttack;
            } else if(21 <= weightedRandAction && weightedRandAction <= 23)
            {
                newEnemyActionNode = enemyAction.SuperAttack;
            } else
            {
                newEnemyActionNode = enemyAction.MoveRight;
            }

        } else
        {
            //must be within reaching distnace
            int weightedRandAction = Random.Range(0, 25);     //ragular attack (0-10), block (11-15), move Right (16-20), super attack (21-23), move Left (24+25)
            if (0 <= weightedRandAction && weightedRandAction <= 10)
            {
                newEnemyActionNode = enemyAction.RegularAttack;
            }
            else if (11 <= weightedRandAction && weightedRandAction <= 15)
            {
                newEnemyActionNode = enemyAction.Block;
            }
            else if (16 <= weightedRandAction && weightedRandAction <= 20)
            {
                newEnemyActionNode = enemyAction.MoveRight;
            }
            else if (21 <= weightedRandAction && weightedRandAction <= 23)
            {
                newEnemyActionNode = enemyAction.SuperAttack;
            }
            else
            {
                newEnemyActionNode = enemyAction.MoveLeft;
            }

        }
    }


    public void moveLeft()
    {
        if (currentPosition - 1 != playerScript.currentPosition && currentPosition > -movementRange)
        {
            currentPosition -= 1f;
            transform.position = new Vector3(currentPosition, 0, 0);
            //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
        }
        else
        {
            //do something bc it tried to move into your space?? or nothing
            Debug.Log("Hey! I'm here! (Player speaking)");
            damageBadGuy(5);
        }
    }

    public void moveRight()
    {
        if (currentPosition + 1 != playerScript.currentPosition && currentPosition < movementRange)
        {
            currentPosition += 1f;
            transform.position = new Vector3(currentPosition, 0, 0);
            //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
        }
        else
        {
            //do something bc you tried to move into your space?? or nothing
            Debug.Log("Hey! I'm here! (Player Speaking)");
            damageBadGuy(5);
        }
    }

    void punch(float damage)
    {
        if (currentPosition - 1 == playerScript.currentPosition)
        {
            playerScript.damagePlayer(damage);
        }
    }

    public void regularAttack()
    {
        //program this
        Debug.Log("enemy regular Attacksshsihh");
    }

    public void superAttack()
    {
        //program this
        Debug.Log("enemy superAtaccksish");
    }

    public void block()
    {
        //program this
        Debug.Log("enemy blockssssish");
    }

    public void damageBadGuy(float damage)
    {
        if (true)       //IF ENEMY NOT IN BLOCK STATE
        {
            //activate damage animation
            health -= damage;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
        }
    }



}
