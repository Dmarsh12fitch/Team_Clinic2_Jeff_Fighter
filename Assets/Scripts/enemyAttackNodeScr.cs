using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackNodeScr : MonoBehaviour
{
    public enum enemyAction
    {
        MoveLeft,
        MoveRight,
        Block,
        RegularAttack,
        SuperAttack
    }
    public enemyAction enemyActionToDo;

    public Enemy badGuyScript;
    public GameObject display;
    private bool hasMadeCommand;

    private Vector3 translationX;



    // Start is called before the first frame update
    void Start()
    {
        badGuyScript = GameObject.Find("BadGuy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x > 297)
        {
            translationX = new Vector3(-2, 0, 0);
            transform.Translate(translationX);
        } else
        {
            if (!hasMadeCommand)
            {
                hasMadeCommand = true;
                if (enemyActionToDo.Equals(enemyAction.MoveLeft))
                {
                    badGuyScript.moveLeft();
                }
                else if (enemyActionToDo.Equals(enemyAction.MoveRight))
                {
                    badGuyScript.moveRight();
                }
                else if (enemyActionToDo.Equals(enemyAction.Block))
                {
                    badGuyScript.block();
                }
                else if (enemyActionToDo.Equals(enemyAction.RegularAttack))
                {
                    badGuyScript.regularAttack();
                }
                else
                {
                    badGuyScript.superAttack();
                }
                //maybe do some effect quick before dying?
                Destroy(gameObject, 0.5f);
            }
        }

    }
}
