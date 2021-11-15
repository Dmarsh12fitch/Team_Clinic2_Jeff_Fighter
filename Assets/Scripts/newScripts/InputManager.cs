using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Making this a singleton _____________________________________
    public static InputManager Instance = null;

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
    /*
    //Player1 button states
    private bool player1MoveBackwardButtonDown;
    private bool player1MoveForwardButtonDown;
    private bool player1BlockButtonDown;

    //Player2 button states
    private bool player2MoveBackwardButtonDown;
    private bool player2MoveForwardButtonDown;
    private bool player2BlockButtonDown;
    */
    //checks for all the keys pressed
    void Update()
    {
        //PLAYER1 INPUTS BEGIN -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


        //get MoveBackwards
        if (Input.GetKeyDown(KeyCode.A))
        {
            //send a control key request to enable movebackwards
            Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.MoveBackwards);
        } else
        {
            //when button is let up
            if (Input.GetKeyUp(KeyCode.A))
            {
                //send a control key request to disable movebackwards
                Player1And2Manager.Instance.ControlKeyUp(1, Player1And2Manager.playerActionType.MoveBackwards);
            }



            //Get MoveForwards
            if (Input.GetKeyDown(KeyCode.D))
            {
                //send a control key request to enable moveforwards
                Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.MoveForwards);
            } else
            {
                //when button is let up
                if (Input.GetKeyUp(KeyCode.D))
                {
                    //send a control key request to disable moveforwards
                    Player1And2Manager.Instance.ControlKeyUp(1, Player1And2Manager.playerActionType.MoveForwards);
                }



                //Get Block
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //send a control key request to enable block
                    Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.BlockSTART);
                } else
                {
                    //when button is let up
                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        //send a control key request to disable block
                        Player1And2Manager.Instance.ControlKeyUp(1, Player1And2Manager.playerActionType.BlockSTOP);
                    }



                    //Get RegularAttack
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        //send a control key request to enable regular attack
                        Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.RegularAttack);
                    } else
                    {
                        //no way to disable attack once launched


                        //Get SuperAttack
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            //send a control key request to enable super attack
                            Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.SuperAttack);
                            //no way to disable super attack once launched
                        }
                    }
                }
            }
        }


        /*
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Player1And2Manager.Instance.controlKeyDown(1, Player1And2Manager.playerActionType.MoveForwards);
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            Player1And2Manager.Instance.controlKeyDown(1, Player1And2Manager.playerActionType.Block);
        } else
        {
            //state that the player should not be doing this anymore


        }

    */
        //PLAYER1 INPUTS ENDSS -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=


        /*
        //player2 inputs
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Player1And2Manager.Instance.aControlKeyIsDown(2, Player1And2Manager.playerActionType.MoveForwards);
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Player1And2Manager.Instance.aControlKeyIsDown(2, Player1And2Manager.playerActionType.MoveBackwards);
        }
        */
    }






}
