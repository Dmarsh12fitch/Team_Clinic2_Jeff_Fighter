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





    //checks for all the keys pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Player1And2Manager.Instance.aControlKeyIsDown(Player1And2Manager.playerActionType.MoveBackwards);
        }
    }






}
