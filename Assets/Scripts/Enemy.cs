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
    private bool isBlocking;

    public GameObject enemyActionNodePrefab;
    private float actionSpawnTimer = 10f;

    public PlayerController playerScript;
    public Transform spawnLocoOfenemyAttackNodes;


    public enum enemyAction
    {
        MoveLeft,
        MoveRight,
        Block,
        RegularAttack,
        SuperAttack
    }
    private enemyAction newEnemyActionNodeType;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        actionSpawnTimer -= Time.deltaTime;
        if(actionSpawnTimer <= 0)
        {
            actionSpawnTimer = 10f;
            makeNewEnemyActionNode();
            spawnEnemyAttackNode();
        }
    }

    void spawnEnemyAttackNode()
    {
        GameObject go = Instantiate(enemyActionNodePrefab, spawnLocoOfenemyAttackNodes);         //spawn it in the bar on the top the screen not wherever this is
        go.gameObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        if(newEnemyActionNodeType == enemyAction.MoveLeft)
        {
            go.gameObject.GetComponent<enemyAttackNodeScr>().enemyActionToDo = enemyAttackNodeScr.enemyAction.MoveLeft;
        } else if(newEnemyActionNodeType == enemyAction.MoveRight)
        {
            go.gameObject.GetComponent<enemyAttackNodeScr>().enemyActionToDo = enemyAttackNodeScr.enemyAction.MoveRight;
        } else if(newEnemyActionNodeType == enemyAction.Block)
        {
            go.gameObject.GetComponent<enemyAttackNodeScr>().enemyActionToDo = enemyAttackNodeScr.enemyAction.Block;
        } else if(newEnemyActionNodeType == enemyAction.RegularAttack)
        {
            go.gameObject.GetComponent<enemyAttackNodeScr>().enemyActionToDo = enemyAttackNodeScr.enemyAction.RegularAttack;
        } else
        {
            go.gameObject.GetComponent<enemyAttackNodeScr>().enemyActionToDo = enemyAttackNodeScr.enemyAction.SuperAttack;
        }
    }

    void makeNewEnemyActionNode()
    {
        if(currentPosition - 1 != playerScript.currentPosition)
        {
            //must be far away
            int weightedRandAction = Random.Range(0, 25);     //move Left (0-10), block (11-15), regular attack (16-20), super attack (21-23), move Right (24+25)
            if(0 <= weightedRandAction && weightedRandAction <= 10)
            {
                newEnemyActionNodeType = enemyAction.MoveLeft;
            } else if(11 <= weightedRandAction && weightedRandAction <= 15)
            {
                newEnemyActionNodeType = enemyAction.Block;
            } else if(16 <= weightedRandAction && weightedRandAction <= 20)
            {
                newEnemyActionNodeType = enemyAction.RegularAttack;
            } else if(21 <= weightedRandAction && weightedRandAction <= 23)
            {
                newEnemyActionNodeType = enemyAction.SuperAttack;
            } else
            {
                newEnemyActionNodeType = enemyAction.MoveRight;
            }

        } else
        {
            //must be within reaching distnace
            int weightedRandAction = Random.Range(0, 25);     //ragular attack (0-10), block (11-15), move Right (16-20), super attack (21-23), move Left (24+25)
            if (0 <= weightedRandAction && weightedRandAction <= 10)
            {
                newEnemyActionNodeType = enemyAction.RegularAttack;
            }
            else if (11 <= weightedRandAction && weightedRandAction <= 15)
            {
                newEnemyActionNodeType = enemyAction.Block;
            }
            else if (16 <= weightedRandAction && weightedRandAction <= 20)
            {
                newEnemyActionNodeType = enemyAction.MoveRight;
            }
            else if (21 <= weightedRandAction && weightedRandAction <= 23)
            {
                newEnemyActionNodeType = enemyAction.SuperAttack;
            }
            else
            {
                newEnemyActionNodeType = enemyAction.MoveLeft;
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
        //do regular attack anim here
        punch(baseDmg);
    }

    public void superAttack()
    {
        //do super Attack Enemy anim here
        punch(specialAttackDmg);
    }

    public void block()
    {
        //program this
        Debug.Log("enemy blockssssish");
        //do block anim
        //set "is blocking" to true for a certain amount of time, then back to false
    }

    public void damageBadGuy(float damage)
    {
        if (!isBlocking)
        {
            //activate damage animation
            health -= damage;
            healthBar.gameObject.GetComponent<Image>().fillAmount = health / 200;
        }
    }



}
