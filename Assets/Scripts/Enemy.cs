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

    void moveLeft()
    {
        if (currentPosition - 1 != playerScript.currentPosition)
        {
            currentPosition -= 1f;
            transform.position = new Vector3(currentPosition, 0, 0);
            //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
        }
        else
        {
            //do something bc it tried to move into your space?? or nothing
            Debug.Log("Hey! I'm here! (Player speaking)");
        }
    }

    void moveRight()
    {
        if (currentPosition + 1 != playerScript.currentPosition)
        {
            currentPosition += 1f;
            transform.position = new Vector3(currentPosition, 0, 0);
            //Instead of previous line, trigger the animation and then make it actually move (not teleport) with the animation
        }
        else
        {
            //do something bc you tried to move into your space?? or nothing
            Debug.Log("Hey! I'm here! (Player Speaking)");
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
