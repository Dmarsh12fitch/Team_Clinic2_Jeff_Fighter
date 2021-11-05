using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1And2Manager : MonoBehaviour
{
    //Making this a singleton _____________________________________
    public static Player1And2Manager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    //END Making this a singleton _________________________________


    public enum playerActionType
    {
        GotHit,
        Block,
        RegularAttack,
        SuperAttack,
        MoveBackwards,
        MoveForwards,
        Idle
    }
    public playerActionType player1CurrentAction;
    public playerActionType player1NextAction;

    public playerActionType player2CurrentAction;
    public playerActionType player2NextAction;


    public void aControlKeyIsDown(playerActionType player1And2Input)
    {

    }




}
