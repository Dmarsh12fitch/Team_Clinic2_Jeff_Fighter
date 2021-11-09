using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private Animator Player1Animator;



    // Start is called before the first frame update
    void Start()
    {
        Player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Player1SuperAttackDamage()
    {
        //dmgother player
    }


    public void Player1RegularAttackDamage()
    {
        //dmgother player
    }

    public void Player1HasFinishedAnim()
    {
        
    }


}
