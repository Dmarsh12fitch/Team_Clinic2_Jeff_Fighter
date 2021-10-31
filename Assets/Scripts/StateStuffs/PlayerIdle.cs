using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jeffu_Fighter
{
    public class PlayerIdle : CharacterStateBase
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //is this even needed???
            if (!getPlayerController(animator).idle)
            {
                if (animator.GetBool("moveForward"))
                {
                    

                }

                if (animator.GetBool("moveBackward"))
                {


                }

                if (getPlayerController(animator).attack)
                {


                }

                if (getPlayerController(animator).superAttack)
                {


                }

                if (getPlayerController(animator).block)
                {


                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            
        }

    }
}


