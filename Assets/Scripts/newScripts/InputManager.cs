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


    public bool StopFightingInputs;
        
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
        if (!StopFightingInputs)
        {
            //PLAYER1 INPUTS BEGIN -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

            //get MoveBackwards
            if (Input.GetKeyDown(KeyCode.A))
            {
                //send a control key request to enable movebackwards
                Player1And2Manager.Instance.ControlKeyDown(1, Player1And2Manager.playerActionType.MoveBackwards);
            }
            else
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
                }
                else
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
                    }
                    else
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
                        }
                        else
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

            //PLAYER1 INPUTS ENDSS -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=



            //PLAYER2 INPUTS START -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=

            //get MoveBackwards
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //send a control key request to enable movebackwards
                Player1And2Manager.Instance.ControlKeyDown(2, Player1And2Manager.playerActionType.MoveBackwards);
            }
            else
            {
                //when button is let up
                if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    //send a control key request to disable movebackwards
                    Player1And2Manager.Instance.ControlKeyUp(2, Player1And2Manager.playerActionType.MoveBackwards);
                }



                //Get MoveForwards
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    //send a control key request to enable moveforwards
                    Player1And2Manager.Instance.ControlKeyDown(2, Player1And2Manager.playerActionType.MoveForwards);
                }
                else
                {
                    //when button is let up
                    if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        //send a control key request to disable moveforwards
                        Player1And2Manager.Instance.ControlKeyUp(2, Player1And2Manager.playerActionType.MoveForwards);
                    }



                    //Get Block
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        //send a control key request to enable block
                        Player1And2Manager.Instance.ControlKeyDown(2, Player1And2Manager.playerActionType.BlockSTART);
                    }
                    else
                    {
                        //when button is let up
                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            //send a control key request to disable block
                            Player1And2Manager.Instance.ControlKeyUp(2, Player1And2Manager.playerActionType.BlockSTOP);
                        }



                        //Get RegularAttack
                        if (Input.GetKeyDown(KeyCode.RightControl))                                                            //CHANGE THIS FOR THE ARCADE!!!
                        {
                            //send a control key request to enable regular attack
                            Player1And2Manager.Instance.ControlKeyDown(2, Player1And2Manager.playerActionType.RegularAttack);
                        }
                        else
                        {
                            //no way to disable attack once launched


                            //Get SuperAttack
                            if (Input.GetKeyDown(KeyCode.RightAlt))                                                             //CHANGE THIS FOR THE ARCADE!!!
                            {
                                //send a control key request to enable super attack
                                Player1And2Manager.Instance.ControlKeyDown(2, Player1And2Manager.playerActionType.SuperAttack);
                                //no way to disable super attack once launched
                            }
                        }
                    }
                }
            }





            //PLAYER2 INPUTS ENDSS -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
        }
    }






}
