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


    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void doAction()
    {
        //if far away
            //(order of likelihood) move Left, block, regular attack, super attack, move Right
        
        //if close
            //(order of likelihood) regular attack, block, move Right, super attack, move Left



        //the result of this ends up with an action thing being spawned and going across the screen, and when it hits then the enemy executes it
    }


    void moveLeft()
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

    void moveRight()
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
