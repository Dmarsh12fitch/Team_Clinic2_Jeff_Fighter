using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Scr : MonoBehaviour
{
    private Animator Player1Animator;

    public float player1HealthBarFillAmount = 0;   //from 0 - 1
    public float player1SuperBarFillAmount = 1;    //from 0 - 1

    // Start is called before the first frame update
    void Start()
    {
        Player1Animator = GameObject.Find("Player1_Display").GetComponent<Animator>();

    }



    //Player1 Action Calls START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


    //ARE THESE EVEN ACCURATE??? HOW AM I SUPPOSED TO MAKE THINGS SNAP???

    public void P1MoveForwards()
    {
        Player1Animator.SetBool("MoveForwardState", true);
        //that should be it bc when you let the key up it should snap to idle
    }

    public void P1MoveBackwards()
    {
        //setbool move forwards to true
        //that should be it bc when you let the key up it should snap to idle
    }

    public void P1BlockSTART()
    {
        //setbool blockingstart to true     //(should make it do the blocking start anim)
        //wait
        //setbool blocking to true          //(should make it stay in block) (this should be the bool to see if you actually block something or not)
        //set blockingstart to false
    }

    public void P1BlockSTOP()
    {
        //setbool blockingstop to true      //(should make it do the blocking stop anim)
        //setbool blocking to false         //(this should be the bool to see if you actually block something or not)^^
        //wait
        //setbool blockingstop to false
        //wait
        //IF blockSTOP is still the current, then call the DefaultStateChange
    }

    public void P1RegularAttack()
    {
        //setbool regularAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool regularAttack to false
        //If regularAttack is still the current, then call the DefaultStateChange
    }

    public void P1SuperAttack()
    {
        Player1Animator.SetBool("SuperAttackState", true);
        //setbool superAttack to true
        //wait                              //(in here somewhere is damage + shake if in range, shake if in range + blocking, nothing if not in range)
        //setbool superAttack to false
        //IF superAttack is still the current, then call the DefaultStateChange
    }

    public void P1GotHit()
    {

    }

    public void P1Idle()
    {
        //MAKE SURE THIS IS DONE RIHGT (disable ALL the other currents)
        Player1Animator.SetBool("SuperAttackState", false);
    }

    //Player2 Action Calls STOPP -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-































    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void Player1SuperAttackDamage()
    {
        Debug.Log("SuperAttackDamage!");
        //dmgother player
    }


    public void Player1RegularAttackDamage()
    {
        Debug.Log("RegularAttackDamage!");
        //dmgother player
    }

    public void Player1HasFinishedAnim()
    {
        //TEMPORARY
        Debug.Log("End this anim");
        Player1And2Manager.Instance.DefaultStateChange(1);
    }


}
